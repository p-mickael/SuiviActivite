using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;

namespace SuiviActivite.Web.Models.Users.ManageUsers
{
    public class ManageUsersUser : UserModel
    {
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }

        public ManageUsersUser(User user)
            :base(user)
        {
            this.Id = user.Id;
            this.IsLocked = user.IsLocked;
            this.IsActive = user.IsActive;
        }

        public ManageUsersUser()
        {}
    }
}