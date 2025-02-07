using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class CanvasParticipant
    {
        [Key]
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }

        public int CanvasId { get; set; }
        public virtual Canvas Canvas { get; set; }

    }
}