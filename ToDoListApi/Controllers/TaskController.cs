using Microsoft.AspNetCore.Mvc;
using ToDoListApi.DTOs;
using ToDoListApi.Models;
using ToDoListApi.Services.ToDo;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении задач");
                return StatusCode(500, "Произошла внутренняя ошибка.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                    return NotFound($"Задача с id = {id} не найдена.");

                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении задачи по id = {Id}", id);
                return StatusCode(500, "Произошла внутренняя ошибка.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskDto task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var taskItem = new TaskItem
                {
                    Title = task.Title,
                    IsDone = task.IsDone
                };

                var createdTask = await _taskService.CreateAsync(taskItem);
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании задачи");
                return StatusCode(500, "Произошла внутренняя ошибка.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var deleted = await _taskService.DeleteAsync(id);
                if (!deleted)
                    return NotFound($"Задача с id = {id} не найдена.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении задачи с id = {Id}", id);
                return StatusCode(500, "Произошла внутренняя ошибка.");
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTaskStatusDto dto)
        {
            try
            {
                var result = await _taskService.UpdateStatusAsync(id, dto.IsDone);
                if (!result)
                    return NotFound($"Задача с id = {id} не найдена.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении статуса задачи с id = {Id}", id);
                return StatusCode(500, "Произошла внутренняя ошибка.");
            }
        }
    }
}
