using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebHmoney.Models;

namespace WebHmoney.Controllers
{
    public class TipoGastoesController : Controller
    {
        private HmoneyModelContainer db = new HmoneyModelContainer();
        [Authorize]
        // GET: TipoGastoes
        public ActionResult Index()
        {
            return View(db.TipoGastos.ToList());
        }

        // GET: TipoGastoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGasto tipoGasto = db.TipoGastos.Find(id);
            if (tipoGasto == null)
            {
                return HttpNotFound();
            }
            return View(tipoGasto);
        }

        // GET: TipoGastoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoGastoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,EsActivo")] TipoGasto tipoGasto)
        {
            if (ModelState.IsValid)
            {
                db.TipoGastos.Add(tipoGasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoGasto);
        }

        // GET: TipoGastoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGasto tipoGasto = db.TipoGastos.Find(id);
            if (tipoGasto == null)
            {
                return HttpNotFound();
            }
            return View(tipoGasto);
        }

        // POST: TipoGastoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,EsActivo")] TipoGasto tipoGasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoGasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoGasto);
        }

        // GET: TipoGastoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoGasto tipoGasto = db.TipoGastos.Find(id);
            if (tipoGasto == null)
            {
                return HttpNotFound();
            }
            return View(tipoGasto);
        }

        // POST: TipoGastoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoGasto tipoGasto = db.TipoGastos.Find(id);
            db.TipoGastos.Remove(tipoGasto);
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
