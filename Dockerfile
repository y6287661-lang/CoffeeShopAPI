# المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API_Neeew/API_Neeew.csproj", "API_Neeew/"]
RUN dotnet restore "API_Neeew/API_Neeew.csproj"
COPY . .
WORKDIR "/src/API_Neeew"
RUN dotnet build "API_Neeew.csproj" -c Release -o /app/build

# مرحلة النشر
FROM build AS publish
RUN dotnet publish "API_Neeew.csproj" -c Release -o /app/publish /p:UseAppHost=false

# المرحلة النهائية لتشغيل التطبيق
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Neeew.dll"]