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
    public class mecanicosController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: mecanicos
        public ActionResult Index()
        {
            return View(db.mecanicos.ToList());
        }

        // GET: mecanicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mecanicos mecanicos = db.mecanicos.Find(id);
            if (mecanicos == null)
            {
                return HttpNotFound();
            }
            return View(mecanicos);
        }

        // GET: mecanicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mecanicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mecanico,Nombre,cedula,telefono,fecha_registro")] mecanicos mecanicos)
        {
            if (ModelState.IsValid)
            {
                db.mecanicos.Add(mecanicos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mecanicos);
        }

        // GET: mecanicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mecanicos mecanicos = db.mecanicos.Find(id);
            if (mecanicos == null)
            {
                return HttpNotFound();
            }
            return View(mecanicos);
        }

        // POST: mecanicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mecanico,Nombre,cedula,telefono,fecha_registro")] mecanicos mecanicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mecanicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mecanicos);
        }

        // GET: mecanicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mecanicos mecanicos = db.mecanicos.Find(id);
            if (mecanicos == null)
            {
                return HttpNotFound();
            }
            return View(mecanicos);
        }

        // POST: mecanicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mecanicos mecanicos = db.mecanicos.Find(id);
            db.mecanicos.Remove(mecanicos);
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
