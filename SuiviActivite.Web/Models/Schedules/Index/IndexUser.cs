using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Linq;

namespace SuiviActivite.Web.Models.Schedules.ManageSchedules
{
    public class IndexUser : UserModel
    {
        public int ThisMonthWorkedHours { get; set; }
        public int ThisMonthWorkedMinutes { get; set; }
        public bool IsWorking { get; set; }

        public IndexUser(User user)
            :base(user)
        {
            double nbWorkedMinutes = (from s in user.Schedules
                                   where s.DateLogIn.Month == DateTime.Now.Month 
                                   where s.DateLogIn.Year == DateTime.Now.Year
                                   where s.DateLogOut != null
                                   select s.GetMinutes()).Sum();


            this.ThisMonthWorkedHours = (int)(nbWorkedMinutes / 60);
            this.ThisMonthWorkedMinutes = (int)(nbWorkedMinutes % 60);

            Schedule lastSchedule = user.Schedules.OrderByDescending(s => s.DateLogIn).FirstOrDefault();

            this.IsWorking = lastSchedule != null && lastSchedule.DateLogOut == null;
        }

        public IndexUser()
        {}
    }
}