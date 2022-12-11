#Anterior: See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#Actual: https://www.developerro.com/2022/04/27/dockerfile-net6-test/

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

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build #Imagen oficial de miscrosoft .Net 6 con SDK
WORKDIR /src #directorio de trabajo donde se copia el código fuente
COPY .. #copia del código fuente
RUN dotnet publish "ProyectoES/Program.cs" -c Release -o /app #código para publicar la aplicación en un directorio dentro del contenedor

