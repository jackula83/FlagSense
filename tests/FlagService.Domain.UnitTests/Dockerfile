FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

# Copy project file and install dependencies
COPY "FlagService.Domain.UnitTests.csproj" ./
RUN dotnet restore

# Copy everything else
COPY . ./

# Run tests on build
RUN dotnet tests

# Run tests on run
ENTRYPOINT [ "dotnet", "test" ]