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


        [HttpGet]
        public ActionResult GetEmployee()
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                var employees = db.Employees.OrderBy(a => a.FirstName).ToList();

                return Json(new {data = employees}, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                var v = db.Employees.FirstOrDefault(a => a.EmployeeId == id);
                return View(v);

            }
        }

        [HttpPost]
        public ActionResult Save(Employee employee)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities db = new MyDatabaseEntities())
                {
                    if (employee.EmployeeId > 0)
                    {


                        //Edit:
                        var v = db.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

                        if (v != null)
                        {
                            v.City = employee.City;
                            v.Country = employee.Country;
                            v.Email = employee.Email;
                            v.City = employee.City;
                            v.LastName = employee.LastName;
                        }


                    }
                    else
                    {
                        //Save
                        db.Employees.Add(employee);
                    }

                    db.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult {Data = new {status = status}};
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDatabaseEntities db = new MyDatabaseEntities() )
            {
                var v = db.Employees.FirstOrDefault(a => a.EmployeeId == id);
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }

            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;

            using (MyDatabaseEntities db = new MyDatabaseEntities() )
            {
                var v = db.Employees.FirstOrDefault(a => a.EmployeeId == id);

                if (v != null)
                {
                    db.Employees.Remove(v);
                    db.SaveChanges();

                    status = true;
                }
            }

            return  new JsonResult { Data = new { status = status}};
        }
    }
}