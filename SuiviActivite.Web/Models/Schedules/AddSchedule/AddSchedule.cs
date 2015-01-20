using SuiviActivite.Domain;
using SuiviActivite.Web.Custom.Attributes;
using SuiviActivite.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Schedules.AddSchedule
{
    public class AddSchedule
    {
        public IList<System.Web.Mvc.SelectListItem> Users { get; set; }

        [Required(ErrorMessage="Vous devez sélectionner un utilisateur")]
        [Display(Name = "Utilisateur à modifier")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? DateSchedule { get; set; }

        [Required]
        [Display(Name = "Heure d'arrivée")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        //[InPlanningBoundaries(ErrorMessage = "L'heure d'arrivée doit être dans les limites des heures de bureaux")]
        public DateTime LogInTime { get; set; }

        [Display(Name = "Heure de départ")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        //[InPlanningBoundaries(ErrorMessage="L'heure de départ doit être dans les limites des heures de bureaux")]
        public DateTime? LogOutTime { get; set; }

        public Schedule GenerateDomainSchedule()
        {
            DateTime LogInDate = new DateTime
            (
                DateSchedule.Value.Year, 
                DateSchedule.Value.Month, 
                DateSchedule.Value.Day, 
                LogInTime.Hour, 
                LogInTime.Minute, 
                0
            );

            DateTime? LogOutDate = LogOutTime == null ? null : new Nullable<DateTime>
            (
                new DateTime
                (
                    DateSchedule.Value.Year,
                    DateSchedule.Value.Month,
                    DateSchedule.Value.Day,
                    LogOutTime.Value.Hour,
                    LogOutTime.Value.Minute,
                    0
                )
            );

            Schedule newSchedule = new Schedule
            {
                UserId = UserId,
                DateLogIn = LogInDate,
                DateLogOut = LogOutDate
            };

            return newSchedule;
        }

        //private void ParseDateTimes(out DateTime LogInDate, out DateTime LogOutDate)
        //{
        //    int year = DateSchedule.Value.Year;
        //    int month = DateSchedule.Value.Month;
        //    int day = DateSchedule.Value.Day;

        //    int logInTimeHour = LogInTime.Value.Hour;
        //    int logInTimeMinute = LogInTime.Value.Minute;

        //    int logOutTimeHour = LogOutTime.Value.Hour;
        //    int logOutTimeMinute = LogOutTime.Value.Minute;

        //    LogInDate = new DateTime(year, month, day, logInTimeHour, logInTimeMinute, 0);
        //    LogOutDate = new DateTime(year, month, day, logOutTimeHour, logOutTimeMinute, 0);
        //}

        public AddSchedule(IEnumerable<User> users)
        {
            GenerateUserList(users);
        }

        public void GenerateUserList(IEnumerable<User> users)
        {
            this.Users = new List<System.Web.Mvc.SelectListItem>();
            foreach (User user in users)
            {
                this.Users.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = String.Format("{0} {1}", user.LastName, user.FirstName),
                    Value = user.Id.ToString()
                });
            }
        }

        public AddSchedule()
        {}
    }
}