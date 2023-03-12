using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static EmployeeTasks.Data.Constants.Task;


namespace EmployeeTasks.Models.Task
{
    using Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        public int AssigneeId { get; set; }
        
        public Employee? Assignee { get; set; }

        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
    }
}
