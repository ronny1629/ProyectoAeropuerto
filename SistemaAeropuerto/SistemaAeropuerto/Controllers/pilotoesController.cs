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
    public class pilotoesController : Controller
    {
        private SistemaAeropuertoEntities db = new SistemaAeropuertoEntities();

        // GET: pilotoes
        public ActionResult Index()
        {
            return View(db.piloto.ToList());
        }

        // GET: pilotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            piloto piloto = db.piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // GET: pilotoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pilotoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_piloto,Nombre,cedula,telefono,licencia,Horas_Vuelo,fecha_revision")] piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.piloto.Add(piloto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(piloto);
        }

        // GET: pilotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            piloto piloto = db.piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: pilotoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_piloto,Nombre,cedula,telefono,licencia,Horas_Vuelo,fecha_revision")] piloto piloto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piloto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(piloto);
        }

        // GET: pilotoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            piloto piloto = db.piloto.Find(id);
            if (piloto == null)
            {
                return HttpNotFound();
            }
            return View(piloto);
        }

        // POST: pilotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            piloto piloto = db.piloto.Find(id);
            db.piloto.Remove(piloto);
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
