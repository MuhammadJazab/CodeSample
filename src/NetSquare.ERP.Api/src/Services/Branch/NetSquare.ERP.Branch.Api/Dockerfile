#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Branch/NetSquare.ERP.Branch.Api/NetSquare.ERP.Branch.Api.csproj", "src/Services/Branch/NetSquare.ERP.Branch.Api/"]
COPY ["src/Services/Branch/NetSquare.ERP.Branch.Domain/NetSquare.ERP.Branch.Domain.csproj", "src/Services/Branch/NetSquare.ERP.Branch.Domain/"]
COPY ["src/Services/Branch/NetSquare.ERP.Branch.Infrastructure/NetSquare.ERP.Branch.Infrastructure.csproj", "src/Services/Branch/NetSquare.ERP.Branch.Infrastructure/"]
COPY ["src/BuildingBlocks/SystemExtensions/SystemExtensions.csproj", "src/BuildingBlocks/SystemExtensions/"]
COPY ["src/BuildingBlocks/ExceptionHandler/ExceptionHandler.csproj", "src/BuildingBlocks/ExceptionHandler/"]
RUN dotnet restore "src/Services/Branch/NetSquare.ERP.Branch.Api/NetSquare.ERP.Branch.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Branch/NetSquare.ERP.Branch.Api"
RUN dotnet build "NetSquare.ERP.Branch.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetSquare.ERP.Branch.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSquare.ERP.Branch.Api.dll"]
