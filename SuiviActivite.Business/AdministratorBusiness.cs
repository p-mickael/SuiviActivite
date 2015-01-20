using SuiviActivite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business
{
    public class AdministratorBusiness : IDisposable
    {
        private AdministratorDAO _administratorDAO;

        public AdministratorBusiness()
        {
            this._administratorDAO = new AdministratorDAO();
        }

        public int CountAdministrators()
        {
            return this._administratorDAO.CountAdministrators();
        }

        public void Dispose()
        {
            this._administratorDAO.Dispose();
        }
    }
}
