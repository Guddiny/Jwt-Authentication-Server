FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY src/AuthServer/bin/Docker .
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet AuthServer.dll --urls "http://*:8000"