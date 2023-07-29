#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/HmanResource/NetSquare.ERP.HumanResource.Api/NetSquare.ERP.HumanResource.Api.csproj", "src/Services/HmanResource/NetSquare.ERP.HumanResource.Api/"]
COPY ["src/Services/HmanResource/NetSquare.ERP.HumanResource.Infrastructure/NetSquare.ERP.HumanResource.Infrastructure.csproj", "src/Services/HmanResource/NetSquare.ERP.HumanResource.Infrastructure/"]
COPY ["src/Services/HmanResource/NetSquare.ERP.HumanResource.Domain/NetSquare.ERP.HumanResource.Domain.csproj", "src/Services/HmanResource/NetSquare.ERP.HumanResource.Domain/"]
COPY ["src/BuildingBlocks/SystemExtensions/SystemExtensions.csproj", "src/BuildingBlocks/SystemExtensions/"]
COPY ["src/BuildingBlocks/ExceptionHandler/ExceptionHandler.csproj", "src/BuildingBlocks/ExceptionHandler/"]
RUN dotnet restore "src/Services/HmanResource/NetSquare.ERP.HumanResource.Api/NetSquare.ERP.HumanResource.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/HmanResource/NetSquare.ERP.HumanResource.Api"
RUN dotnet build "NetSquare.ERP.HumanResource.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetSquare.ERP.HumanResource.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSquare.ERP.HumanResource.Api.dll"]
