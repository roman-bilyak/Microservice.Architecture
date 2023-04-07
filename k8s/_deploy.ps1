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
		Add-Content $file ('    newName: "' + $registry + $image + '"')
	}
}

kubectl apply -k $directory

Remove-Item -Path $directory -Recurse -Force