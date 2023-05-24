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

docker-compose -f docker-compose/docker-compose.yml -f docker-compose/docker-compose.override.yml -f docker-compose-api/docker-compose.yml -f docker-compose-api/docker-compose.override.yml build

If ([string]::IsNullOrEmpty($registry))
{
	& minikube -p minikube docker-env -u | Invoke-Expression
}
Else
{
	echo '📌 Push images to registry'
	docker-compose -f docker-compose/docker-compose.yml -f docker-compose/docker-compose.override.yml -f docker-compose-api/docker-compose.yml -f docker-compose-api/docker-compose.override.yml push
}
