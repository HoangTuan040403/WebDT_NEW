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
    public class NhapHangsController : Controller
    {
        private QuanLyTapHoaFinalEntities1 db = new QuanLyTapHoaFinalEntities1();

        // GET: NhapHangs
        public ActionResult Index()
        {
            var nhapHangs = db.NhapHang.Include(n => n.NhaCungCap).Include(n => n.SanPham);
            return View(nhapHangs.ToList());
        }

        // GET: NhapHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapHang nhapHang = db.NhapHang.Find(id);
            if (nhapHang == null)
            {
                return HttpNotFound();
            }
            return View(nhapHang);
        }

        // GET: NhapHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        // POST: NhapHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhapHang,MaSP,MaNCC,NgayNhapSP,SoLuongNhap")] NhapHang nhapHang)
        {
            if (ModelState.IsValid)
            {
                db.NhapHang.Add(nhapHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", nhapHang.MaNCC);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", nhapHang.MaSP);
            return View(nhapHang);
        }

        // GET: NhapHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapHang nhapHang = db.NhapHang.Find(id);
            if (nhapHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", nhapHang.MaNCC);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", nhapHang.MaSP);
            return View(nhapHang);
        }

        // POST: NhapHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhapHang,MaSP,MaNCC,NgayNhapSP,SoLuongNhap")] NhapHang nhapHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhapHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", nhapHang.MaNCC);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", nhapHang.MaSP);
            return View(nhapHang);
        }

        // GET: NhapHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapHang nhapHang = db.NhapHang.Find(id);
            if (nhapHang == null)
            {
                return HttpNotFound();
            }
            return View(nhapHang);
        }

        // POST: NhapHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhapHang nhapHang = db.NhapHang.Find(id);
            db.NhapHang.Remove(nhapHang);
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
