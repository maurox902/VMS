using System.ComponentModel.DataAnnotations;

namespace VMS.Web.Models
{
    public class Users
    {

        [System.ComponentModel.DataAnnotations.Key]
        [Display(Name = "ID")]
        public int user_id { get; set; }

        [Display(Name = "Username")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [DisplayFormat(DataFormatString = "••••••••••")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Rol")]
        public int role_id { get; set; }
    }
}
