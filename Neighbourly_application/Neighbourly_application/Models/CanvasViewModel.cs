using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neighbourly_application.Models
{
    public class CanvasViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamLeaderId { get; set; }
        public List<int> SelectedParticipants { get; set; }

        public CanvasViewModel()
        {
            SelectedParticipants = new List<int>();
        }

    }
}