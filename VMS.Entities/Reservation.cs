using System;
using System.Collections.Generic;

namespace VMS.Entities
{
    public partial class Reservation
    {
        public int ReservactionId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ScheduleId { get; set; }
        public int SeatId { get; set; }
        public int ReservationTypeId { get; set; }
        public string Detail { get; set; } = null!;

        public virtual ReservationType ReservationType { get; set; } = null!;
        public virtual Schedule Schedule { get; set; } = null!;
        public virtual Seat Seat { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
