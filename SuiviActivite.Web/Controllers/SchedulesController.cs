using SuiviActivite.Business;
using SuiviActivite.Business.Criteria;
using SuiviActivite.Web.Models.Schedules.ManageSchedules;
using System.Web.Mvc;
using SuiviActivite.Web.Models.Schedules.AddSchedule;
using SuiviActivite.Web.Models.Schedules.ConsultSchedules;
using SuiviActivite.Web.Models.Schedules.ConsultUserSchedules;
using SuiviActivite.Web.Models.Schedules.EditSchedule;
using System;
using SuiviActivite.Domain;

namespace SuiviActivite.Web.Controllers
{
    [Authorize]
    public class SchedulesController : Controller
    {
        [HttpGet]
        public ActionResult ConsultSchedules()
        {
            ConsultSchedules model;
            using(UserBusiness userBusiness = new UserBusiness())
            {
                model = new ConsultSchedules(userBusiness.GetAll());
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ConsultUserSchedules(int id, string ancor)
        {
            ConsultUserSchedules model;
            using(UserBusiness userBusiness = new UserBusiness())
            {
                model = new ConsultUserSchedules(userBusiness.Get(id));
            }

            if (ancor != null)
                model.MonthToJumpTo = ancor;
            
            return View(model);
        }

        [HttpGet]
        public ActionResult AddSchedule()
        {
            AddSchedule model = GetAddScheduleModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchedule(AddSchedule schedule)
        {
            using(ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {
                Schedule domainSchedule = schedule.GenerateDomainSchedule();
                if (CheckScheduleForErrors(domainSchedule))
                {
                    scheduleBusiness.SaveOrUpdate(domainSchedule);
                    return RedirectToAction("ConsultUserSchedules", new { id = schedule.UserId });
                }

                return View(GetAddScheduleModel(schedule));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSchedule(int id, string ancor)
        {
            int userId;
            using(ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {
                var schedule = scheduleBusiness.Get(id);
                userId = schedule.UserId;
                scheduleBusiness.Delete(schedule);
            }
            return RedirectToAction("ConsultUserSchedules", new { id =  userId, ancor = ancor });
        }

        [HttpGet]
        public ActionResult EditSchedule(int id)
        {
            EditSchedule model;
            using (ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {
                model = new EditSchedule(scheduleBusiness.Get(id));
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditSchedule(EditSchedule schedule, string ancor)
        {
            using (ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {
                Schedule domainSchedule = new Schedule();

                if(schedule.Id > 0)
                    domainSchedule = scheduleBusiness.Get(schedule.Id);

                domainSchedule = schedule.UpdateSchedule(domainSchedule);

                if(CheckScheduleForErrors(domainSchedule))
                {
                    scheduleBusiness.SaveOrUpdate(domainSchedule);
                    return RedirectToAction("ConsultUserSchedules", new { id = schedule.UserId, ancor = ancor });
                }
                else
                {
                    return View(schedule);
                }
            }
        }

        [HttpPost]
        public ActionResult AddScheduleOnDay(EditSchedule schedule)
        {
            return View("EditSchedule", schedule);
        }

        #region Tools
        private AddSchedule GetAddScheduleModel(AddSchedule model = null)
        {
            using (UserBusiness userBusiness = new UserBusiness())
            {
                var userList = userBusiness.Get(new UserBusinessCriteria { IsActive = true });

                if (model == null)
                    model = new AddSchedule(userList);
                else
                    model.GenerateUserList(userList);
            }

            return model;
        }

        private bool CheckScheduleForErrors(Schedule schedule)
        {
            using(ScheduleBusiness scheduleBusiness = new ScheduleBusiness())
            {
                bool noError = true;

                if (!scheduleBusiness.CheckScheduleOverLap(schedule))
                {
                    ModelState.AddModelError("OverlapSchedule", "La période définie pour ce suivi est déjà couverte par un autre suivi");
                    noError &= false;
                }

                if(!scheduleBusiness.CheckSchedulePeriodeNotNull(schedule))
                {
                    ModelState.AddModelError("PeriodeNull", "L'heure d'arrivée doit être strictement inférieure à l'heure de départ");
                    noError &= false;
                }

                return noError;
            }
        }
        #endregion
    }
}
