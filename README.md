# ChatEcoSystem

Микросервисная система чата с веб-интерфейсом в стиле Telegram, построенная на .NET Core 3.1 с использованием API Gateway (Ocelot) и SignalR для real-time коммуникации.

## 📋 Техническое задание

### Цель проекта
Создание полнофункциональной системы чата с поддержкой:
- Регистрации и авторизации пользователей
- Создания личных и групповых чатов
- Real-time обмена сообщениями
- Редактирования и удаления сообщений
- Уведомлений о новых сообщениях
- Отслеживания статуса пользователей (онлайн/оффлайн)

### Функциональные требования
1. **Аутентификация и авторизация**
   - Регистрация пользователей с email и паролем
   - JWT-токен авторизация
   - Профиль пользователя

2. **Чат функциональность**
   - Создание личных и групповых чатов
   - Отправка текстовых сообщений
   - Редактирование и удаление сообщений
   - История сообщений
   - Real-time обновления через WebSocket

3. **Уведомления**
   - Email уведомления о новых сообщениях
   - Push-уведомления в браузере

4. **Пользовательский интерфейс**
   - Адаптивный веб-интерфейс в стиле Telegram
   - Список чатов с аватарами
   - Чат-комната с историей сообщений
   - Индикаторы онлайн/оффлайн статуса

### Технические требования
- Микросервисная архитектура
- API Gateway для маршрутизации
- Real-time коммуникация через SignalR
- База данных SQL Server
- Docker контейнеризация
- CORS поддержка

## 🏗️ Архитектура проекта

### Структура решения
```
ChatEcoSystem/
├── AuthService/                    # Сервис аутентификации
│   ├── ChatEcoSystem.Auth.Api/     # Web API для аутентификации
│   └── ChatEcoSystem.Auth.Logic/   # Бизнес-логика и данные
├── ChatService/                    # Сервис чата
│   ├── ChatEcoSystem.Chat.Api/     # Web API для чата
│   └── ChatEcoSystem.Chat.Logic/   # Бизнес-логика и SignalR
├── NotificationService/            # Сервис уведомлений
│   ├── ChatEcoSystem.Notification.Logic/    # Логика уведомлений
│   └── ChatEcoSystem.Notification.Worker/   # Фоновый сервис
├── Gateway/                        # API Gateway (Ocelot)
│   └── ChatEcoSystem.Gateway/      # Маршрутизация запросов
├── UI/                            # Веб-интерфейс
│   └── ChatEcoSystem.WebApp/       # Razor Pages приложение
├── SharedLib/                     # Общие библиотеки
│   ├── ChatEcoSystem.SharedLib.Abstractions/  # Интерфейсы
│   └── ChatEcoSystem.SharedLib.Contracts/     # DTO и контракты
├── configs/                       # Конфигурационные файлы
│   └── ocelot.json               # Маршруты API Gateway
└── docker-compose.yml            # Docker Compose конфигурация
```

### Описание сервисов

#### 🔐 AuthService
**Назначение:** Управление пользователями и аутентификацией
- **API Endpoints:**
  - `POST /auth/login` - Вход пользователя
  - `POST /users/create` - Регистрация пользователя
  - `GET /users/getbyid` - Получение профиля пользователя
- **Функции:**
  - JWT токен генерация и валидация
  - Хеширование паролей
  - Управление пользователями в БД

#### 💬 ChatService
**Назначение:** Управление чатами и сообщениями
- **API Endpoints:**
  - `GET /chatrooms/getuserchatrooms` - Список чатов пользователя
  - `GET /messages/getchathistory` - История сообщений чата
  - `POST /chatrooms/create` - Создание чата
- **SignalR Hub:**
  - `/chatHub` - Real-time коммуникация
  - Методы: `SendMessage`, `EditMessage`, `DeleteMessage`, `JoinChat`
- **Функции:**
  - Управление чат-комнатами
  - Обработка сообщений
  - Real-time уведомления

#### 📧 NotificationService
**Назначение:** Отправка уведомлений
- **Функции:**
  - Email уведомления через SMTP
  - Обработка очереди уведомлений
  - Отслеживание непрочитанных сообщений

#### 🌐 Gateway
**Назначение:** API Gateway для маршрутизации запросов
- **Технология:** Ocelot
- **Функции:**
  - Маршрутизация HTTP запросов к микросервисам
  - WebSocket прокси для SignalR
  - CORS поддержка
  - Балансировка нагрузки

#### 🖥️ WebApp
**Назначение:** Веб-интерфейс пользователя
- **Технология:** ASP.NET Core Razor Pages
- **Функции:**
  - Страницы входа и регистрации
  - Список чатов
  - Чат-комната с real-time сообщениями
  - Профиль пользователя
  - Telegram-подобный UI

## 🔧 Конфигурационные файлы (.env)

### auth-service.env
```env
# Настройки базы данных
ConnectionStrings__DefaultConnection=Server=localhost,1433;Database=ChatEcoSystem_Auth;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true

# JWT настройки
Jwt__SecretKey=your-super-secret-key-with-at-least-32-characters
Jwt__Issuer=ChatEcoSystem
Jwt__Audience=ChatEcoSystem
Jwt__ExpirationMinutes=60

# Настройки приложения
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:5000
```

### chat-service.env
```env
# Настройки базы данных
ConnectionStrings__DefaultConnection=Server=localhost,1433;Database=ChatEcoSystem_Chat;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true

# Настройки приложения
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:5002

# SignalR настройки
SignalR__EnableDetailedErrors=true
```

### notification-service.env
```env
# Настройки базы данных
ConnectionStrings__DefaultConnection=Server=localhost,1433;Database=ChatEcoSystem_Notifications;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true

# SMTP настройки
Smtp__Host=smtp.gmail.com
Smtp__Port=587
Smtp__Username=your-email@gmail.com
Smtp__Password=your-app-password
Smtp__EnableSsl=true
Smtp__FromEmail=your-email@gmail.com
Smtp__FromName=ChatEcoSystem

# Настройки приложения
ASPNETCORE_ENVIRONMENT=Development
```

### gateway.env
```env
# Настройки Gateway
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8081

# CORS настройки
Cors__AllowedOrigins=http://localhost:8080,http://localhost:3000
```

### webapp.env
```env
# Настройки WebApp
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080

# API Gateway URL
ApiGateway__BaseUrl=http://localhost:8081
```

### mssql.env
```env
# SQL Server настройки
ACCEPT_EULA=Y
SA_PASSWORD=YourStrong@Passw0rd
MSSQL_PID=Developer
```

## 🚀 Запуск окружения

### Предварительные требования
- Docker Desktop
- .NET Core 3.1 SDK
- Git

### Пошаговая инструкция

#### 1. Клонирование репозитория
```bash
git clone <repository-url>
cd ChatEcoSystem
```

#### 2. Настройка переменных окружения
Скопируйте и настройте .env файлы:
```bash
cp auth-service.env.example auth-service.env
cp chat-service.env.example chat-service.env
cp notification-service.env.example notification-service.env
cp gateway.env.example gateway.env
cp webapp.env.example webapp.env
cp mssql.env.example mssql.env
```

Отредактируйте файлы, указав:
- Пароли для SQL Server
- JWT секретный ключ
- SMTP настройки для уведомлений

#### 3. Запуск через Docker Compose
```bash
# Запуск всех сервисов
docker-compose up -d

# Проверка статуса
docker-compose ps

# Просмотр логов
docker-compose logs -f
```

#### 4. Инициализация баз данных
Базы данных создаются автоматически при первом запуске сервисов благодаря Entity Framework migrations.

#### 5. Доступ к приложению
- **WebApp:** http://localhost:8080
- **API Gateway:** http://localhost:8081
- **AuthService:** http://localhost:5000
- **ChatService:** http://localhost:5002
- **SQL Server:** localhost,1433

### Альтернативный запуск (локальная разработка)

#### 1. Запуск SQL Server
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 --name sql1 --hostname sql1 \
  -d mcr.microsoft.com/mssql/server:2019-latest
```

#### 2. Запуск сервисов по отдельности
```bash
# AuthService
cd AuthService/ChatEcoSystem.Auth.Api
dotnet run

# ChatService  
cd ChatService/ChatEcoSystem.Chat.Api
dotnet run

# Gateway
cd Gateway/ChatEcoSystem.Gateway
dotnet run

# WebApp
cd UI/ChatEcoSystem.WebApp
dotnet run
```

## 🔍 Тестирование

### Регистрация пользователя
1. Откройте http://localhost:8080
2. Перейдите на страницу регистрации
3. Заполните форму: Email, Имя, Пароль
4. Нажмите "Зарегистрироваться"

### Вход в систему
1. На странице входа введите Email и Пароль
2. После успешного входа вы попадете в список чатов

### Создание чата
1. В списке чатов нажмите "Создать чат"
2. Выберите участников
3. Начните обмен сообщениями

## 🛠️ Разработка

### Добавление нового API endpoint
1. Создайте контроллер в соответствующем сервисе
2. Добавьте маршрут в `configs/ocelot.json`
3. Обновите клиентский код в WebApp

### Изменение DTO
1. Отредактируйте файлы в `SharedLib/ChatEcoSystem.SharedLib.Contracts`
2. Обновите AutoMapper профили
3. Измените клиентский код для соответствия новым DTO

### Добавление нового сервиса
1. Создайте новый проект в соответствующей папке
2. Добавьте сервис в `docker-compose.yml`
3. Настройте маршруты в `ocelot.json`
4. Создайте .env файл для конфигурации

## 📝 Логи и отладка

### Просмотр логов
```bash
# Все сервисы
docker-compose logs

# Конкретный сервис
docker-compose logs auth-service
docker-compose logs chat-service
docker-compose logs gateway
docker-compose logs webapp
```

### Отладка в Visual Studio
1. Откройте решение `ChatEcoSystem.sln`
2. Установите точки останова в коде
3. Запустите проект в режиме отладки

