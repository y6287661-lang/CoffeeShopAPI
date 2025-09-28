# المرحلة الأساسية لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملف المشروع واستعادة الحزم
COPY ["API_Meeew/API_Meeew.csproj", "API_Meeew/"]
RUN dotnet restore "API_Meeew/API_Meeew.csproj"

# نسخ باقي الملفات وبناء المشروع
COPY . .
WORKDIR "/src/API_Meeew"
RUN dotnet build "API_Meeew.csproj" -c Release -o /app/build

# مرحلة النشر
FROM build AS publish
RUN dotnet publish "API_Meeew.csproj" -c Release -o /app/publish /p:UseAppHost=false

# المرحلة النهائية لتشغيل التطبيق
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Meeew.dll"]