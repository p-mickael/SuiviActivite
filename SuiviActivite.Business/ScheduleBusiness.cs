using SuiviActivite.Business.Configuration;
using SuiviActivite.Business.Criteria;
using SuiviActivite.Business.Exceptions;
using SuiviActivite.DAL;
using SuiviActivite.DAL.Criteria;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SuiviActivite.Business
{
    public class ScheduleBusiness : IDisposable
    {
        private ScheduleDAO _scheduleDAO;

        #region Gestion de l'instance
        public ScheduleBusiness()
        {
            this._scheduleDAO = new ScheduleDAO();
        }

        public void Dispose()
        {
            this._scheduleDAO.Dispose();
        }
        #endregion

        #region Gestion des objets
        public IEnumerable<Schedule> GetAll()
        {
            return _scheduleDAO.GetAll();
        }

        public Schedule Get(int id)
        {
            return _scheduleDAO.Get(id);
        }

        public IEnumerable<Schedule> Get(ScheduleBusinessCriteria criteria)
        {
            return _scheduleDAO.Get(AutoMapper.Mapper.Map<ScheduleDalCriteria>(criteria));
        }

        public bool SaveOrUpdate(Schedule schedule)
        {
            bool scheduleIsValid = CheckScheduleOverLap(schedule);
            
            if(!CheckSchedulePeriodeNotNull(schedule))
            {
                var scheduleToDelete = _scheduleDAO.Get(schedule.Id);
                if (scheduleToDelete != null)
                    _scheduleDAO.Delete(scheduleToDelete);
            }
            else if (scheduleIsValid)
            {
                _scheduleDAO.SaveOrUpdate(schedule);
            }

            return scheduleIsValid;
        }

        public void Delete(Schedule schedule)
        {
            _scheduleDAO.Delete(schedule);
        }
        #endregion

        #region Validation
        public bool CheckSchedulesTimeOfDay(IEnumerable<Schedule> schedules)
        {
            foreach (Schedule schedule in schedules)
            {
                if (!CheckScheduleTimeOfDay(schedule))
                    return false;
            }

            return true;
        }

        public bool CheckScheduleTimeOfDay(Schedule schedule)
        {
            OfficeHours hours = ConfigManager.Instance.OfficeHours;
            DateTime loginHour = DateTime.Now;

            return
                (loginHour.TimeOfDay >= hours.MorningBegin.TimeOfDay && loginHour.TimeOfDay <= hours.MorningEnd.TimeOfDay) ||
                (loginHour.TimeOfDay >= hours.AfternoonBegin.TimeOfDay && loginHour.TimeOfDay <= hours.AfternoonEnd.TimeOfDay);
        }

        public bool CheckSchedulesIntegrity(IEnumerable<Schedule> schedules)
        {
            foreach (Schedule schedule in schedules)
            {
                if (!CheckScheduleIntegrity(schedule))
                    return false;
            }

            return true;
        }

        public bool CheckScheduleIntegrity(Schedule schedule)
        {
            return CheckScheduleOverLap(schedule) && CheckSchedulePeriodeNotNull(schedule);
        }

        public bool CheckScheduleOverLap(Schedule schedule)
        {
            Func<Schedule, bool> overlap = (s) =>
            {
                return
                    (schedule.DateLogIn == s.DateLogIn) ||
                    (schedule.DateLogOut == s.DateLogOut) ||
                    (schedule.DateLogIn < s.DateLogIn && schedule.DateLogOut > s.DateLogIn) || 
                    (schedule.DateLogIn < s.DateLogOut && schedule.DateLogOut > s.DateLogOut);
            };

            var schedules = _scheduleDAO.Get(new ScheduleDalCriteria { UserId = schedule.UserId })
                                        .Where(overlap)
                                        .Where(s => s.Id != schedule.Id);
            
            return schedules.Count() == 0;
        }

        public bool CheckSchedulePeriodeNotNull(Schedule schedule)
        {
            return schedule.DateLogOut == null || (schedule.DateLogOut.Value - schedule.DateLogIn).TotalMinutes >= 1 ;
        }

        #endregion
    }
}
