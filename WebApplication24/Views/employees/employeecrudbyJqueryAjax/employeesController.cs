using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication24.Models;

namespace WebApplication24.Controllers
{
    public class employeesController : Controller
    {
        private empEntities db = new empEntities();

        //GET: employees
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult exportreport()
        {

            List<employee> emp = db.employee.ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/reports"), "CrystalReport1.rpt"));
            rd.SetDataSource(emp);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream s = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                s.Seek(0, SeekOrigin.Begin);
                return File(s, "application/pdf", "employees.pdf");
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public JsonResult getemployees()
        {
            return Json(db.employee.ToList(), JsonRequestBehavior.AllowGet);
        }



        public JsonResult getemployee(int ? id )
        {

            //if (id == null)
            //{
            //    return Json.st
            //}
           employee employee = db.employee.Find(id);
            //if (employee == null)
            //{
            //    return HttpNotFound();
            //}
            
            return Json(employee, JsonRequestBehavior.AllowGet);
        }
        //  [HttpPost]
        //public JsonResult addemployees(string employee)
        //{
        //    employee obj = JsonConvert.DeserializeObject<employee>(employee);


        //    db.employee.Add(obj);
        //    db.SaveChanges();

        //    return Json(db.employee.ToList(), JsonRequestBehavior.AllowGet);
        //}


        // GET: employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,city")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }




        // GET: employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,city")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = db.employee.Find(id);
            db.employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
