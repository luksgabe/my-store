#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Products.WebApi/Products.WebApi.csproj", "Products.WebApi/"]
COPY ["Products.Application/Products.Application.csproj", "Products.Application/"]
COPY ["Products.Domain/Products.Domain.csproj", "Products.Domain/"]
COPY ["Products.CrossCutting.IoT/Products.CrossCutting.IoT.csproj", "Products.CrossCutting.IoT/"]
COPY ["Products.CrossCutting.Bus/Products.CrossCutting.Bus.csproj", "Products.CrossCutting.Bus/"]
COPY ["Products.Infra.Data/Products.Infra.Data.csproj", "Products.Infra.Data/"]
RUN dotnet restore "Products.WebApi/Products.WebApi.csproj"
COPY . .
WORKDIR "/src/Products.WebApi"
RUN dotnet build "Products.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.WebApi.dll"]