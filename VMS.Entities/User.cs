using System;
using System.Collections.Generic;

namespace VMS.Entities
{
    public partial class User
    {
        public User()
        {
            Preferences = new HashSet<Preference>();
            Reservations = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Preference> Preferences { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
