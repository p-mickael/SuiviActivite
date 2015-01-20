using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Shared
{
    public abstract class UserModel
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public UserModel()
        { }

        public UserModel(User user)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }

        public virtual User GenerateDomainUser()
        {
            return new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }

        public virtual User UpdateDomainUser(User user)
        {
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;

            return user;
        }
    }
}