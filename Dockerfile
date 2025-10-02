FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["API_Neeew.csproj", "./"]
RUN dotnet restore "./API_Neeew.csproj"
COPY . .
RUN dotnet build "API_Neeew.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Neeew.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Neeew.dll"]