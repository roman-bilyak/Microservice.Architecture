minikube start

& minikube -p minikube docker-env | Invoke-Expression
docker-compose -f docker-compose.yml -f docker-compose.override.yml build

cd ./k8s
kubectl create -f gateway.yaml,identity-service.yaml,movie-service.yaml,payment-service.yaml,review-service.yaml
kubectl create -f gateway-deployment.yaml,identity-service-deployment.yaml,movie-service-deployment.yaml,payment-service-deployment.yaml,review-service-deployment.yaml

cd ..

Start-Sleep -s 10

$jobs=@()
$portforward = {
    param($app, $port)
    kubectl port-forward "$app" "$port"
}

$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/gateway,51500:80
$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/identity-service,51501:80
$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/movie-service,51502:80
$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/payment-service,51503:80
$jobs+=Start-Job -ScriptBlock $portforward -ArgumentList deployment.apps/review-service,51504:80
Wait-Job $jobs