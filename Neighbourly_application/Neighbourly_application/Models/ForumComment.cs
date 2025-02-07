using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class ForumComment
    {
        [Key]
        public int Id { get; set; } 
        public string Content { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public int tId { get; set; } 
        public virtual Forum Thread { get; set; }
        public int? ParentCommentId { get; set; } 
//        public virtual ForumComment ParentComment { get; set; } 
        public virtual List<String> Replies { get; set; } 
        public int commentCreatedById { get; set; }
        public virtual Participant commentCreatedBy { get; set; } 

        public ForumComment()
        {
            Replies = new List<String>();
        }
    }
}