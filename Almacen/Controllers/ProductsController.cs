using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Almacen.Models;
using PagedList;
//este controlador es para los productos

namespace Almacen.Controllers

{
    public class ProductsController : Controller
    {
        private db_almacenEntities db = new db_almacenEntities();

        // GET: Products

        public ActionResult Index(int? page, string searchString = "")
        {
            ViewBag.CurrentFilter = searchString;

            var products = from s in db.product.Include(p => p.c_typeProduct) select s;

            if (!String.IsNullOrEmpty(searchString.ToString()))
                products = (products.Where(s => s.fldname.Contains(searchString.ToUpper())));

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
                return PartialView("_ListProducts", products.OrderByDescending(m => m.id_product).ToPagedList(pageNumber, pageSize));

            return View(products.OrderByDescending(m => m.id_product).ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.id_fktypeProduct = new SelectList(db.c_typeProduct, "id_typeProduct", "flddescription");
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_product,fldname,fldprice,flddateOn,fldactive,id_fktypeProduct")] product product)
        {
            if (ModelState.IsValid)
            {
                db.product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_fktypeProduct = new SelectList(db.c_typeProduct, "id_typeProduct", "flddescription", product.id_fktypeProduct);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_fktypeProduct = new SelectList(db.c_typeProduct, "id_typeProduct", "flddescription", product.id_fktypeProduct);
            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_product,fldname,fldprice,flddateOn,fldactive,id_fktypeProduct")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_fktypeProduct = new SelectList(db.c_typeProduct, "id_typeProduct", "flddescription", product.id_fktypeProduct);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.product.Find(id);
            db.product.Remove(product);
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
