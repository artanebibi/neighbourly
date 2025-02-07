using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class Forum
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public int CanvasId { get; set; } 
        public Canvas Canvas { get; set; } 
        public int pId { get; set; } 
        public Participant CreatedBy { get; set; } 
        public List<ForumComment> Comments { get; set; } 

        public Forum()
        {
            Comments = new List<ForumComment>();
        }
    }
}