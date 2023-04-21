#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:latest as node
WORKDIR /app

COPY ["applications/Microservice.ReactUI", "./"]
RUN npm install
RUN npm run-script build

FROM nginx:alpine
COPY --from=node /app/build/ /usr/share/nginx/html
