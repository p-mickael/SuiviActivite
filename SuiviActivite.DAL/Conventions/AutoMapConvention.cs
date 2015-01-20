using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.DAL.Conventions
{
    public class AutoMapConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(Inflector.Inflector.Pluralize(instance.EntityType.Name));
        }
    }
}
