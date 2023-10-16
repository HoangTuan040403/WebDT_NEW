using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnQuanLyTapHoa.Models;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class LoaiSPsController : Controller
    {
        private QuanLyTapHoaFinalEntities1 db = new QuanLyTapHoaFinalEntities1();

        // GET: LoaiSPs
        public ActionResult Index()
        {
            return View(db.LoaiSP.ToList());
        }

        // GET: LoaiSPs/Details/
        public ActionResult Details(string id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            LoaiSP loaiSP = db.LoaiSP.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // GET: LoaiSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoai,TenLoai")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {                              
                db.LoaiSP.Add(loaiSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(loaiSP);
        }

        // GET: LoaiSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSP.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // POST: LoaiSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoai,TenLoai")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSP);
        }

        // GET: LoaiSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSP.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // POST: LoaiSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiSP loaiSP = db.LoaiSP.Find(id);
            db.LoaiSP.Remove(loaiSP);
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
