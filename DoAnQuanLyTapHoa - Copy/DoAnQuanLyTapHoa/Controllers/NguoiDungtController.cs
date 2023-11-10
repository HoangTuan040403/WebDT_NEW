using DoAnQuanLyTapHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class NguoiDungtController : Controller
    {
        // GET: NguoiDungt
        QLBANDTEntities database = new QLBANDTEntities();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(User kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.TenUser))
                    ModelState.AddModelError(string.Empty, "Họ tên không được để trống");
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (string.IsNullOrEmpty(kh.email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(kh.sdt))
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                if (string.IsNullOrEmpty(kh.DiaChi))
                    ModelState.AddModelError(string.Empty, "Địa chỉ thoại không được để trống");
                //
                var khachhang = database.Users.FirstOrDefault(k => k.TenDN == kh.TenDN);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng ký tên này");
                else if (ModelState.IsValid)
                {
                    database.Users.Add(kh);
                    database.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(User kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kh.TenDN))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(kh.MatKhau))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    var khachhang = database.Users.FirstOrDefault(k => k.TenDN == kh.TenDN && k.MatKhau == kh.MatKhau);
                    if (khachhang != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        Session["TaiKhoan"] = khachhang;
                        Session["TenDN"] = khachhang.TenUser;
                        Session["MaUser"] = khachhang.MaUser;
                        Session["UserRole"] = khachhang.Roleuser;
                        if (khachhang.Roleuser == "NhanVien")
                        {
                            // Nếu là nhân viên, thiết lập tên mặc định
                            Session["DisplayName"] = "NhanVien";
                            return RedirectToAction("Index", "TrangChu");
                        }
                        else
                        {
                            // Nếu không phải nhân viên, để người dùng điền tên
                            Session["DisplayName"] = "";
                            return RedirectToAction("ProductList", "SanPhams");
                        }
                        return RedirectToAction("Index", "SanPhams");
                        if (khachhang.Roleuser.ToString() == "NhanVien")
                            return RedirectToAction("Index", "TrangChu");
                        else if (khachhang.Roleuser.ToString() == "")
                            return RedirectToAction("ProductList", "SanPhams");
                        else
                            return RedirectToAction("DangNhap", "NguoiDung");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();

        }

        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("ProductList", "SanPhams");

        }
    }
}