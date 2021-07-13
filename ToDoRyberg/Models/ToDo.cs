using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoRyberg.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Please enter a description")]
        [StringLength(250, ErrorMessage = "Please limit your description to 250 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Please enter a due date")]
        [Range(typeof(DateTime), "1/1/2020", "12/31/2122", ErrorMessage ="Please choose a date in the future within 100 years")]
        public DateTime? DueDate { get; set; }
        [Required(ErrorMessage = "Please select a category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage= "Please select a status")]
        public string StatusId { get; set; }
        public Status Status { get; set; }
        [Required(ErrorMessage ="Please select a priorty number")]
        public string PriorityId { get; set; }
        public Priority Priority { get; set; }


        public bool Overdue => Status?.Name.ToLower() == "open" && DueDate < DateTime.Today;
    }
}
