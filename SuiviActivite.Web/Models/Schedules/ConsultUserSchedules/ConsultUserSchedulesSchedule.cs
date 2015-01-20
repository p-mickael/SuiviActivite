using SuiviActivite.Business.Configuration;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultUserSchedules
{
    public class ConsultUserSchedulesSchedule
    {
        public DateTime DateLogIn { get; set; }
        public DateTime? DateLogOut { get; set; }
        public int Id { get; set; }

        public bool Error { get; set; }

        public ConsultUserSchedulesSchedule(Schedule schedule)
        {
            this.DateLogIn = schedule.DateLogIn;
            this.DateLogOut = schedule.DateLogOut;
            this.Id = schedule.Id;

            CheckErrors();
        }

        private void CheckErrors()
        {
            OfficeHours hours = ConfigManager.Instance.OfficeHours;
            this.Error =
                this.DateLogIn.TimeOfDay < hours.MorningBegin.TimeOfDay ||
                (this.DateLogIn.TimeOfDay > hours.MorningEnd.TimeOfDay &&
                this.DateLogIn.TimeOfDay < hours.AfternoonBegin.TimeOfDay) ||
                this.DateLogIn.TimeOfDay > hours.AfternoonEnd.TimeOfDay;
        }

        public ConsultUserSchedulesSchedule()
        {

        }
    }
}