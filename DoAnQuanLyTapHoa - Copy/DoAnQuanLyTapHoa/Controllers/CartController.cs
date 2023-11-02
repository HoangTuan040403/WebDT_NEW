using DoAnQuanLyTapHoa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        QLBANDTEntities db = new QLBANDTEntities();
        public List<MatHangMua> LayGioHang()
        {
            List<MatHangMua> gioHang = Session["GioHang"] as List<MatHangMua>;
            if (gioHang == null)
            {
                gioHang = new List<MatHangMua>();
                Session["GioHang"] = gioHang;
            }
            return gioHang;
        }

        public ActionResult ThemSanPhamVaoGio(int MaSP)
        {
            List<MatHangMua> gioHang = LayGioHang();
            MatHangMua sanPham = gioHang.FirstOrDefault(s => s.Masp == MaSP);
            if (sanPham == null)
            {
                sanPham = new MatHangMua(MaSP);
                gioHang.Add(sanPham);
            }
            else
            {
                sanPham.SoLuong++;
            }
            return RedirectToAction("HienThiGioHang", "Cart", new { id = MaSP });
        }
        private int TinhTongSL()
        {
            int tongSL = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                tongSL = gioHang.Sum(sp => sp.SoLuong);
            return tongSL;
        }
        private double TinhTongTien()
        {
            double TongTien = 0;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
                TongTien = gioHang.Sum(sp => sp.ThanhTien());
            return TongTien;
        }
        public ActionResult HienThiGioHang()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("ProductList", "SanPhams");
            }
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult XoaMatHang(int MaSP)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.Masp == MaSP);
            if (sanpham != null)
            {
                gioHang.RemoveAll(s => s.Masp == MaSP);
                return RedirectToAction("HienThiGioHang");
            }
            if (gioHang.Count == 0)
                return RedirectToAction("ProductList", "SanPhams");
            return RedirectToAction("HienThiGioHang");
        }

        public ActionResult CapNhatMatHang(int MaSP, int SoLuong)
        {
            List<MatHangMua> gioHang = LayGioHang();
            var sanpham = gioHang.FirstOrDefault(s => s.Masp == MaSP);

            if (sanpham != null)
            {
                sanpham.SoLuong = SoLuong;
            }
            return RedirectToAction("HienThiGioHang", "Cart");
        }

        public ActionResult DatHang()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
                return RedirectToAction("ProductList", "SanPhams");
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }
        public ActionResult DatHang2()
        {
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)
                return RedirectToAction("ProductList", "SanPhams");
            ViewBag.TongSL = TinhTongSL();
            ViewBag.TongTien = TinhTongTien();
            return View(gioHang);
        }

        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                //_order.MaDH = 3; // chỗ này phải để tự nhảy mã đơn ;
                _order.DateOr = DateTime.Now;
                _order.SDT = (form["SDT"]);
                _order.TenNgNhan = (form["TenNgNhan"]);
                _order.DiaChiNhan = form["DCNhan"];
                db.Orders.Add(_order);
                foreach (var item in cart.Items)
                {
                    // lưu dòng sản phẩm vào chi tiết hóa đơn
                    OrdersDetail _order_detail = new OrdersDetail();
                    //_order_detail.MaCTDH = 2;  // chỗ này phải để tự nhảy mã đơn 
                    _order_detail.SoLuong = item._shopping_product.SoLuong;
                    _order_detail.ThanhTien = item._shopping_product.GiaSp;
                    _order_detail.MaSP = item._shopping_product.MaSP;
                    _order_detail.MaOr = _order.MaOr;
                    db.OrdersDetails.Add(_order_detail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "XemDonHang");
            }
            catch
            {
                return Content("Có sai sót! Xin kiểm tra lại thông tin"); ;
            }
        }

        public ActionResult DongYDatHang(bool isDirectPayment)
        {

            try
            {
                // Thực hiện thao tác cập nhật dữ liệu ở đây
                User khach = Session["TaiKhoan"] as User;
                List<MatHangMua> gioHang = LayGioHang();
                DONDATHANG DonHang = new DONDATHANG();
                DonHang.MaUser = khach.MaUser;
                DonHang.NgayDH = DateTime.Now;
                DonHang.Trigia = (int)TinhTongTien();
                DonHang.Dagiao = false;
                DonHang.Tennguoinhan = khach.TenUser;
                DonHang.Diachinhan = khach.DiaChi;
                DonHang.Dienthoainhan = khach.sdt;
                DonHang.HinhThucTT = true;
                DonHang.HTGiaohang = false;
                db.DONDATHANGs.Add(DonHang);
                db.SaveChanges();

                foreach (var sanpham in gioHang)
                {
                    CTDATHANG chitiet = new CTDATHANG();
                    chitiet.SODH = DonHang.SODH;
                    chitiet.MaSP = sanpham.Masp;
                    chitiet.Soluong = sanpham.SoLuong;
                    chitiet.Dongia = (int)sanpham.Gia;
                    db.CTDATHANGs.Add(chitiet);
                    Session["GioHang"] = null;
                }
                db.SaveChanges();

            }
            catch (DbUpdateException ex)
            {
                // Lấy thông tin inner exception
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Debug.WriteLine(innerException.Message); // In thông tin lỗi
                    innerException = innerException.InnerException;

                }
            }
            return RedirectToAction("HoanThanhDonHang");
            //


        }
    }
}