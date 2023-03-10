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

            if (allEmployees == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(allEmployees);
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

            return RedirectToAction(nameof(Add));
        }
    }
}
