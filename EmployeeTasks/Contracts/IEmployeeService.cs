namespace EmployeeTasks.Contracts
{
    using Models.Employee;

    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetAll();

        Task Add(EmployeeModel model);
    }
}
