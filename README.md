# ToDoListApi
API для управления задачами (todo list) на ASP.NET Core и PostgreSQL (EF Core)

## Запуск проекта
1. Настройте строку подключения в `appsettings.json` (Изменить имя пользователя и пароль под свои настройки)
2. Создайте базу данных: `todo_db`
3. Обновите базу данных:
   `dotnet ef database update`
4. Запустите проект:
   dotnet run
   
## Эндпоинты проекта
- `POST /api/tasks` — создать задачу
- `GET /api/tasks` — получить все задачи
- `DELETE /api/tasks/{id}` — удалить задачу
- `PATCH /api/Tasks/{id}/status` - обновить статус задачи
- `GET /api/Tasks/{id}` - получить задачу по его id
