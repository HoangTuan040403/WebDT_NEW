using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnQuanLyTapHoa.Models;
using System.Globalization;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class SanPhamsController : Controller
    {
        public QuanLyTapHoaFinalEntities1 db = new QuanLyTapHoaFinalEntities1();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPham.Include(s => s.LoaiSP);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,HinhSP,MoTaSP,GiaSP,SoLuongSP,MaLoai,MaNSX")] SanPham sanPham,
            HttpPostedFileBase HinhSP)
        {
            if (ModelState.IsValid)
            {

                if (HinhSP != null)
                {
                    //Lấy tên file của hình được up lên
                    var fileName = Path.GetFileName(HinhSP.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    //Lưu tên
                    sanPham.HinhSP = fileName;
                    //Save vào Images Folder
                    HinhSP.SaveAs(path);
                }

                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,HinhSP,MoTaSP,GiaSP,SoLuongSP,MaLoai,MaNSX")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSP, "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Products
        [HttpGet]
        public ActionResult ProductList(String searchString)
        {
            // Tạo Products và có tham chiếu đến Loại sản phẩm:
            var products = db.SanPham.Include(p => p.LoaiSP);

            //Tìm kiếm chuỗi truy vấn theo tên sản phẩm, nếu chuỗi truy vấn SearchString khác rỗng, null
            if(!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.TenSP.Contains(searchString));
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm nào");
            }    
            return View(products.ToList());

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
