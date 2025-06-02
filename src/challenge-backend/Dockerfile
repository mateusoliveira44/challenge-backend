FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Adpater/Driver/challenge-backend.API/challenge-backend.API.csproj", "challenge-backend.API/"]
COPY ["Core/challenge-backend.Application/challenge-backend.Application.csproj", "challenge-backend.Application/"]
COPY ["Adpater/Driven/challenge-backend.Infrastructure/challenge-backend.Infrastructure.csproj", "challenge-backend.Infrastructure/"]
COPY ["Adpater/Driven/challenge-backend.PostgreSQL/challenge-backend.PostgreSQL.csproj", "challenge-backend.PostgreSQL/"]
COPY ["Core/challenge-backend.Core/challenge-backend.Core.csproj", "challenge-backend.Core/"]
COPY ["Core/challenge-backend.Domain/challenge-backend.Domain.csproj", "challenge-backend.Domain/"]

RUN dotnet restore "challenge-backend.API/challenge-backend.API.csproj"

COPY . .

WORKDIR "Adpater/Driver/challenge-backend.API"
RUN dotnet publish "challenge-backend.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "challenge-backend.API.dll"]