
using Service.DTO.Admin.Task;

namespace Service.Services.Interfaces
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskDto dto);
        Task<List<TaskResponseDto>> GetTasksAsync(string userId);
        Task CompleteTaskAsync(CompleteTaskDto dto);
        Task MarkTaskAsSeenAsync(int taskId, string seenBy);
        Task<List<TaskResponseDto>> GetAllAsync();
        Task DeleteTaskAsync(int taskId);
    }
}
