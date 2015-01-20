using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.Business.Configuration
{
    public struct OfficeHours
    {
        public DateTime MorningBegin { get; set; }
        public DateTime MorningEnd { get; set; }

        public DateTime AfternoonBegin { get; set; }
        public DateTime AfternoonEnd { get; set; }
    }
}
