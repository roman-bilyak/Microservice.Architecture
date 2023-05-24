param (
	[string] $registry = '',
	[string] $tag = ''
)

echo '📌 Deploy to minikube'
$directory = '.\overlays\temp'
$file = $directory + '\kustomization.yaml'

New-Item $directory -ItemType Directory -Force | Out-Null
New-Item $file -ItemType File -Force | Out-Null

Add-Content $file 'bases:'
Add-Content $file '  - "../../base"'

Add-Content $file 'images:'

$images = @('api-auth-service', 'api-gateway','api-identity-service','api-identity-service-migrator','api-movie-service','api-movie-service-migrator','api-payment-service','api-payment-service-migrator','api-review-service','api-review-service-migrator','api-test-service','api-test-service-migrator')
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