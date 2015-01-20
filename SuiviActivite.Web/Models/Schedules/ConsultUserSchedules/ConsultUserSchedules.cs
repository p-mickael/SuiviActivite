using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultUserSchedules
{
    public class ConsultUserSchedules
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MonthToJumpTo { get; set; }

        public IList<ConsultUserSchedulesMonth> Schedules { get; set; }

        public ConsultUserSchedules(User user)
        {
            if (user != null)
            {
                this.FirstName = user.FirstName;
                this.LastName = user.LastName;

                this.Schedules = new List<ConsultUserSchedulesMonth>();

                if (user.Schedules != null)
                {
                    foreach (var group in user.Schedules.OrderByDescending(s => s.DateLogIn).GroupBy(s => new { s.DateLogIn.Month, s.DateLogIn.Year }))
                    {
                        this.Schedules.Add(new ConsultUserSchedulesMonth(group));
                    }
                }
            }
        }
    }
}