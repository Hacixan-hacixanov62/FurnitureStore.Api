using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Interface;
using Service.DTO.Admin.Task;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class TasksController : BaseController
    {
        private readonly ITaskService _service;
        private readonly IEmailService _emailService;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskService service, IEmailService emailService, ITaskRepository taskRepository) {
            _service = service;
            _emailService = emailService;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
                await _service.CreateTaskAsync(dto);
                return Ok("Task created");            
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetByUser([FromRoute] string userName)
        {
            var tasks = await _service.GetTasksAsync(userName);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Complete([FromBody] CompleteTaskDto dto)
        {
            await _service.CompleteTaskAsync(dto);
            return Ok("Task marked as complete and email sent");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> MarkSeen([FromRoute]int id, [FromBody] MarkSeenDto dto)
        {
            if (id != dto.TaskId)
                return BadRequest("Task ID mismatch");

            try
            {
                await _service.MarkTaskAsSeenAsync(dto.TaskId, dto.SeenBy);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          var tasks = await _service.GetAllAsync();
          return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
                await _service.DeleteTaskAsync(id);
                return NoContent();         }
    }
}
