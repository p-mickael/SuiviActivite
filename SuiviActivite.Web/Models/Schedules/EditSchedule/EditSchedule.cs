using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.EditSchedule
{
    public class EditSchedule
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name="Date du suivi")]
        [DisplayFormat(DataFormatString="{0:D}")]
        public DateTime Date { get; set; }

        [Display(Name="Heure d'arrivée")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:HH:mm}")]
        public DateTime TimeLogIn { get; set; }

        [Display(Name="Heure du départ")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime? TimeLogOut { get; set; }

        public Schedule UpdateSchedule(Schedule schedule)
        {
            schedule.Id = this.Id;
            schedule.UserId = this.UserId;
            schedule.DateLogIn = new DateTime(Date.Year, Date.Month, Date.Day, TimeLogIn.Hour, TimeLogIn.Minute, 0);
            
            DateTime? logOutTime = this.TimeLogOut == null ? null : new Nullable<DateTime>(new DateTime(Date.Year, Date.Month, Date.Day, TimeLogOut.Value.Hour, TimeLogOut.Value.Minute, 0));
            schedule.DateLogOut = logOutTime;

            return schedule;
        }

        public EditSchedule(Schedule schedule)
        {
            this.Id = schedule.Id;
            this.UserId = schedule.UserId;
            this.Date = schedule.DateLogIn;
            this.TimeLogIn = schedule.DateLogIn;
            this.TimeLogOut = schedule.DateLogOut;
        }

        public EditSchedule()
        {}
    }
}