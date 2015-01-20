using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace SuiviActivite.Web.Models.Users.AddUser
{
    public class AddUser : UserModel
    {
        [Required]
        [Display(Name="Prénom")]
        public override string FirstName { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public override string LastName { get; set; }

        public AddUser(User user)
            :base(user)
        {

        }

        public AddUser()
        {

        }
    }
}