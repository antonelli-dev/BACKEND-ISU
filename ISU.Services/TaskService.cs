using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync() => await _taskRepository.FindAll().ToListAsync();

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.FindByCondition(t => t.TaskId == id).FirstOrDefaultAsync();
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            _taskRepository.Create(task);
            await _taskRepository.SaveAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _taskRepository.Update(task);
            await _taskRepository.SaveAsync();
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _taskRepository.Delete(task);
            await _taskRepository.SaveAsync();
        }
    }
}
