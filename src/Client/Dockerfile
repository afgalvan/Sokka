FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Sokka.Client/Sokka.Client.csproj", "Sokka.Client/"]
RUN dotnet restore "Sokka.Client/Sokka.Client.csproj"
COPY /src .
WORKDIR "/src/Sokka.Client"
RUN dotnet build "Sokka.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sokka.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sokka.Client.dll"]
