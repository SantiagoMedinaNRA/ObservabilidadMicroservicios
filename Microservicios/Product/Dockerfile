# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Microservicios/Product/Product.csproj", "Microservicios/Product/"]

# Restore dependencies
RUN dotnet restore "Microservicios/Product/Product.csproj"

# Copy everything else
COPY . .

# Build the application
WORKDIR "/src/Microservicios/Product"
RUN dotnet build "Product.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Product.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port your app runs on
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Product.dll"]