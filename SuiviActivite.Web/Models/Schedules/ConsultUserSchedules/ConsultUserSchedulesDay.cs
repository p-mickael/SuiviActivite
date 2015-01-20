using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultUserSchedules
{
    public class ConsultUserSchedulesDay : WorkedTimeRange
    {
        public IList<ConsultUserSchedulesSchedule> Schedules { get; set; }
        public int UserId { get; set; }

        public bool Error { get; set; }

        public ConsultUserSchedulesDay(IEnumerable<Schedule> schedules)
        {
            if (schedules.Count() > 0)
                this.UserId = schedules.FirstOrDefault().UserId;

            this.Schedules = new List<ConsultUserSchedulesSchedule>();
            foreach (Schedule schedule in schedules.OrderBy(s => s.DateLogIn))
            {
                if (this.Date == null)
                    this.Date = schedule.DateLogIn.Date;

                this.WorkedTime += (int)schedule.GetMinutes();
                this.Schedules.Add(new ConsultUserSchedulesSchedule(schedule));
            }

            this.SetWorkedTime();

            this.Error = Schedules.Where(s => s.Error).Count() > 0;
        }
    }
}