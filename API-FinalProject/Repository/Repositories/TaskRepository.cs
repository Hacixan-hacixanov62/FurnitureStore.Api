using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }
        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TaskItem>> GetTasksByUserAsync(string userName) =>
            await _context.TaskItems.Where(t => t.AssignedTo == userName).ToListAsync();


        public async Task<TaskItem> GetTaskByIdAsync(int id) =>
            await _context.TaskItems.FindAsync(id);

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
