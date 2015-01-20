using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Home.Index
{
    public class Index : UserList<IndexUser>
    {
        public Index(IEnumerable<User> users)
            :base(users)
        {}

        public Index()
        {}
    }
}