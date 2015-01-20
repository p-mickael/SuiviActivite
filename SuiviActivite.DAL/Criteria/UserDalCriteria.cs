using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.DAL.Criteria
{
    public class UserDalCriteria
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public bool? IsWorking { get; set; }
    }
}
