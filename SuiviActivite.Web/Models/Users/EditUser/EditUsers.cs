using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Users.EditUser
{
    public class EditUser : UserModel
    {
        [Required]
        [Display(Name="Prénom")]
        public override string FirstName { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public override string LastName { get; set; }

        public int Id { get; set; }

        public EditUser(User user)
            :base(user)
        {
            this.Id = user.Id;
        }

        public EditUser()
        {}
    }
}