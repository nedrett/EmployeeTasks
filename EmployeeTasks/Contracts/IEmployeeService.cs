namespace EmployeeTasks.Contracts
{
    using Data.Entities;
    using Models.Employee;
    using Task = System.Threading.Tasks.Task;

    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetAll();

        Task<IEnumerable<Employee>> GetAllEmployees();
        
        Task Add(EmployeeModel model);

        Task<EmployeeModel> GetById(int id); 

        Task Edit(int id, EmployeeModel model);

        Task<bool> Exist(int id);

        Task Delete(int id);
    }
}
