param (
    [string] $registry = '',
    [string] $tag = ''
)

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