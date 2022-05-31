#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:latest as node
WORKDIR /app
COPY ["applications/Microservice.AngularUI", "./"]

RUN npm install
RUN npm run build --prod

FROM nginx:alpine
COPY --from=node /app/dist/microservice.angular-ui /usr/share/nginx/html
