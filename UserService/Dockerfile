FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY UserService/UserService.csproj ./UserService/
COPY Application/Application.csproj ./Application/
COPY Entities/Entities.csproj ./Entities/
COPY UserRepository/UserRepository.csproj ./UserRepository/
RUN dotnet restore ./UserService/UserService.csproj
COPY UserService/ ./UserService/
COPY UserRepository/ ./UserRepository/
COPY Entities/ ./Entities/
COPY Application/ ./Application/
WORKDIR /src/UserService
RUN dotnet build "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release 
RUN dotnet publish "./UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]