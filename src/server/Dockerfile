# Docker file for ASP.NET Core Web API
# Working on Mac M1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet build QuizApp.WebAPI --configuration Release --output /app/build

FROM build as publish
RUN dotnet publish --configuration Release --output /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "QuizApp.WebAPI.dll"]