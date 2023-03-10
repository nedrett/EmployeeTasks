namespace EmployeeTasks.Services
{
    using Contracts;
    using Data.Entities;
    using Data.Common;
    using Microsoft.EntityFrameworkCore;
    using Models.Employee;
    using Task = System.Threading.Tasks.Task;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repo;

        public EmployeeService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAll()
        {
            var allEmployees = await repo
                .AllReadonly<Employee>()
                .Select(e => new EmployeeModel()
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    CompletedTasks = e.CompletedTasks
                }).ToListAsync();

            return allEmployees;
        }

        public async Task Add(EmployeeModel model)
        {
            var employeeItem = new Employee
            {
                Id = model.Id,
                FullName = model.FullName,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate,
                Salary = model.Salary,
                CompletedTasks = model.CompletedTasks
            };

            await repo.AddAsync(employeeItem);
            await repo.SaveChangesAsync();
        }

    }
}
