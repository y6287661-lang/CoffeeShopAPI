FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API_Neeev/API_Neeev.csproj", "API_Neeev/"]
RUN dotnet restore "API_Neeev/API_Neeev.csproj"
COPY . .
WORKDIR "/src/API_Neeev"
RUN dotnet build "API_Neeev.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Neeev.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Neeev.dll"]