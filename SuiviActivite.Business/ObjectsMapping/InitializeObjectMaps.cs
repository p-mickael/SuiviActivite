using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiviActivite;
using SuiviActivite.Domain;
using SuiviActivite.Business.Criteria;
using SuiviActivite.DAL.Criteria;

namespace SuiviActivite.Business.ObjectsMapping
{
    public static class InitializeObjectMaps
    {
        public static void InitializeMaps()
        {
            AutoMapper.Mapper.CreateMap<UserBusinessCriteria, UserDalCriteria>();
            AutoMapper.Mapper.CreateMap<ScheduleBusinessCriteria, ScheduleDalCriteria>();
        }
    }
}

