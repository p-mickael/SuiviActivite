using SuiviActivite.Business.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuiviActivite.Web.Models.Configuration
{
    public class EditConfig
    {
        [Required]
        [Display(Name="Heure du début de la matinée")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:HH:mm}")]
        public DateTime MornginBegin { get; set; }

        [Required]
        [Display(Name = "Heure de la fin de la matinée")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime MorningEnd { get; set; }

        [Required]
        [Display(Name = "Heure du début de l'après midi")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime AfternoonBegin { get; set; }

        [Required]
        [Display(Name = "Heure de la fin de l'après midi")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime AfternoonEnd { get; set; }

        public EditConfig()
        {
            OfficeHours hours = ConfigManager.Instance.OfficeHours;
            
            this.MornginBegin = hours.MorningBegin;
            this.MorningEnd = hours.MorningEnd;
            this.AfternoonBegin = hours.AfternoonBegin;
            this.AfternoonEnd = hours.AfternoonEnd;
        }
    }
}