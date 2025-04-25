# 1. Use official .NET 8 SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy the only project file first to optimize the build process
COPY RenterScoreAPIv2.csproj .
RUN dotnet restore

# 3. Build and publish the application
COPY . .
RUN dotnet publish -c Release -o /app/out

# 4. Use the official .NET 8 runtime to create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 5. Copy the published application to the runtime image
COPY --from=build /app/out .

# 6. Run the application
ENTRYPOINT ["dotnet", "RenterScoreAPIv2.dll"]