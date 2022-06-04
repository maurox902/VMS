using System.ComponentModel.DataAnnotations;

namespace VMSRazorTest.Models
{
    public class Role
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Display(Name = "RolID")]
        public int role_id { get; set; }

        [Display(Name = "Rol")]
        public string description { get; set; }

    }
}
