# Build stage
FROM node:latest as node
WORKDIR /app

COPY ["applications/Microservice.App.Angular/package*.json", "./"]
RUN npm install

COPY ["applications/Microservice.App.Angular", "./"]
RUN npm run build --prod

# Production stage
FROM nginx:alpine
COPY --from=node /app/dist/microservice.app.angular /usr/share/nginx/html
COPY --from=node /app/nginx.conf /etc/nginx/conf.d/default.conf

ENTRYPOINT sed -i "s|ENVIRONMENT_BASE_URL|$BASE_URL|g" /usr/share/nginx/html/main.*.js && \
           sed -i "s|ENVIRONMENT_AUTH_URL|$AUTH_URL|g" /usr/share/nginx/html/main.*.js && \
           sed -i "s|ENVIRONMENT_API_URL|$API_URL|g" /usr/share/nginx/html/main.*.js && \
           nginx -g 'daemon off;'

EXPOSE 80
EXPOSE 443
