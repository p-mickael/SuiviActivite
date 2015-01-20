using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Criteria
{
    public class ScheduleBusinessCriteria
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateLogIn { get; set; }
        public DateTime? DateLogOut { get; set; }
    }
}
