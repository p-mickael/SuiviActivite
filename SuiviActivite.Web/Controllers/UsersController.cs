using SuiviActivite.Business;
using SuiviActivite.Web.Custom.Attributes;
using SuiviActivite.Web.Models.Users.AddUser;
using SuiviActivite.Web.Models.Users.DeleteUsers;
using SuiviActivite.Web.Models.Users.EditUser;
using SuiviActivite.Web.Models.Users.ManageUsers;
using System.Web.Mvc;

namespace SuiviActivite.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        [HttpGet]
        public ActionResult ManageUsers()
        {
            ManageUsers model;
            using(UserBusiness userBusiness = new UserBusiness())
            {
                model = new ManageUsers(userBusiness.GetAll());
            }

            return View(model);
        }

        [HttpPost]
        [HttpParamAction]
        [ValidateAntiForgeryToken]
        public ActionResult SwitchActivation(int id)
        {
            using (UserBusiness userBusiness = new UserBusiness())
            {
                Domain.User user = userBusiness.Get(id);
                user.IsActive = !user.IsActive;

                userBusiness.SaveOrUpdate(user);
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        [HttpParamAction]
        [ValidateAntiForgeryToken]
        public ActionResult SwitchLock(int id)
        {
            using(UserBusiness userBusiness = new UserBusiness())
            {
                Domain.User user = userBusiness.Get(id);
                user.IsLocked = !user.IsLocked;

                userBusiness.SaveOrUpdate(user);
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(AddUser newUser)
        {
            if (ModelState.IsValid)
            {
                using (UserBusiness userBusiness = new UserBusiness())
                {
                    Domain.User newDomainuser = newUser.GenerateDomainUser();
                    newDomainuser.IsActive = true;

                    userBusiness.SaveOrUpdate(newDomainuser);
                }
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public ActionResult DeleteUsers()
        {
            DeleteUsers model;
            using (UserBusiness userBusiness = new UserBusiness())
            {
                model = new DeleteUsers(userBusiness.GetAll());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteUsers(DeleteUsers model)
        {
            if (ModelState.IsValid)
            {
                using (UserBusiness userBusiness = new UserBusiness())
                {
                    foreach (DeleteUsersUser user in model.Users)
                    {
                        if (user.ToDelete)
                        {
                            userBusiness.Delete(userBusiness.Get(user.Id));
                        }
                    }
                }
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        [HttpParamAction]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int id)
        {
            EditUser model;
            using(UserBusiness userBusiness = new UserBusiness())
            {
                model = new EditUser(userBusiness.Get(id));
            }

            return View("EditUser", model);
        }

        [HttpPost]
        public ActionResult SaveUser(EditUser user)
        {
            using(UserBusiness userBusiness = new UserBusiness())
            {
                userBusiness.SaveOrUpdate(user.UpdateDomainUser(userBusiness.Get(user.Id)));
            }

            return RedirectToAction("ManageUsers");
        }
    }
}
