using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Controllers;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Tests;

public class TasksControllerTests
{
    private AppDbContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoTasks()
    {
        var db = GetInMemoryDb();
        var controller = new TasksController(db);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var tasks = Assert.IsType<List<TaskItem>>(ok.Value);
        Assert.Empty(tasks);
    }

    [Fact]
    public async Task Create_AddsTask_AndReturnsCreated()
    {
        var db = GetInMemoryDb();
        var controller = new TasksController(db);
        var newTask = new TaskItem { Title = "Test", Description = "Desc" };

        var result = await controller.Create(newTask);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        var task = Assert.IsType<TaskItem>(created.Value);
        Assert.Equal("Test", task.Title);
    }

    [Fact]
    public async Task Delete_RemovesTask_AndReturnsNoContent()
    {
        var db = GetInMemoryDb();
        var controller = new TasksController(db);
        var task = new TaskItem { Title = "Ta bort mig" };
        db.Tasks.Add(task);
        await db.SaveChangesAsync();

        var result = await controller.Delete(task.Id);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal(0, await db.Tasks.CountAsync());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenTaskMissing()
    {
        var db = GetInMemoryDb();
        var controller = new TasksController(db);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }
}