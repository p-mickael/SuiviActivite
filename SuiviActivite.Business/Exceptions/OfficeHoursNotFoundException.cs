using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Exceptions
{
    public class OfficeHoursNotFoundException : Exception
    {
        public OfficeHoursNotFoundException()
            :base("Horaires de bureau non trouvés dans le fichier de configuration xml")
        {

        }
    }
}
