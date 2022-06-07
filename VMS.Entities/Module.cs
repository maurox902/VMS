using System;
using System.Collections.Generic;

namespace VMS.Entities
{
    public partial class Module
    {
        public Module()
        {
            Seats = new HashSet<Seat>();
        }

        public int ModuleId { get; set; }
        public string Description { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
