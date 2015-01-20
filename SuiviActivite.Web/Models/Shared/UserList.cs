using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Shared
{
    public class UserList<T> where T : UserModel
    {
        public IList<T> Users{ get; set; }

        public UserList(IEnumerable<User> users)
        {
            this.Users = new List<T>();
            foreach (User user in users)
            {

                this.Users.Add((T)Activator.CreateInstance(typeof(T), user));
            }
        }

        public UserList()
        {}
    }
}