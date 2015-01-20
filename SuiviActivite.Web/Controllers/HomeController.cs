using SuiviActivite.Business;
using SuiviActivite.Business.Criteria;
using SuiviActivite.Domain;
using SuiviActivite.Web.Models.Home.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuiviActivite.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.HideMenu = true;
            ViewBag.HideLogin = true;
            using(UserBusiness userBusiness = new UserBusiness())
            {
                Index model = new Index(userBusiness.Get(new UserBusinessCriteria { IsActive = true }));
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(int id)
        {
            Schedule newSchedule = new Schedule
            {
                UserId = id,
                DateLogIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0),
                DateLogOut = null
            };

            using (ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {

                if (!scheduleBusiness.CheckScheduleTimeOfDay(newSchedule))
                {
                    newSchedule.DateLogOut = DateTime.Now.AddMinutes(1);
                    LockUser(id);
                }

                scheduleBusiness.SaveOrUpdate(newSchedule);
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut(int id)
        {
            using(ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {
                Schedule lastSchedule = scheduleBusiness.Get(new ScheduleBusinessCriteria { UserId = id })
                                                        .OrderByDescending(s => s.DateLogIn)
                                                        .FirstOrDefault();

                if(lastSchedule != null && lastSchedule.DateLogOut == null)
                {
                    lastSchedule.DateLogOut = new DateTime
                    (
                        DateTime.Now.Year, 
                        DateTime.Now.Month, 
                        DateTime.Now.Day, 
                        DateTime.Now.Hour, 
                        DateTime.Now.Minute, 
                        0
                    );

                    if(!scheduleBusiness.CheckSchedulePeriodeNotNull(lastSchedule))
                    {
                        scheduleBusiness.Delete(lastSchedule);
                    }
                    else if (!scheduleBusiness.CheckScheduleTimeOfDay(lastSchedule))
                    {
                        LogOutWithLock(id, scheduleBusiness, lastSchedule);
                    }
                }
            }

            return RedirectToAction("index");
        }

        /// <summary>
        /// Ferme le dernier pointage a 23:59:59 et crée un nouveau pointage 
        /// qui démarre a 00:00:00 du jour en cours et termine à l'heure du click
        /// </summary>
        /// <param name="userId">Id de l'utilisateur</param>
        /// <param name="scheduleBusiness">Instance de gestion des suivis</param>
        /// <param name="lastSchedule">Derniers suivi ouvert trouvé pour l'utilisateur</param>
        /// <param name="logOutTime">Heure du click</param>
        private static void LogOutWithLock(int userId, ScheduleBusiness scheduleBusiness, Schedule lastSchedule)
        {
            LockUser(userId);

            Schedule newSchedule = new Schedule
            {
                DateLogIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0),
                DateLogOut = lastSchedule.DateLogOut,
                UserId = userId
            };

            lastSchedule.DateLogOut = new DateTime
            (
                lastSchedule.DateLogIn.Year,
                lastSchedule.DateLogIn.Month,
                lastSchedule.DateLogIn.Day,
                23, 59, 00
            );

            scheduleBusiness.SaveOrUpdate(newSchedule);
        }

        /// <summary>
        /// Permet de verrouiller un utilisateur
        /// </summary>
        /// <param name="userId">Id de l'utilisateur</param>
        private static void LockUser(int userId)
        {
            using(UserBusiness userBusiness = new UserBusiness())
            {
                userBusiness.LockUser(userId);
            }
        }
    }
}
