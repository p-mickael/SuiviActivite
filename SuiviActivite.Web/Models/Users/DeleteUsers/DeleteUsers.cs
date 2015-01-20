using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Users.DeleteUsers
{
    public class DeleteUsers : UserList<DeleteUsersUser>
    {
        public DeleteUsers(IEnumerable<User> users)
            :base(users)
        {}

        public DeleteUsers()
        {}
    }
}