# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the backend project files to the container
COPY ["Api/Api.csproj", "Api/"]

# Restore dependencies (use the dotnet restore command)
RUN dotnet restore "Api/Api.csproj"

# Copy all the remaining files
COPY . .

# Build the project
WORKDIR /src/Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Expose the port the app will listen on
EXPOSE 5277

# Start the app
ENTRYPOINT ["dotnet", "Api.dll"]
