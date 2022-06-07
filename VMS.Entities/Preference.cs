using System;
using System.Collections.Generic;

namespace VMS.Entities
{
    public partial class Preference
    {
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public string Detail { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
