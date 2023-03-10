namespace EmployeeTasks.Contracts
{
    using Models.Employee;

    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetAll();

        Task Add(EmployeeModel model);

        Task<EmployeeModel> GetById(int id); 

        Task Edit(int id, EmployeeModel model);

        Task<bool> Exist(int id);
    }
}
