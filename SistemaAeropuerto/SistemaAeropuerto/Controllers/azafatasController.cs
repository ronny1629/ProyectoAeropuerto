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
    public class azafatasController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: azafatas
        public ActionResult Index()
        {
            return View(db.azafata.ToList());
        }

        // GET: azafatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            azafata azafata = db.azafata.Find(id);
            if (azafata == null)
            {
                return HttpNotFound();
            }
            return View(azafata);
        }

        // GET: azafatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: azafatas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_azafata,Nombre,cedula,fecha_registro")] azafata azafata)
        {
            if (ModelState.IsValid)
            {
                db.azafata.Add(azafata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(azafata);
        }

        // GET: azafatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            azafata azafata = db.azafata.Find(id);
            if (azafata == null)
            {
                return HttpNotFound();
            }
            return View(azafata);
        }

        // POST: azafatas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_azafata,Nombre,cedula,fecha_registro")] azafata azafata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(azafata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(azafata);
        }

        // GET: azafatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            azafata azafata = db.azafata.Find(id);
            if (azafata == null)
            {
                return HttpNotFound();
            }
            return View(azafata);
        }

        // POST: azafatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            azafata azafata = db.azafata.Find(id);
            db.azafata.Remove(azafata);
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
