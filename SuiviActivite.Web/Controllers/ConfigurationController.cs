using SuiviActivite.Business.Configuration;
using SuiviActivite.Web.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuiviActivite.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("EditConfig");
        }

        [HttpGet]
        public ActionResult EditConfig()
        {
            EditConfig model = new EditConfig();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditConfig(EditConfig config)
        {
            ConfigManager.Instance.SetNewOfficeHours
            (
                morningBegin: config.MornginBegin,
                morningEnd: config.MorningEnd,
                afternoonBegin: config.AfternoonBegin,
                afternoonEnd: config.AfternoonEnd
            );

            return RedirectToAction("Index", "Admin");
        }
    }
}
