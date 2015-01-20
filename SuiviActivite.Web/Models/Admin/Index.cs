using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ManageSchedules
{
    public class Index
    {
        public IList<IndexUser> Users { get; set; }

        public Index(IEnumerable<User> users)
        {
            this.Users = new List<IndexUser>();
            foreach (User user in users)
            {
                this.Users.Add(new IndexUser(user));
            }
        }

        public Index()
        {}
    }
}