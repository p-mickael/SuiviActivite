using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Criteria
{
    public class UserBusinessCriteria
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public bool? IsWorking { get; set; }
    }
}
