minikube start

& minikube -p minikube docker-env | Invoke-Expression
docker-compose -f docker-compose.yml -f docker-compose.override.yml build

kubectl apply -k ./k8s

Start-Sleep -s 10

kubectl port-forward deployment.apps/gateway 51500:80