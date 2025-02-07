using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class Participant
    {
       [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<CanvasParticipant> CParticipants { get; set; } 
        public virtual List<KanbanTask> AssignedTasks { get; set; } 
        public virtual List<Forum> CreatedThreads { get; set; } 
        public virtual List<ForumComment> CreatedComments { get; set; }

        public Participant()
        {
            CParticipants = new List<CanvasParticipant>();
            AssignedTasks = new List<KanbanTask>();
            CreatedThreads = new List<Forum>();
            CreatedComments = new List<ForumComment>();
        }

    }
}