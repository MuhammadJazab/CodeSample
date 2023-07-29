#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Gateway/ApiGateway/NetSquare.ERP.Gateway.Api.csproj", "src/Gateway/ApiGateway/"]
COPY ["src/BuildingBlocks/SystemExtensions/SystemExtensions.csproj", "src/BuildingBlocks/SystemExtensions/"]
RUN dotnet restore "src/Gateway/ApiGateway/NetSquare.ERP.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/src/Gateway/ApiGateway"
RUN dotnet build "NetSquare.ERP.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetSquare.ERP.Gateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSquare.ERP.Gateway.Api.dll"]
