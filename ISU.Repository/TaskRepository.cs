using Entities.Models;
using Repository.Interfaces;

namespace Repository
{
    public class TaskRepository : RepositoryBase<TaskItem>, ITaskRepository
    {
        public TaskRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    }
}
