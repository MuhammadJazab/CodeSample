#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Authentication/NetSquare.ERP.Authentication.Api/NetSquare.ERP.Authentication.Api.csproj", "src/Services/Authentication/NetSquare.ERP.Authentication.Api/"]
COPY ["src/Services/Authentication/NetSquare.ERP.Authentication.Domain/NetSquare.ERP.Authentication.Domain.csproj", "src/Services/Authentication/NetSquare.ERP.Authentication.Domain/"]
COPY ["src/Services/Authentication/NetSquare.ERP.Authentication.Infrastructure/NetSquare.ERP.Authentication.Infrastructure.csproj", "src/Services/Authentication/NetSquare.ERP.Authentication.Infrastructure/"]
COPY ["src/BuildingBlocks/SystemExtensions/SystemExtensions.csproj", "src/BuildingBlocks/SystemExtensions/"]
COPY ["src/BuildingBlocks/ExceptionHandler/ExceptionHandler.csproj", "src/BuildingBlocks/ExceptionHandler/"]
RUN dotnet restore "src/Services/Authentication/NetSquare.ERP.Authentication.Api/NetSquare.ERP.Authentication.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Authentication/NetSquare.ERP.Authentication.Api"
RUN dotnet build "NetSquare.ERP.Authentication.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetSquare.ERP.Authentication.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSquare.ERP.Authentication.Api.dll"]
