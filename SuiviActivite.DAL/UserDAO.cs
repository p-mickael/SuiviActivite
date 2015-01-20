using NHibernate;
using NHibernate.Linq;
using SuiviActivite.DAL.Criteria;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuiviActivite.DAL
{
    public class UserDAO : IDisposable
    {
        private ISessionFactory _ISessionFactory;
        private ISession _ISession;

        #region Gestion de l'instance
        public UserDAO()
        {
            this._ISessionFactory = SessionFactoryBuilder.BuildSessionFactory();
            this._ISession = _ISessionFactory.OpenSession();
        }

        public void Dispose()
        {
            this._ISession.Flush();
            this._ISession.Dispose();
            this._ISessionFactory.Dispose();
        }
        #endregion

        #region Gestion des objets
        public IEnumerable<User> GetAll()
        {
            return _ISession.Query<User>()
                .Select(u => u)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName);
        }

        public User Get(int id)
        {
            return (_ISession.Query<User>().Where(u => u.Id == id).SingleOrDefault());
        }

        public IEnumerable<User> Get(UserDalCriteria criteria)
        {
            Func<User, bool> isWorkingPredicate = (u =>
            {
                if (criteria.IsWorking == null)
                    return true;

                Schedule lastSchedule = u.Schedules.OrderByDescending(s => s.DateLogIn).FirstOrDefault();

                return lastSchedule != null ? lastSchedule.DateLogOut == null : false;
            });

            return _ISession.Query<User>()
                .Where(u => u.Id == criteria.Id || criteria.Id == null)
                .Where(u => u.FirstName == criteria.FirstName || criteria.FirstName == null)
                .Where(u => u.LastName == criteria.LastName || criteria.LastName == null)
                .Where(u => u.IsActive == criteria.IsActive || criteria.IsActive == null)
                .Where(u => u.IsLocked == criteria.IsLocked || criteria.IsLocked == null)
                .Where(isWorkingPredicate);
        }

        public void SaveOrUpdate(User user)
        {
            _ISession.SaveOrUpdate(user);
        }

        public void Delete(User user)
        {
            _ISession.Delete(user);
        }
        #endregion
    }
}
