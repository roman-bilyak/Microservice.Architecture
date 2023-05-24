param (
	[Int32][ValidateRange(1, 10)] $nodes = 1,
	[Int32][ValidateRange(2, 10)] $cpus = 2,
	[Int32][ValidateRange(2048, 8192)] $memory = 2048
)

echo '📌 Start minikube'

if ((minikube profile list -o json 2>$null | ConvertFrom-Json).valid | Where-Object { $_.Name -eq 'minikube'}) {
    minikube delete --all
}

minikube start --nodes $nodes --cpus $cpus --memory $memory --insecure-registry '10.0.0.0/24' --addons dashboard --addons ingress --addons registry

Remove-Job -Name RunJob* -Force
	
Start-Job -Name RunJobPortForward -ScriptBlock { kubectl port-forward --namespace kube-system service/registry 5000:80 } | Out-Null
Start-Job -Name RunJobRedirectPort -ScriptBlock { docker run --rm --network=host alpine ash -c 'apk add socat && socat TCP-LISTEN:5000,reuseaddr,fork TCP:host.docker.internal:5000' } | Out-Null