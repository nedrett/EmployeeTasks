namespace EmployeeTasks.Services
{
    using Contracts;
    using Data;
    using Data.Entities;
    using Data.Common;
    using Microsoft.EntityFrameworkCore;
    using Models.Employee;
    using Task = System.Threading.Tasks.Task;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repo;

        public EmployeeService(IRepository _repo, ApplicationDbContext _data)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAll()
        {
            var allEmployees = await repo.AllReadonly<Employee>()
                .Where(e => e.IsActive)
                .Select(e => new EmployeeModel
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    CompletedTasks = new List<Data.Entities.Task>()
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
