namespace EmployeeTasks.Controllers
{
    using Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Task;

    [AllowAnonymous]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService _taskService)
        {
            taskService = _taskService;
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
        public IActionResult Add()
        {
            var model = new TaskModel();

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
    }
}
