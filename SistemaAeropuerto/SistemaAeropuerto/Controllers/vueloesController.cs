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
    public class vueloesController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: vueloes
        public ActionResult Index()
        {
            var vuelo = db.vuelo.Include(v => v.avion);
            return View(vuelo.ToList());
        }

        // GET: vueloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vuelo vuelo = db.vuelo.Find(id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // GET: vueloes/Create
        public ActionResult Create()
        {
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo");
            return View();
        }

        // POST: vueloes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_vuelo,fecha_vuelo,id_avion")] vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                db.vuelo.Add(vuelo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", vuelo.id_avion);
            return View(vuelo);
        }

        // GET: vueloes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vuelo vuelo = db.vuelo.Find(id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", vuelo.id_avion);
            return View(vuelo);
        }

        // POST: vueloes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_vuelo,fecha_vuelo,id_avion")] vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vuelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_avion = new SelectList(db.avion, "id_avion", "tipo", vuelo.id_avion);
            return View(vuelo);
        }

        // GET: vueloes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vuelo vuelo = db.vuelo.Find(id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // POST: vueloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vuelo vuelo = db.vuelo.Find(id);
            db.vuelo.Remove(vuelo);
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
