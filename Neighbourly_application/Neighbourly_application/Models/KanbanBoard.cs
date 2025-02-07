using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class KanbanBoard
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; } 
        public int CanvasId { get; set; }
        public virtual Canvas Canvas { get; set; } 
        public virtual List<KanbanColumn> Columns { get; set; } 

        public KanbanBoard()
        {
            Columns = new List<KanbanColumn>();
        }
    }
}