#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NoSqlStress.WebApi/NoSqlStress.WebApi.csproj", "NoSqlStress.WebApi/"]
RUN dotnet restore "NoSqlStress.WebApi/NoSqlStress.WebApi.csproj"
COPY . .
WORKDIR "/src/NoSqlStress.WebApi"
RUN dotnet build "NoSqlStress.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoSqlStress.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Требуется для запуска в GKE
ENV ASPNETCORE_URLS http://*:8080

ENTRYPOINT ["dotnet", "NoSqlStress.WebApi.dll"]