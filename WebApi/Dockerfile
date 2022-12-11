#Actual: https://www.developerro.com/2022/04/27/dockerfile-net6-test/

#A PARTIR DE AQUÍ BIEN, ESTO SE QUEDA
#1a Parte
#Imagen oficial de miscrosoft .Net 6 con SDK
#directorio de trabajo donde se copia el codigo fuente
#copia del codigo fuente
#codigo para publicar la aplicacion en un directorio dentro del contenedor

#2a Parte
#Ejecución de la aplicación usando un dotnet run sin usar la imagen SDK
#Creación de un directorio de trabajo nuevo y copia de los archivos binarios que se compilan en la imagen anterior
#Ejecución de la aplicacon

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "./WebApi.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebApi.dll"]





















#BORRAR A PARTIR DE AQUI, ES EL DOCKERFILE PRO DEFECTO
#Anterior: See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["WebApi/Program.cs", "WebApi/"]
#RUN dotnet restore "WebApi/WebApi.csproj"
#COPY . .
#WORKDIR "/src/WebApi"
#RUN dotnet build "WebApi.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WebApi.dll"]