using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Exceptions
{
    public class ConfigFileNotFoundException : Exception
    {
        public ConfigFileNotFoundException()
            :base("Le fichier de configuration n'a pas été trouvé ou est inaccessible")
        {

        }
    }
}
