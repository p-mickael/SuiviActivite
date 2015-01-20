using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultUserSchedules
{
    public class ConsultUserSchedulesWeek : WorkedTimeRange
    {
        public IList<ConsultUserSchedulesDay> Days { get; set; }

        public ConsultUserSchedulesWeek(IEnumerable<Schedule> schedules)
        {
            this.Days = new List<ConsultUserSchedulesDay>();
            foreach(var group in schedules.GroupBy(s => s.DateLogIn.Date))
            {
                ConsultUserSchedulesDay day = new ConsultUserSchedulesDay(group);
                this.WorkedTime += day.WorkedTime;
                this.Days.Add(day);
            }

            this.SetWorkedTime();
        }
    }
}