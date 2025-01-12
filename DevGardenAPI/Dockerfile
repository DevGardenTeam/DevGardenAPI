#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DevGardenAPI/DevGardenAPI.csproj", "DevGardenAPI/"]
COPY ["Auth/Auth.csproj", "Auth/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["DatabaseEf/DatabaseEf.csproj", "DatabaseEf/"]
RUN dotnet restore "./DevGardenAPI/./DevGardenAPI.csproj"
COPY . .
WORKDIR "/src/DevGardenAPI"
RUN dotnet build "./DevGardenAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevGardenAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV SWAGGER_BASE_PATH=/containers/DevGarden-devgardenapi
ENTRYPOINT ["dotnet", "DevGardenAPI.dll"]