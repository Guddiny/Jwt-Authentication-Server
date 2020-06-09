FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Common/Common.csproj", "src/Common/"]
RUN dotnet restore "src/Common/Common.csproj"
COPY . .
WORKDIR "/src/src/Common"
RUN dotnet build "Common.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Common.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Common.dll"]
