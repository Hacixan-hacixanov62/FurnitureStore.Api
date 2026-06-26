using Domain.Entities;

namespace Repository.Repositories.Interface
{
    public interface ITaskRepository : IBaseRepository<TaskItem>
    {
        Task AddTaskAsync(TaskItem task);
        Task<List<TaskItem>> GetTasksByUserAsync(string userName);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(TaskItem task);
    }
}
