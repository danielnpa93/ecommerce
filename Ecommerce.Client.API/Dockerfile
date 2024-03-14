# #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src ./

# RUN dotnet restore

# RUN dotnet publish -c Release -o out


# # COPY ["./Ecommerce.Client.API/Ecommerce.Clients.API.csproj", "services/Ecommerce.Clients.API/"]
# # COPY ["../buildingBlocks/Ecommerce.Core/Ecommerce.Core.csproj", "buildingBlocks/Ecommerce.WebAPI.Core/"]
# # COPY ["../buildingBlocks/Ecommerce.WebAPI.Core/Ecommerce.WebAPI.Core.csproj", "buildingBlocks/Ecommerce.WebAPI.Core/"]
# # COPY ["../buildingBlocks/Ecommerce.MessageBus/Ecommerce.MessageBus.csproj", "buildingBlocks/Ecommerce.MessageBus/"]

# # RUN dotnet restore "services/Ecommerce.Clients.API/Ecommerce.Clients.API.csproj"
# # COPY . .
# # WORKDIR "services/Ecommerce.Clients.API"
# # RUN dotnet build "Ecommerce.Clients.API.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "Ecommerce.Clients.API.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Ecommerce.Clients.API.dll"]



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ./Ecommerce.Client.API ./
COPY ../buildingBlocks/. ./

# RUN dotnet restore "Ecommerce.Clients.API.csproj"

# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:8.0

# WORKDIR /App
# COPY --from=build-env /App/out .

ENV ASPNETCORE_URLS http://+:8080
EXPOSE 8080
# EXPOSE 443

# ENTRYPOINT ["dotnet", "Ecommerce.Clients.API.dll"]
 ENTRYPOINT ["tail", "-f", "/dev/null"]
