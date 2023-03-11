namespace EmployeeTasks.Contracts
{
    using Models.Task;

    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAll();

        Task Add(TaskModel model);

        Task<TaskModel> GetById(int id);

        Task Edit(int id, TaskModel model);

        Task<bool> Exist(int id);

        Task Delete(int id);
    }
}
