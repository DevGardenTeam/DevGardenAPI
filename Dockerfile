FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ["DevGardenAPI/DevGardenAPI.csproj", "DevGardenAPI/"]
COPY ["Auth/Auth.csproj", "Auth/"]
COPY ["Model/Model.csproj", "Model/"]
RUN dotnet restore "DevGardenAPI/DevGardenAPI.csproj"
COPY . .
WORKDIR "/src/DevGardenAPI"
ARG ENV=release
RUN dotnet build "DevGardenAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevGardenAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevGardenAPI.dll"]