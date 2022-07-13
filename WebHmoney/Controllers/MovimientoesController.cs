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
    public class MovimientoesController : Controller
    {
        private HmoneyModelContainer db = new HmoneyModelContainer();
        [Authorize]
        // GET: Movimientoes
        public ActionResult Index()
        {
            var movimientos = db.Movimientos.Include(m => m.Cuentas).Include(m => m.TipoGasto);
            return View(movimientos.ToList());
        }

        // GET: Movimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = db.Movimientos.Find(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            return View(movimiento);
        }

        // GET: Movimientoes/Create
        public ActionResult Create()
        {
            ViewBag.CuentasId = new SelectList(db.Cuentas, "Id", "NombreCuenta");
            ViewBag.TipoGastoId = new SelectList(db.TipoGastos, "Id", "Nombre");
            return View();
        }

        // POST: Movimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Descripcion,Monto,CuentasId,TipoGastoId")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Movimientos.Add(movimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuentasId = new SelectList(db.Cuentas, "Id", "NombreCuenta", movimiento.CuentasId);
            ViewBag.TipoGastoId = new SelectList(db.TipoGastos, "Id", "Nombre", movimiento.TipoGastoId);
            return View(movimiento);
        }

        // GET: Movimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = db.Movimientos.Find(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentasId = new SelectList(db.Cuentas, "Id", "NombreCuenta", movimiento.CuentasId);
            ViewBag.TipoGastoId = new SelectList(db.TipoGastos, "Id", "Nombre", movimiento.TipoGastoId);
            return View(movimiento);
        }

        // POST: Movimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Descripcion,Monto,CuentasId,TipoGastoId")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentasId = new SelectList(db.Cuentas, "Id", "NombreCuenta", movimiento.CuentasId);
            ViewBag.TipoGastoId = new SelectList(db.TipoGastos, "Id", "Nombre", movimiento.TipoGastoId);
            return View(movimiento);
        }

        // GET: Movimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = db.Movimientos.Find(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            return View(movimiento);
        }

        // POST: Movimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimiento movimiento = db.Movimientos.Find(id);
            db.Movimientos.Remove(movimiento);
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
