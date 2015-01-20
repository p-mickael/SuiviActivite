using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultUserSchedules
{
    public class ConsultUserSchedulesMonth : WorkedTimeRange
    {
        public IList<ConsultUserSchedulesDay> Days { get; set; }

        public ConsultUserSchedulesMonth(IEnumerable<Schedule> schedules)
        {
            this.Days = new List<ConsultUserSchedulesDay>();
            foreach (var group in schedules.GroupBy(s => s.DateLogIn.Date))
            {
                ConsultUserSchedulesDay day = new ConsultUserSchedulesDay(group);

                if (this.Date == null)
                    this.Date = day.Date;

                this.WorkedTime += day.WorkedTime;
                this.Days.Add(day);
            }

            this.SetWorkedTime();
        }

        #region Gestion des semaines
        //public IList<ConsultUserSchedulesWeek> Weeks { get; set; }

        //public ConsultUserSchedulesMonth(IEnumerable<Schedule> schedules)
        //{
        //    this.Weeks = new List<ConsultUserSchedulesWeek>();
        //    foreach (var group in schedules.GroupBy(s => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(s.DateLogIn, CalendarWeekRule.FirstDay, DayOfWeek.Monday)))
        //    {
        //        ConsultUserSchedulesWeek week = new ConsultUserSchedulesWeek(group);
        //        this.WorkedTime += week.WorkedTime;
        //        this.Weeks.Add(week);
        //    }

        //    this.SetWorkedTime();
        //}
        #endregion
    }
}