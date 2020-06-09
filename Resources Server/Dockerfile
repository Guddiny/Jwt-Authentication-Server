FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY src/ProtectedAPIResource/bin/Docker .
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet ProtectedAPIResource.dll --urls "http://*:5000"