using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.DAL.AutoMappingOverrides
{
    public class UserOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.HasMany<Schedule>(u => u.Schedules).KeyColumn("UserId").Inverse().Cascade.All();
        }
    }
}
