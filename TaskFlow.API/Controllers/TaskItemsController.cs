using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor
        public TaskItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItems
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<TaskItemResponse>>> GetTaskItems()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var taskItems = await _context.TaskItems
                .Where(t => t.UserId == userId)
                .Select(t => new TaskItemResponse
                {
                    TaskItemId = t.TaskItemId,
                    Title = t.Title,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                })
                .ToListAsync();

            return Ok(taskItems);
        }

        // GET: api/TaskItem/2
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemResponse>> GetTaskItem(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.TaskItemId == id && t.UserId == userId);

            if (taskItem == null)
            {
                return NotFound();
            }

            var response = new TaskItemResponse
            {
                TaskItemId = taskItem.TaskItemId,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted,
                CreatedAt = taskItem.CreatedAt,
                UpdatedAt = taskItem.UpdatedAt
            };

            return Ok(response);
        }

        // POST: api/TaskItems
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskItemResponse>> AddTaskItem(CreateTaskItemRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var taskItem = new TaskItem
            {
                UserId = userId,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                CreatedAt = DateTime.UtcNow
            };

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            var response = new TaskItemResponse
            {
                TaskItemId = taskItem.TaskItemId,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted,
                CreatedAt = taskItem.CreatedAt
            };

            return CreatedAtAction(nameof(GetTaskItem),
                new { id = taskItem.TaskItemId },
                response);
        }

        // PUT: api/TaskItems/2
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemResponse>> UpdateTaskItem(int id, UpdateTaskItemRequest request)
        {
            // Get user claim of logged in user, convert to int if not null
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim);

            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.TaskItemId == id && t.UserId == userId);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.IsCompleted = request.IsCompleted;
            taskItem.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var response = new TaskItemResponse
            {
                TaskItemId = taskItem.TaskItemId,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted,
                CreatedAt = taskItem.CreatedAt,
                UpdatedAt = taskItem.UpdatedAt
            };

            return Ok(response);
        }

        // DELETE: api/TaskItems/2
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            // Get user claim of logged in user, convert to int if not null
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();
            var userId = int.Parse(userIdClaim);

            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(t => t.TaskItemId == id && t.UserId == userId);

            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
