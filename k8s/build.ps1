param (
    [string] $registry = '',
    [string] $tag = ''
)

echo '📌 Start minikube'

$enableRegistryAddon = $registry -eq 'localhost:5000'
$registryAddon = $(If ($enableRegistryAddon) { 'registry' } else { 'fake' })

minikube start --insecure-registry '10.0.0.0/24' --addons dashboard --addons ingress --addons $registryAddon

If ($enableRegistryAddon)
{
    Remove-Job -Name RunJob* -Force
	
    Start-Job -Name RunJobPortForward -ScriptBlock { kubectl port-forward --namespace kube-system service/registry 5000:80 } | Out-Null
    Start-Job -Name RunJobRedirectPort -ScriptBlock { docker run --rm --network=host alpine ash -c 'apk add socat && socat TCP-LISTEN:5000,reuseaddr,fork TCP:host.docker.internal:5000' } | Out-Null
}

echo '📌 Build images'

$Env:DOCKER_REGISTRY = $registry
$Env:TAG = $tag

If ([string]::IsNullOrEmpty($registry))
{
    & minikube -p minikube docker-env | Invoke-Expression
}

docker-compose -f ../docker/docker-compose-infrastructure/docker-compose.yml -f ../docker/docker-compose-api/docker-compose.yml -f ../docker/docker-compose/docker-compose.yml build

If ([string]::IsNullOrEmpty($registry))
{
    & minikube -p minikube docker-env -u | Invoke-Expression
}
Else
{
    docker-compose -f ../docker/docker-compose-infrastructure/docker-compose.yml -f ../docker/docker-compose-api/docker-compose.yml -f ../docker/docker-compose/docker-compose.yml push
}