#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/OrderService/Order.Api/Order.Api.csproj", "Services/OrderService/Order.Api/"]
COPY ["Services/OrderService/Order.Domain/Order.Domain.csproj", "Services/OrderService/Order.Domain/"]
COPY ["Services/OrderService/Order.Infrastructure/Order.Infrastructure.csproj", "Services/OrderService/Order.Infrastructure/"]
COPY ["Services/UserService/User.Domain/User.Domain.csproj", "Services/UserService/User.Domain/"]
RUN dotnet restore "./Services/OrderService/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/Services/OrderService/Order.Api"
RUN dotnet build "./Order.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]