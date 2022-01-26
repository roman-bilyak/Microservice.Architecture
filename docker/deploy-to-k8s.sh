#!/bin/bash
set -e

export SHELL="/bin/bash"

minikube start --cpus 8 --memory 8192
minikube addons enable ingress

eval $(minikube docker-env)
docker-compose -f docker-compose.yml -f docker-compose.override.yml build

kubectl apply -k ./k8s

kubectl port-forward deployment.apps/gateway 51500:80