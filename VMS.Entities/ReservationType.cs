using System;
using System.Collections.Generic;

namespace VMS.Entities
{
    public partial class ReservationType
    {
        public ReservationType()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int ReservationTypeId { get; set; }
        public string Description { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
