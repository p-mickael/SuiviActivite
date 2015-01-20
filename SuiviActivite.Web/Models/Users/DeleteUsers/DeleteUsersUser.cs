using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Shared;

namespace SuiviActivite.Web.Models.Users.DeleteUsers
{
    public class DeleteUsersUser : UserModel
    {
        public bool ToDelete { get; set; }
        public int Id { get; set; }

        public DeleteUsersUser(User user)
            :base(user)
        {
            this.ToDelete = false;
            this.Id = user.Id;
        }

        public DeleteUsersUser()
        {}
    }
}