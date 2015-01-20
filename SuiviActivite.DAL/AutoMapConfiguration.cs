using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.DAL
{
    public class AutoMapConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "SuiviActivite.Domain";
        }
    }
}
