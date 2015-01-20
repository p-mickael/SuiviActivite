using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultSchedules
{
    public class ConsultSchedules : UserList<ConsultSchedulesUser>
    {
        public ConsultSchedules(IEnumerable<User> users)
            :base(users)
        {}

        public ConsultSchedules()
        {}
    }
}