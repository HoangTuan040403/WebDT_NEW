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
        QLBANDTEntities db = new QLBANDTEntities();

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
        public ActionResult TaoDonHang(int id)
        {
            // lấy sản phẩm theo id
            var pro = db.SanPhams.SingleOrDefault(s => s.MaSP == id);
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

            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);

            if (_quantity > 0)
            {
                cart.Update_quantity(id_pro, _quantity);
            }

            return RedirectToAction("ShowToCart", "XemDonHang");
        }

        // Xóa dòng sản phẩm trong giỏ hàng
        public ActionResult RemoveCart(int id)
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