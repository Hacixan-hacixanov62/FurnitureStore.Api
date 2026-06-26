using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interface;
using Service.DTO.Admin.Task;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IMapper _mapper;
        private readonly ISendEmail _send;
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
             
        public TaskService(ITaskRepository repo, IMapper mapper, ISendEmail send, 
                           IAccountService accountService, 
                           UserManager<AppUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _send = send;
            _accountService = accountService;
            _userManager = userManager;
            
        }

        public async Task CreateTaskAsync(CreateTaskDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.AssignedTo);
            if (user == null)
                throw new Exception("User not found.");

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
                throw new Exception("This user cannot be assigned a task. Only those with the Admin role are allowed to do so.");
       
            var task = _mapper.Map<TaskItem>(dto);
            await _repo.AddTaskAsync(task);
        }
        public async Task<List<TaskResponseDto>> GetTasksAsync(string userName)
        {
            var tasks = await _repo.GetTasksByUserAsync(userName);
            return _mapper.Map<List<TaskResponseDto>>(tasks);
        }
        public async Task CompleteTaskAsync(CompleteTaskDto dto)
        {
            var task = await _repo.GetTaskByIdAsync(dto.TaskId);
            if (task == null)
                throw new Exception("Task not found.");

            task.IsCompleted = true;
            await _repo.UpdateTaskAsync(task);

            var allUsers = await _accountService.GetAllUsersAsync();
            var superadminEmails = allUsers
                .Where(u => u.Roles.Contains("SuperAdmin"))
                .Select(u => u.Email)
                .ToList();

            if (!superadminEmails.Any())
                throw new Exception("No SuperAdmins found.");

            string from = "aitajjf2@gmail.com";
            string displayName = "JoiFurn - Task Manager Notification";
            string subject = $"Task Completed: {task.Title}";
            string messageBody = $"Admin ({dto.CompletedBy}) completed the following task:<br/><br/>" +
                                 $"<strong>Title:</strong> {task.Title}<br/>" +
                                 $"<strong>Description:</strong> {task.Description}<br/>" +
                                 $"<strong>Created Date:</strong> {DateTime.UtcNow}";

            foreach (var to in superadminEmails)
            {
                await _send.SendAsync(from, displayName, to, messageBody, subject);
            }
        }

        public async Task MarkTaskAsSeenAsync(int taskId, string seenBy)
        {
            var task = await _repo.GetTaskByIdAsync(taskId);
            if (task == null)
                throw new Exception("Task not found.");
            await _repo.UpdateTaskAsync(task);

            var allUsers = await _accountService.GetAllUsersAsync();
            var superadminEmails = allUsers
                .Where(u => u.Roles.Contains("SuperAdmin"))
                .Select(u => u.Email)
                .ToList();

            if (!superadminEmails.Any())
                throw new Exception("No SuperAdmins found.");

            string from = "aitajjf2@gmail.com";
            string displayName = "JoiFurn - Task Manager Notification";
            string subject = $"Task Viewed: {task.Title}";
            string messageBody = $"Admin ({seenBy}) viewed the following task:<br/><br/>" +
                                 $"<strong>Title:</strong> {task.Title}<br/>" +
                                 $"<strong>Description:</strong> {task.Description}<br/>" +
                                 $"<strong>Viewed At:</strong> {DateTime.UtcNow}";

            foreach (var to in superadminEmails)
            {
                await _send.SendAsync(from, displayName, to, messageBody, subject);
            }
        }

        public async Task<List<TaskResponseDto>> GetAllAsync()
        {
            var tasks = await _repo.GetAllAsync();
            return _mapper.Map<List<TaskResponseDto>>(tasks);
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _repo.GetTaskByIdAsync(taskId);
            if (task == null)
                throw new Exception("Task not found.");

            await _repo.DeleteAsync(task);
        }

    }
}
