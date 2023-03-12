namespace EmployeeTasks.Controllers
{
    using Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Employee;

    [AllowAnonymous]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<EmployeeModel> allEmployees = await employeeService.GetAll();

            return View(allEmployees);
        }

        public async Task<IActionResult> Top5()
        {
            IEnumerable<EmployeeModel> top5Employees = await employeeService.GetTop5Employees();

            return View(top5Employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EmployeeModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await employeeService.Add(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await employeeService.Exist(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var employee = await employeeService.GetById(id);

            var model = new EmployeeModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                BirthDate = employee.BirthDate,
                Salary = employee.Salary,
                CompletedTasksCount = employee.CompletedTasksCount
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await employeeService.Edit(model.Id, model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await employeeService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
