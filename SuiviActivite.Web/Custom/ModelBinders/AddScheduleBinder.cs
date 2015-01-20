using SuiviActivite.Web.Models.Schedules.AddSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuiviActivite.Web.Custom.ModelBinders
{
    public class AddScheduleBinder : DefaultModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string logInTimeStr = controllerContext.HttpContext.Request.Form["LogInTime"].ToString().ToLower();
            string logOutTimeStr = controllerContext.HttpContext.Request.Form["LogOutTime"].ToString().ToLower();

            if (logInTimeStr.Contains('h') || logOutTimeStr.Contains('h'))
            {
                FormatTimes(ref logInTimeStr, ref logOutTimeStr);
                AddSchedule model = GetModel(controllerContext, logInTimeStr, logOutTimeStr);

                return model;
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

        private static void FormatTimes(ref string logInTimeStr, ref string logOutTimeStr)
        {
            logInTimeStr = logInTimeStr.Replace('h', ':');
            logOutTimeStr = logOutTimeStr.Replace('h', ':');

            if (logInTimeStr[logInTimeStr.Length - 1] == ':')
                logInTimeStr += "00";

            if (logOutTimeStr[logOutTimeStr.Length - 1] == ':')
                logOutTimeStr += "00";
        }

        private static AddSchedule GetModel(ControllerContext controllerContext, string logInTimeStr, string logOutTimeStr)
        {
            DateTime logInTime;
            DateTime.TryParse(String.Format("01/01/0001 {0}", logInTimeStr), out logInTime);

            DateTime logOutTime;
            DateTime.TryParse(String.Format("01/01/0001 {0}", logOutTimeStr), out logOutTime);

            DateTime dateSchedule;
            DateTime.TryParse(controllerContext.HttpContext.Request.Form["DateSchedule"].ToString(), out dateSchedule);

            int userId;
            Int32.TryParse(controllerContext.HttpContext.Request.Form["UserId"].ToString(), out userId);

            AddSchedule model = new AddSchedule
            {
                DateSchedule = dateSchedule,
                LogInTime = logInTime,
                LogOutTime = logOutTime,
                UserId = userId
            };

            return model;
        }
    }
}