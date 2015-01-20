using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;
using System.Linq;

namespace SuiviActivite.Web.Models.Home.Index
{
    public class IndexUser : UserModel
    {
        public int Id { get; set; }
        public bool IsLocked { get; set; }
        public bool IsWorking { get; set; }

        public IndexUser(User user)
            : base(user)
        {
            this.IsLocked = user.IsLocked;
            this.Id = user.Id;

            Schedule lastSchedule = user.Schedules.OrderByDescending(s => s.DateLogIn).FirstOrDefault();
            this.IsWorking = lastSchedule != null && lastSchedule.DateLogOut == null;
        }

        public IndexUser()
        {

        }
    }
}