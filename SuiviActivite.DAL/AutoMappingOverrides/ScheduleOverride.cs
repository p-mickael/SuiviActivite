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
    public class ScheduleOverride : IAutoMappingOverride<Schedule>
    {
        public void Override(AutoMapping<Schedule> mapping)
        {
            mapping.References<User>(s => s.User).Column("UserId").ReadOnly();
        }
    }
}
