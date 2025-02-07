using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{


    public class KanbanColumn
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Position { get; set; } 
        public int KanbanBoardId { get; set; } 
        public virtual KanbanBoard KanbanBoard { get; set; }
        public virtual List<KanbanTask> Tasks { get; set; } 

        public KanbanColumn()
        {
            Tasks = new List<KanbanTask>();
        }
    }



}