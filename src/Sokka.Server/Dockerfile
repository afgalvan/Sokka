FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Sokka.Server/Sokka.Server.csproj", "Sokka.Server/"]
RUN dotnet restore "Sokka.Server/Sokka.Server.csproj"
COPY /src .
WORKDIR "/src/Sokka.Server"
RUN dotnet build "Sokka.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sokka.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sokka.Server.dll"]
