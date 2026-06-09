# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["FinanceToolkitApi.csproj", "./"]
RUN dotnet restore "FinanceToolkitApi.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "FinanceToolkitApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "FinanceToolkitApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5125
ENV ASPNETCORE_HTTP_PORTS=5125

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceToolkitApi.dll"]
