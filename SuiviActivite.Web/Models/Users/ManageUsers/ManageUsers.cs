using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System.Collections.Generic;

namespace SuiviActivite.Web.Models.Users.ManageUsers
{
    public class ManageUsers : UserList<ManageUsersUser>
    {
        public ManageUsers(IEnumerable<User> users)
            :base(users)
        {}
    }
}