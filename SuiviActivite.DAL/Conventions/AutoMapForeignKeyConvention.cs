using FluentNHibernate.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.DAL.Conventions
{
    public class AutoMapForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(FluentNHibernate.Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";  // many-to-many, one-to-many, join

            return property.Name + "Id"; // many-to-one
        }
    }
}
