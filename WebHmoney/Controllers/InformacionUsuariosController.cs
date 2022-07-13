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
    public class InformacionUsuariosController : Controller
    {
        private HmoneyModelContainer db = new HmoneyModelContainer();
        [Authorize]
        // GET: InformacionUsuarios
        public ActionResult Index()
        {
            return View(db.InformacionUsuarios.ToList());
        }

        // GET: InformacionUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionUsuario informacionUsuario = db.InformacionUsuarios.Find(id);
            if (informacionUsuario == null)
            {
                return HttpNotFound();
            }
            return View(informacionUsuario);
        }

        // GET: InformacionUsuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InformacionUsuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreCompleto,Email,Telefono,EsActivo")] InformacionUsuario informacionUsuario)
        {
            if (ModelState.IsValid)
            {
                db.InformacionUsuarios.Add(informacionUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(informacionUsuario);
        }

        // GET: InformacionUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionUsuario informacionUsuario = db.InformacionUsuarios.Find(id);
            if (informacionUsuario == null)
            {
                return HttpNotFound();
            }
            return View(informacionUsuario);
        }

        // POST: InformacionUsuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreCompleto,Email,Telefono,EsActivo")] InformacionUsuario informacionUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informacionUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(informacionUsuario);
        }

        // GET: InformacionUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformacionUsuario informacionUsuario = db.InformacionUsuarios.Find(id);
            if (informacionUsuario == null)
            {
                return HttpNotFound();
            }
            return View(informacionUsuario);
        }

        // POST: InformacionUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformacionUsuario informacionUsuario = db.InformacionUsuarios.Find(id);
            db.InformacionUsuarios.Remove(informacionUsuario);
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
