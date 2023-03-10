namespace EmployeeTasks.Services
{
    using Contracts;
    using Data.Common;
    using Data.Entities;
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
            return await repo.AllReadonly<Employee>()
                .Where(e => e.IsActive)
                .Select(e => new EmployeeModel
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    CompletedTasksCount = e.CompletedTasksCount
                }).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeModel>> GetTop5Employees()
        {
            return await repo.AllReadonly<Employee>()
                .Where(e => e.IsActive)
                .Select(e => new EmployeeModel
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    CompletedTasksCount = e.CompletedTasksCount
                })
                .OrderByDescending(e => e.CompletedTasksCount)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await repo.AllReadonly<Employee>()
                .Where(e => e.IsActive)
                .ToListAsync();
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
                CompletedTasksCount = model.CompletedTasksCount
            };

            await repo.AddAsync(employeeItem);
            await repo.SaveChangesAsync();
        }

        public async Task<EmployeeModel> GetById(int id)
        {
            return await repo.AllReadonly<Employee>()
                .Where(e => e.IsActive)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeModel
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    CompletedTasksCount = e.CompletedTasksCount
                })
                .FirstAsync();
        }


        public async Task Edit(int id, EmployeeModel model)
        {
            var employee = await repo.GetByIdAsync<Employee>(id);

            employee.FullName = model.FullName;
            employee.EmailAddress = model.EmailAddress;
            employee.PhoneNumber = model.PhoneNumber;
            employee.BirthDate = model.BirthDate;
            employee.Salary = model.Salary;
            employee.CompletedTasksCount = model.CompletedTasksCount;

            await repo.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            return await repo.AllReadonly<Employee>()
                .Where(e => e.IsActive)
                .AnyAsync(e => e.Id == id);
        }

        public async Task Delete(int id)
        {
            var employee = await repo.GetByIdAsync<Employee>(id);

            employee.IsActive = false;

            await repo.SaveChangesAsync();
        }
    }
}
