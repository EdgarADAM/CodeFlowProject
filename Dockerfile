FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ADAM-SaaS-Incidences/IncidentManagement.csproj", "ADAM-SaaS-Incidences/"]
RUN dotnet restore "./ADAM-SaaS-Incidences/./IncidentManagement.csproj"
COPY . .
WORKDIR "/src/ADAM-SaaS-Incidences"
RUN dotnet build "./IncidentManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IncidentManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IncidentManagement.dll"]