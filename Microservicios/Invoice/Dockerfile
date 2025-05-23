# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
 
# Copy solution and project files
COPY ["Microservicios/Invoice/Invoice.csproj", "Microservicios/Invoice/"]
# COPY ["Microservicios/Microservicios.sln", "Microservicios/"]
 
# Restore dependencies
RUN dotnet restore "Microservicios/Invoice/Invoice.csproj"
 
# Copy everything else
COPY . .
 
# Build the application
WORKDIR "/src/Microservicios/Invoice"
RUN dotnet build "Invoice.csproj" -c Release -o /app/build
 
# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Invoice.csproj" -c Release -o /app/publish
 
# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "Invoice.dll"]
