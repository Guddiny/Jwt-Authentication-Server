FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:8000
EXPOSE 8000
ARG Configuration=Release

WORKDIR /app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY AuthServer/src/AuthServer/AuthServer.csproj ./AuthServer/src/AuthServer/
COPY Common/src/Common/Common.csproj ./Common/src/Common/

COPY AuthServer/AuthServer.sln ./AuthServer/
RUN dotnet restore /AuthServer/AuthServer.sln

COPY . .
RUN dotnet build ./AuthServer/AuthServer.sln -o /app


