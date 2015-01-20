using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using SuiviActivite.Domain;

namespace SuiviActivite.DAL
{
    public class AdministratorDAO : IDisposable
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;

        public AdministratorDAO()
        {
            this._sessionFactory = SessionFactoryBuilder.BuildSessionFactory();
            this._session = this._sessionFactory.OpenSession();
        }

        public int CountAdministrators()
        {
            return _session.Query<Administrator>().Count();
        }

        public void Dispose()
        {
            this._session.Dispose();
            this._sessionFactory.Dispose();
        }
    }
}
