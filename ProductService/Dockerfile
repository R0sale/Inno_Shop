FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ProductService/ProductService.csproj ./ProductService/
COPY Application/Application.csproj ./Application/
COPY Entities/Entities.csproj ./Entities/
COPY ProductRepository/ProductRepository.csproj ./ProductRepository/
RUN dotnet restore ./ProductService/ProductService.csproj
COPY ProductService/ ./ProductService/
COPY ProductRepository/ ./ProductRepository/
COPY Entities/ ./Entities/
COPY Application/ ./Application/
WORKDIR /src/ProductService
RUN dotnet build "ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release 
RUN dotnet publish "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]