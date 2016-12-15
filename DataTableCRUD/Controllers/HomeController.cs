using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTableCRUD.Models;

namespace DataTableCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployee()
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities() )
            {
                var employees= db.Employees.OrderBy(a => a.FirstName).ToList();

                return Json(new {data = employees }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}