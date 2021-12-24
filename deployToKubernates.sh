#!/bin/bash
set -e

export SHELL="/bin/bash"

minikube start

eval $(minikube docker-env)
docker-compose -f docker-compose.yml -f docker-compose.override.yml build

cd ./k8s
kubectl create -f gateway.yaml,identity-service.yaml,movie-service.yaml,payment-service.yaml,review-service.yaml
kubectl create -f gateway-deployment.yaml,identity-service-deployment.yaml,movie-service-deployment.yaml,payment-service-deployment.yaml,review-service-deployment.yaml

cd ..

kubectl port-forward deployment.apps/gateway 51500:80
kubectl port-forward deployment.apps/identity-service 51501:80
kubectl port-forward deployment.apps/movie-service 51502:80
kubectl port-forward deployment.apps/payment-service 51503:80
kubectl port-forward deployment.apps/review-service 51504:80