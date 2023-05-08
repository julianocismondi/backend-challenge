FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /sln

COPY . .

RUN dotnet restore

COPY ./backend-challenge.WebApi/. ./backend-challenge.WebApi/

FROM build AS publish

RUN dotnet publish "backend-challenge.WebApi/backend-challenge.WebApi.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /app
COPY --from=publish /app ./
ENTRYPOINT ["dotnet", "backend-challenge.WebApi.dll"]