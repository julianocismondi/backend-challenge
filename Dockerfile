FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /sln

# Copia el archivo de solucion .sln
COPY *.sln .

# Copia todos los proyectos .csproj de la carpeta src/
COPY *.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "backend-challenge.dll"]