using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{


    public class KanbanTask
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime CreatedAt { get; set; } // Timestamp when the task was created
        public DateTime? DueDate { get; set; } // Optional due date for the task
        public int Position { get; set; } // Position of the task within the column (for ordering)
        public int KanbanColumnId { get; set; } // Foreign key to the Kanban column
        public virtual KanbanColumn KanbanColumn { get; set; } // Navigation property to the Kanban column
        public int? AssignedUserId { get; set; } // Foreign key to the user working on the task
        public virtual Participant AssignedParticipant { get; set; } // Navigation property to the user working on the task
    }



}