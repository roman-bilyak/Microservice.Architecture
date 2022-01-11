minikube start --cpus 4 --memory 8192

& minikube -p minikube docker-env | Invoke-Expression
docker-compose -f docker-compose.yml -f docker-compose.override.yml build

kubectl apply -k ./k8s

Start-Sleep -s 10

$jobs=@()
$portforward = {
    param($app, $port)
    kubectl port-forward "$app" "$port"
}

$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/gateway,51500:80
$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/test-service,51505:80
Wait-Job $jobs