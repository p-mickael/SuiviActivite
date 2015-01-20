using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.ConsultUserSchedules
{
    public class WorkedTimeRange
    {
        public DateTime? Date { get; protected set; }
        public int WorkedHours { get; protected set; }
        public int WorkedMinutes { get; protected set; }
        public int WorkedTime { get; set; }

        protected void SetWorkedTime()
        {
            this.WorkedHours = this.WorkedTime / 60;
            this.WorkedMinutes = this.WorkedTime % 60;
        }
    }
}