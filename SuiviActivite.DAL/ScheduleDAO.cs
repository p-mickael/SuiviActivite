using NHibernate;
using NHibernate.Linq;
using SuiviActivite.DAL.Criteria;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuiviActivite.DAL
{
    public class ScheduleDAO : IDisposable
    {
        private ISessionFactory _ISessionFactory;
        private ISession _ISession;

        #region Gestion de l'instance
        public ScheduleDAO()
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
        public IEnumerable<Schedule> GetAll()
        {
            return _ISession.Query<Schedule>().Select(u => u);
        }

        public Schedule Get(int id)
        {
            return (_ISession.Query<Schedule>().Where(u => u.Id == id).SingleOrDefault());
        }

        public IEnumerable<Schedule> Get(ScheduleDalCriteria criteria)
        {
            return _ISession.Query<Schedule>()
                .Where(s => criteria.Id == s.Id || criteria.Id == null)
                .Where(s => criteria.DateLogIn == s.DateLogIn || criteria.DateLogIn == null)
                .Where(s => criteria.DateLogOut == s.DateLogOut || criteria.DateLogOut == null)
                .Where(s => criteria.UserId == s.UserId || criteria.UserId == null);
        }

        public void SaveOrUpdate(Schedule schedule)
        {
            _ISession.SaveOrUpdate(schedule);
        }

        public void Delete(Schedule schedule)
        {
            _ISession.Delete(schedule);
        }
        #endregion
    }
}
