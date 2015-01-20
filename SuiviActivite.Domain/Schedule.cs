using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Domain
{
    public class Schedule
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime DateLogIn { get; set; }
        public virtual DateTime? DateLogOut { get; set; }
               
        public virtual User User { get; set; }

        public virtual double GetMinutes ()
        {
            DateTime upperScheduleLimite = DateLogOut == null ? DateTime.Now : DateLogOut.Value;
            
            return (upperScheduleLimite - DateLogIn).TotalMinutes;
        }
    }
}
