#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM node:latest as node
WORKDIR /app

COPY ["applications/Microservice.App.Angular/package*.json", "./"]
RUN npm install

COPY ["applications/Microservice.App.Angular", "./"]
RUN npm run build --prod

FROM nginx:alpine
COPY --from=node /app/dist/microservice.app.angular /usr/share/nginx/html

COPY --from=node /app/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
