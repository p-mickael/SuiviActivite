using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual bool IsLocked { get; set; }
        public virtual bool IsActive { get; set; }

        public virtual IList<Schedule> Schedules { get; set; }
    }
}
