using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaAeropuerto;

namespace SistemaAeropuerto.Controllers
{
    public class hangarrsController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: hangarrs
        public ActionResult Index()
        {
            return View(db.hangarr.ToList());
        }

        // GET: hangarrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hangarr hangarr = db.hangarr.Find(id);
            if (hangarr == null)
            {
                return HttpNotFound();
            }
            return View(hangarr);
        }

        // GET: hangarrs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: hangarrs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_hangar,ubicacion,capacidad")] hangarr hangarr)
        {
            if (ModelState.IsValid)
            {
                db.hangarr.Add(hangarr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangarr);
        }

        // GET: hangarrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hangarr hangarr = db.hangarr.Find(id);
            if (hangarr == null)
            {
                return HttpNotFound();
            }
            return View(hangarr);
        }

        // POST: hangarrs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_hangar,ubicacion,capacidad")] hangarr hangarr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangarr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangarr);
        }

        // GET: hangarrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hangarr hangarr = db.hangarr.Find(id);
            if (hangarr == null)
            {
                return HttpNotFound();
            }
            return View(hangarr);
        }

        // POST: hangarrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hangarr hangarr = db.hangarr.Find(id);
            db.hangarr.Remove(hangarr);
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
