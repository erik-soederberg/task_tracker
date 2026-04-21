# Task Tracker

A fullstack task management application built with C# / ASP.NET Core, PostgreSQL and a basic HTML/JS frontend.

## Live

- **Frontend:** https://erik-soederberg.github.io/task_tracker/
- **API:** https://task-tracker-vooz.onrender.com/api/tasks

## Tech stack

- C# / ASP.NET Core 9
- PostgreSQL
- Entity Framework Core
- Docker
- GitHub Actions (CI/CD)
- Render (API hosting)
- GitHub Pages (frontend hosting)

## CI/CD

Every push to main triggers a GitHub Actions pipeline that runs in two stages:

1. **Test** — runs all unit tests. If any test fails, the pipeline stops and nothing is deployed.
2. **Build** — if all tests pass, a Docker image is built to verify the application compiles correctly.

Render is connected to the GitHub repository and automatically deploys the latest version on every push to main. The frontend is hosted on GitHub Pages and communicates with the API on Render.

## Test the Endpoints without frontend

Use a bash terminal and copy + paste the following curl commands:

- `GET /api/tasks` — get all tasks
  curl https://task-tracker-vooz.onrender.com/api/tasks

- `GET /api/tasks/{id}` — get a specific task
  curl https://task-tracker-vooz.onrender.com/api/tasks/{ID}

- `POST /api/tasks` — create a task
  curl -X POST https://task-tracker-vooz.onrender.com/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"title": "Test my API", "description": "This is a test"}'

- `PUT /api/tasks/{id}` — update a task
  curl -X PUT https://task-tracker-vooz.onrender.com/api/tasks/{ID} \
  -H "Content-Type: application/json" \
  -d '{"title": "Updated title", "description": "New description", "isCompleted": true}'

- `DELETE /api/tasks/{id}` — remove a task
  curl -X DELETE https://task-tracker-vooz.onrender.com/api/tasks/{ID}
