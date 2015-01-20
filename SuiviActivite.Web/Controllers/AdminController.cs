using SuiviActivite.Business;
using SuiviActivite.Business.Criteria;
using SuiviActivite.Web.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuiviActivite.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            Index model;
            using (UserBusiness userBusiness = new UserBusiness())
            {
                model = new Index(userBusiness.Get(new UserBusinessCriteria { IsActive = true }));
            }

            return View(model);
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
