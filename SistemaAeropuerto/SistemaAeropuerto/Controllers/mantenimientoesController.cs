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
    public class mantenimientoesController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: mantenimientoes
        public ActionResult Index()
        {
            var mantenimiento = db.mantenimiento.Include(m => m.avion).Include(m => m.mecanicos);
            return View(mantenimiento.ToList());
        }

        // GET: mantenimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mantenimiento mantenimiento = db.mantenimiento.Find(id);
            if (mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(mantenimiento);
        }

        // GET: mantenimientoes/Create
        public ActionResult Create()
        {
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo");
            ViewBag.id_mecanico = new SelectList(db.mecanicos, "id_mecanico", "Nombre");
            return View();
        }

        // POST: mantenimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mantenimiento,id_avion,id_mecanico,fecha_mantenimiento")] mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.mantenimiento.Add(mantenimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", mantenimiento.id_avion);
            ViewBag.id_mecanico = new SelectList(db.mecanicos, "id_mecanico", "Nombre", mantenimiento.id_mecanico);
            return View(mantenimiento);
        }

        // GET: mantenimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mantenimiento mantenimiento = db.mantenimiento.Find(id);
            if (mantenimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", mantenimiento.id_avion);
            ViewBag.id_mecanico = new SelectList(db.mecanicos, "id_mecanico", "Nombre", mantenimiento.id_mecanico);
            return View(mantenimiento);
        }

        // POST: mantenimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mantenimiento,id_avion,id_mecanico,fecha_mantenimiento")] mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mantenimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", mantenimiento.id_avion);
            ViewBag.id_mecanico = new SelectList(db.mecanicos, "id_mecanico", "Nombre", mantenimiento.id_mecanico);
            return View(mantenimiento);
        }

        // GET: mantenimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mantenimiento mantenimiento = db.mantenimiento.Find(id);
            if (mantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(mantenimiento);
        }

        // POST: mantenimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mantenimiento mantenimiento = db.mantenimiento.Find(id);
            db.mantenimiento.Remove(mantenimiento);
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
