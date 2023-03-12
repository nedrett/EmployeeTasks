namespace EmployeeTasks.Controllers
{
    using Contracts;
    using Data.Common;
    using Data.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Task;

    [AllowAnonymous]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;
        private readonly IEmployeeService employeeService;

        public TaskController(
            ITaskService _taskService,
            IEmployeeService _employeeService)
        {
            taskService = _taskService;
            employeeService = _employeeService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<TaskModel> allTasks = await taskService.GetAll();

            if (allTasks == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(allTasks);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new TaskModel();

            IEnumerable<Employee> employees = await employeeService.GetAllEmployees();

            foreach (var employee in employees)
            {
                model.Employees.Add(new SelectListItem { Text = employee.FullName, Value = employee.Id.ToString()});
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await taskService.Add(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await taskService.Exist(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var task = await taskService.GetById(id);

            var model = new TaskModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                AssigneeId = task.AssigneeId,
                Assignee = task.Assignee,
            };

            IEnumerable<Employee> employees = await employeeService.GetAllEmployees();

            foreach (var employee in employees)
            {
                model.Employees.Add(new SelectListItem { Text = employee.FullName, Value = employee.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await taskService.Edit(model.Id, model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await taskService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> TaskDone(int id)
        {
            var taskDone = await taskService.GetTaskById(id);

            var assignedEmployee = await employeeService.GetById(taskDone.AssigneeId);

            assignedEmployee.CompletedTasksCount++;

            await employeeService.Edit(assignedEmployee.Id, assignedEmployee);

            await Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
