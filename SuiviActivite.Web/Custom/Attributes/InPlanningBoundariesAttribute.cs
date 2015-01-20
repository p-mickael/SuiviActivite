using SuiviActivite.Business.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuiviActivite.Web.Custom.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class InPlanningBoundariesAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                return ValidateAgainstOfficeHours((DateTime)value);
            }

            return base.IsValid(value);
        }

        private bool ValidateAgainstOfficeHours(DateTime value)
        {
            OfficeHours officeHours = ConfigManager.Instance.OfficeHours;

            DateTime leveledValue = new DateTime
            (
                officeHours.MorningBegin.Year,
                officeHours.MorningBegin.Month,
                officeHours.MorningBegin.Day,
                value.Hour,
                value.Minute,
                0
            );

            bool validSchedule = (leveledValue >= officeHours.MorningBegin && leveledValue <= officeHours.MorningEnd) ||
                                 (leveledValue >= officeHours.AfternoonBegin && leveledValue <= officeHours.AfternoonEnd);

            return validSchedule;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "InPlanning"
            };
        }
    }
}