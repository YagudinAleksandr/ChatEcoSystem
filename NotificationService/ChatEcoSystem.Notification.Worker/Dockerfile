# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotificationService/ChatEcoSystem.Notification.Worker/ChatEcoSystem.Notification.Worker.csproj", "NotificationService/ChatEcoSystem.Notification.Worker/"]
RUN dotnet restore "./NotificationService/ChatEcoSystem.Notification.Worker/ChatEcoSystem.Notification.Worker.csproj"
COPY . .
WORKDIR "/src/NotificationService/ChatEcoSystem.Notification.Worker"
RUN dotnet build "./ChatEcoSystem.Notification.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatEcoSystem.Notification.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatEcoSystem.Notification.Worker.dll"]