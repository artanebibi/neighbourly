using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }  
        public string Description { get; set; }  
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }  
        public int CanvasId { get; set; } 
        public virtual Canvas Canvas { get; set; }
    }
}