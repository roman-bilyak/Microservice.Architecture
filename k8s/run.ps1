param (
	[Int32][ValidateRange(1, 10)] $nodes = 1,
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048,
	[string] $registry = '',
	[string] $tag = ''
)

echo '📌 Start minikube'

$enableRegistryAddon = $registry -eq 'localhost:5000'
$registryAddon = $(If ($enableRegistryAddon) { 'registry' } else { 'fake' })

minikube start --nodes $nodes --cpus $cpus --memory $memory --insecure-registry '10.0.0.0/24' --addons dashboard --addons ingress --addons $registryAddon

If ($enableRegistryAddon)
{
	Remove-Job -Name RunJob* -Force
	
	Start-Job -Name RunJobPortForward -ScriptBlock { kubectl port-forward --namespace kube-system service/registry 5000:80 } | Out-Null
	Start-Job -Name RunJobRedirectPort -ScriptBlock { docker run --rm --network=host alpine ash -c 'apk add socat && socat TCP-LISTEN:5000,reuseaddr,fork TCP:host.docker.internal:5000' } | Out-Null
}

echo '📌 Deploy to minikube'
$directory = '.\overlays\temp'
$file = $directory + '\kustomization.yaml'

New-Item $directory -ItemType Directory -Force | Out-Null
New-Item $file -ItemType File -Force | Out-Null

Add-Content $file 'bases:'
Add-Content $file '  - "../../base"'

Add-Content $file 'images:'

$images = @('gateway','identity-server','identity-service','movie-service','payment-service','review-service','test-service')
Foreach ($image in $images)
{
	Add-Content $file ('  - name: ' + $image)
	If (![string]::IsNullOrEmpty($tag))
	{
		Add-Content $file ('    newTag: "' + $tag + '"')
	}
	If (![string]::IsNullOrEmpty($registry))
	{
		Add-Content $file ('    newName: "' + $registry + '/' + $image + '"')
	}
}

kubectl apply -k $directory

Remove-Item -Path $directory -Recurse -Force

echo '📌 Expose services'
minikube tunnel