# Create dockerfile for angular application
FROM node:20.0 AS build
WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm run build

FROM nginx:1.21.3 AS final
WORKDIR /usr/share/nginx/html

COPY --from=build /app/dist/quiz-app .

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]