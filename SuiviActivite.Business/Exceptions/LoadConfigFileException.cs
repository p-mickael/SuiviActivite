using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Exceptions
{
    public class LoadConfigFileException : Exception
    {
        public LoadConfigFileException()
            :base("Problème de chargement du fichier xml de configuration")
        {

        }
    }
}
