using EmployeeTasks.Data.Common;

namespace EmployeeTasks.Services
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models.Task;

    public class TaskService : ITaskService
    {
        private readonly IRepository repo;

        public TaskService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            var allTasks = await repo.AllReadonly<Data.Entities.Task>()
                .Where(t => t.IsActive)
                .Select(t => new TaskModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    AssigneeId = t.AssigneeId,
                    Assignee = t.Assignee
                }).ToListAsync();

            return allTasks;
        }

        public async Task Add(TaskModel model)
        {
            var taskItem = new Data.Entities.Task
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
            };

            await repo.AddAsync(taskItem);
            await repo.SaveChangesAsync();
        }

        public async Task<TaskModel> GetById(int id)
        {
            return await repo.AllReadonly<Data.Entities.Task>()
                .Where(t => t.IsActive)
                .Where(t => t.Id == id)
                .Select(t => new TaskModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    AssigneeId = t.AssigneeId,
                    Assignee = t.Assignee
                })
                .FirstAsync();
        }


        public async Task Edit(int id, TaskModel model)
        {
            var task = await repo.GetByIdAsync<Data.Entities.Task>(id);

            task.Title= model.Title;
            task.Description = model.Description;
            task.DueDate = model.DueDate;
            task.AssigneeId = model.AssigneeId;
            task.Assignee = model.Assignee;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            return await repo.AllReadonly<Data.Entities.Task>()
                .Where(t => t.IsActive)
                .AnyAsync(t => t.Id == id);
        }

        public async Task Delete(int id)
        {
            var task = await repo.GetByIdAsync<Data.Entities.Task>(id);

            task.IsActive = false;

            await repo.SaveChangesAsync();
        }
    }
}
