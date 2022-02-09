param (
	[Int32][ValidateRange(1, 10)] $nodes = 1
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048,
	[string] $registry = '',
	[string] $tag = '',
	[switch] $build = $false
)
$JOBS = @()
Remove-Job -Name RunJob* -Force

#Start kubernates
echo '📌 Start minikube'

minikube start --cpus $cpus --memory $memory --nodes=$nodes --insecure-registry '10.0.0.0/24'
minikube addons enable ingress

If ($registry.StartsWith('localhost'))
{
	echo '📌 Create image registry'
	minikube addons enable registry

	$JOBS+=Start-Job -Name RunJobPortForward -ScriptBlock { kubectl port-forward --namespace kube-system service/registry 5000:80 }
	$JOBS+=Start-Job -Name RunJobRedirectPort -ScriptBlock { docker run --rm --network=host alpine ash -c 'apk add socat && socat TCP-LISTEN:5000,reuseaddr,fork TCP:host.docker.internal:5000' }
}

#Build images and push to registry
If (![string]::IsNullOrEmpty($registry) -And !$registry.EndsWith('/'))
{
	$registry += '/'
}

$Env:DOCKER_REGISTRY = $registry
$Env:TAG = $tag
If ($build)
{
	If ([string]::IsNullOrEmpty($registry))
	{
		& minikube -p minikube docker-env | Invoke-Expression
	}
	
	echo '📌 Build images'
	docker-compose -f docker-compose/docker-compose.yml -f docker-compose/docker-compose.override.yml build
	
	If ([string]::IsNullOrEmpty($registry))
	{
		& minikube -p minikube docker-env -u | Invoke-Expression
	}
	Else
	{
		echo '📌 Push images to registry'
		docker-compose -f docker-compose/docker-compose.yml -f docker-compose/docker-compose.override.yml push
	}
}

#Deploy to kubernates
echo '📌 Deploy to minikube'
$directory = '.\k8s\overlays\temp'
$file = $directory + '\kustomization.yaml'

New-Item $directory -ItemType Directory -Force | Out-Null
New-Item $file -ItemType File -Force | Out-Null

Add-Content $file 'bases:'
Add-Content $file '  - "../../base"'

Add-Content $file 'images:'

$images = @('gateway','identity-service','movie-service','payment-service','review-service','test-service')
Foreach ($image in $images)
{
	Add-Content $file ('  - name: ' + $image)
	If (![string]::IsNullOrEmpty($tag))
	{
		Add-Content $file ('    newTag: "' + $tag + '"')
	}
	If (![string]::IsNullOrEmpty($registry))
	{
		Add-Content $file ('    newName: "' + $registry + $image + '"')
	}
}

kubectl apply -k $directory

Remove-Item -Path $directory -Recurse -Force

#Expose services
$JOBS+=Start-Job -Name RunJobTunnel -ScriptBlock { minikube tunnel }
$JOBS+=Start-Job -Name RunJobDashboard -ScriptBlock { minikube dashboard }
#Wait-Job $JOBS

#Finish
echo '📌 Finish'