using System;
using System.Collections.Generic;

namespace VMS.Entities
{
    public partial class Seat
    {
        public Seat()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int SeatId { get; set; }
        public string Description { get; set; } = null!;
        public int ModuleId { get; set; }
        public int TypeId { get; set; }
        public bool Status { get; set; }

        public virtual Module Module { get; set; } = null!;
        public virtual SeatType Type { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
