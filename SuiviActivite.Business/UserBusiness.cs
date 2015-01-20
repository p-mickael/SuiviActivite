using SuiviActivite.Business.Criteria;
using SuiviActivite.DAL;
using SuiviActivite.DAL.Criteria;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;

namespace SuiviActivite.Business
{
    public class UserBusiness : IDisposable
    {
        private UserDAO _userDAO;

        public UserBusiness()
        {
            this._userDAO = new UserDAO();
        }

        public void Dispose()
        {
            this._userDAO.Dispose();
        }

        public void LockUser(int id)
        {
            User user = _userDAO.Get(id);
            LockUser(user);
        }

        public void LockUser(User user)
        {
            user.IsLocked = true;
            _userDAO.SaveOrUpdate(user);
        }

        public void UnlockUser(int id)
        {
            User user = _userDAO.Get(id);
            UnlockUser(user);
        }

        public void UnlockUser(User user)
        {
            user.IsLocked = false;
            _userDAO.SaveOrUpdate(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _userDAO.GetAll();
        }

        public IEnumerable<User> Get(UserBusinessCriteria criteria)
        {
            return _userDAO.Get(AutoMapper.Mapper.Map<UserDalCriteria>(criteria));
        }

        public User Get(int id)
        {
            return _userDAO.Get(id);
        }

        public void SaveOrUpdate(User user)
        {
            bool userIsValid = ValidateUser(user);
            bool scheduleIsInvalid = user.Schedules == null || new ScheduleBusiness().CheckSchedulesIntegrity(user.Schedules);

            if(userIsValid)
                _userDAO.SaveOrUpdate(user);
                
        }

        public void Delete(User user)
        {
            if (user.Schedules != null)
            {
                ScheduleBusiness scheduleBusiness = new ScheduleBusiness();
                foreach (Schedule schedule in user.Schedules)
                {
                    scheduleBusiness.Delete(schedule);
                }
            }
            
            _userDAO.Delete(user);
        }

        private bool ValidateUser(User user)
        {
            return
                !String.IsNullOrEmpty(user.FirstName) &&
                !String.IsNullOrWhiteSpace(user.LastName);
        }
    }
}
