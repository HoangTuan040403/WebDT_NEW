using DoAnQuanLyTapHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class XemDonHangController : Controller

    {
        QuanLyTapHoaFinalEntities1 db = new QuanLyTapHoaFinalEntities1();

        //chuẩn bị dữ liệu cho View
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }

        // Tạo mới giỏ hàng, nguồn được lấy từ Session
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        /// Thêm sản phẩm vào giỏ hàng
        public ActionResult TaoDonHang(string id)
        {
            // lấy sản phẩm theo id
            var pro = db.SanPham.SingleOrDefault(s => s.MaSP == id);
            if (pro != null)
            {
                GetCart().Add_Product_Cart(pro);
            }
            return RedirectToAction("ShowToCart", "XemDonHang");
        }

        // Cập nhật số lượng và tính lại tổng tiền
        [HttpPost]
        public ActionResult Upadate_Quantity_Cart(FormCollection form)  
        {
            Cart cart = Session["Cart"] as Cart;

            string id_pro = (form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);

            if(_quantity > 0)
            {
                cart.Update_quantity(id_pro, _quantity);
            }
           
            return RedirectToAction("ShowToCart", "XemDonHang");
        }

        // Xóa dòng sản phẩm trong giỏ hàng
        public ActionResult RemoveCart(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "XemDonHang");
        }


        // hàm check thanh toán cho khách hàng và lưu trữ dữ liệu đơn hàng
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                DonHang _order = new DonHang();
                //_order.MaDH = 3; // chỗ này phải để tự nhảy mã đơn ;
                _order.NgayMua = DateTime.Now;
                _order.MaUser = int.Parse(form["NguoiBan"]);
                _order.TongTien = int.Parse(form["TongTien"]);
                db.DonHang.Add(_order);
                foreach (var item in cart.Items)
                {
                    // lưu dòng sản phẩm vào chi tiết hóa đơn
                    ChiTietDonHang _order_detail = new ChiTietDonHang();
                    //_order_detail.MaCTDH = 2;  // chỗ này phải để tự nhảy mã đơn 
                    _order_detail.MaDH = _order.MaDH;
                    _order_detail.MaSP = item._shopping_product.MaSP;
                    _order_detail.DonGia = item._shopping_product.GiaSP;
                    _order_detail.SoLuong = item._shopping_quantity;
                    _order_detail.TongTienSP = _order_detail.DonGia * _order_detail.SoLuong;                

                    db.ChiTietDonHang.Add(_order_detail);
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

        // Thông báo thanh toán thành công
        public ActionResult CheckOut_Success()
        {
            return View();
        }

        // GET: XemDonHang
        public ActionResult Index()
        {
            return View();
        }
    }
}