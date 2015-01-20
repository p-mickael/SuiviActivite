using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultSchedules
{
    public class ConsultSchedulesUser : UserModel
    {
        public int Id { get; set; }
        public bool HasSchedules { get; set; }

        public ConsultSchedulesUser(User user)
            :base(user)
        {
            this.Id = user.Id;
            this.HasSchedules = user.Schedules != null && user.Schedules.Count > 0;
        }

        public ConsultSchedulesUser()
        {}
    }
}