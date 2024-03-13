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
    public class EstadiasController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: Estadias
        public ActionResult Index()
        {
            var estadia = db.Estadia.Include(e => e.vuelo);
            return View(estadia.ToList());
        }

        // GET: Estadias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = db.Estadia.Find(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            return View(estadia);
        }

        // GET: Estadias/Create
        public ActionResult Create()
        {
            ViewBag.id_vuelo = new SelectList(db.vuelo, "id_vuelo", "id_vuelo");
            return View();
        }

        // POST: Estadias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estadia,fecha_inicio,fecha_fin,costo,id_vuelo")] Estadia estadia)
        {
            if (ModelState.IsValid)
            {
                db.Estadia.Add(estadia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_vuelo = new SelectList(db.vuelo, "id_vuelo", "id_vuelo", estadia.id_vuelo);
            return View(estadia);
        }

        // GET: Estadias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = db.Estadia.Find(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_vuelo = new SelectList(db.vuelo, "id_vuelo", "id_vuelo", estadia.id_vuelo);
            return View(estadia);
        }

        // POST: Estadias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estadia,fecha_inicio,fecha_fin,costo,id_vuelo")] Estadia estadia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_vuelo = new SelectList(db.vuelo, "id_vuelo", "id_vuelo", estadia.id_vuelo);
            return View(estadia);
        }

        // GET: Estadias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = db.Estadia.Find(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            return View(estadia);
        }

        // POST: Estadias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estadia estadia = db.Estadia.Find(id);
            db.Estadia.Remove(estadia);
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
