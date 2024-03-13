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
    public class propietariosController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: propietarios
        public ActionResult Index()
        {
            var propietario = db.propietario.Include(p => p.avion);
            return View(propietario.ToList());
        }

        // GET: propietarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            propietario propietario = db.propietario.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // GET: propietarios/Create
        public ActionResult Create()
        {
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo");
            return View();
        }

        // POST: propietarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_propietario,Nombre,cedula,id_avion,fecha_registro")] propietario propietario)
        {
            if (ModelState.IsValid)
            {
                db.propietario.Add(propietario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", propietario.id_avion);
            return View(propietario);
        }

        // GET: propietarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            propietario propietario = db.propietario.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", propietario.id_avion);
            return View(propietario);
        }

        // POST: propietarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_propietario,Nombre,cedula,id_avion,fecha_registro")] propietario propietario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propietario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", propietario.id_avion);
            return View(propietario);
        }

        // GET: propietarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            propietario propietario = db.propietario.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // POST: propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            propietario propietario = db.propietario.Find(id);
            db.propietario.Remove(propietario);
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
