FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app


# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore FlagSense.sln

# Build
RUN echo "Building FlagService API..."
RUN dotnet publish ./FlagSense.FlagService.Api -c Release -o out

# Build runtime image
RUN echo "Building FlagService Runtime..."
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "FlagSense.FlagService.Api.dll" ]