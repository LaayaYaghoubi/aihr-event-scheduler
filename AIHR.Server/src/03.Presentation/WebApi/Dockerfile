FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/03.Presentation/WebApi/AIHR.EventScheduler.WebApi.csproj", "src/03.Presentation/WebApi/"]
COPY ["src/02.Infrastructure/Infrastructure/AIHR.EventSchedulerInfrastructure.csproj", "src/02.Infrastructure/Infrastructure/"]
COPY ["src/00.Framework/Contracts/AIHR.EventScheduler.Contracts.csproj", "src/00.Framework/Contracts/"]
COPY ["src/02.Infrastructure/Persistence.EF/AIHR.EventScheduler.Persistence.EF.csproj", "src/02.Infrastructure/Persistence.EF/"]
COPY ["src/01.Core/Application/AIHR.EventScheduler.Application.csproj", "src/01.Core/Application/"]
COPY ["src/01.Core/Domain/AIHR.EventScheduler.Domain.csproj", "src/01.Core/Domain/"]
RUN dotnet restore "src/03.Presentation/WebApi/AIHR.EventScheduler.WebApi.csproj"
COPY . .
WORKDIR "/src/src/03.Presentation/WebApi"
RUN dotnet build "AIHR.EventScheduler.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AIHR.EventScheduler.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIHR.EventScheduler.WebApi.dll"]
