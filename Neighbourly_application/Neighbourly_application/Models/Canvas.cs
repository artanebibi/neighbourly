using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
 //       public virtual ICollection<ChatMessage> ChatMessages { get; set; } 
 //       public virtual ICollection<Resource> Resources { get; set; } // Collection of resources
 //           ChatMessages = new List<ChatMessage>();
 //           Resources = new List<Resource>();

    public class Canvas
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamLeaderId { get; set; }
        public virtual Participant TeamLeader { get; set; }
        public virtual ICollection<CanvasParticipant> CParticipants { get; set; }
        public virtual ICollection<KanbanBoard> KanbanBoards { get; set; }
        public virtual List<Event> Events { get; set; }
        public virtual List<Forum> Forums { get; set; }

        public Canvas()
        {
            CParticipants = new List<CanvasParticipant>();
            KanbanBoards = new List<KanbanBoard>();
            Events = new List<Event>();
            Forums = new List<Forum>();
        }
    }


}