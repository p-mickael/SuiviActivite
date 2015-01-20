using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Exceptions
{
    public class DurationNotFoundException : Exception
    {
        public DurationNotFoundException()
            :base("Les durées minimum et maximum n'ont pas été trouvées dans le fichiers de configuration xml")
        {
            
        }
    }
}
