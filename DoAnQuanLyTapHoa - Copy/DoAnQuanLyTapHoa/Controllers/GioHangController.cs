using DoAnQuanLyTapHoa.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
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

        private double TinhTongTienvnd()
        {
            double TongTien = 0;
            decimal b = 23000;
            List<MatHangMua> gioHang = LayGioHang();
            if (gioHang != null)
            {
                TongTien = gioHang.Sum(sp => sp.ThanhTien());
            }
            decimal c = (decimal)TongTien / b;
            return (double)c;
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



        public ActionResult DongYDatHang(bool isDirectPayment)
        {

            if (isDirectPayment)
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
                    {
                        chitiet.SODH = DonHang.SODH;
                        chitiet.MaSP = sanpham.Masp;
                        chitiet.Soluong = sanpham.SoLuong;
                        chitiet.Dongia = (int)sanpham.Gia;
                    }

                    db.CTDATHANGs.Add(chitiet);
                    Session["GioHang"] = null;
                    foreach (var p in db.SanPhams.Where(s => s.MaSP == chitiet.MaSP)) // lấy ID Product có trong giỏ hàng
                    {
                        var update_quan_pro = p.SoLuong - chitiet.Soluong; //Số lượng tồn mới  = số lượng tồn - số lượng đã mua 
                        if (p.SoLuong > 0)
                        {
                            p.SoLuong = (int)update_quan_pro; //Thực hiện cập nhật lại số lượng tồn cho cột Quantity của bảng Product

                        }
                    }
                }

                db.SaveChanges();
                Session["GioHang"] = null;
                return RedirectToAction("HoanThanhDonHang");

            }
            else
            {
                return RedirectToAction("PaymentWithPaypal", "Cart");
            }


        }


        public ActionResult HoanThanhDonHang()
        {
            return View();
        }


        public ActionResult Paypal()
        {
            // thanh toán thành công
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/GioHang/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            User kh = Session["TaiKhoan"] as User;
            List<MatHangMua> giohang = LayGioHang();

            // thêm dữ liệu vào đơn hàng
            DONDATHANG donhang = new DONDATHANG();
            donhang.MaUser = kh.MaUser;
            donhang.NgayDH = DateTime.Now;
            donhang.Trigia = (Decimal)TinhTongTien();
            donhang.Dagiao = false;
            donhang.Tennguoinhan = kh.TenUser;
            donhang.Diachinhan = kh.DiaChi;
            donhang.Dienthoainhan = kh.sdt.ToString();
            donhang.HinhThucTT = true;
            donhang.HTGiaohang = false;
          
            db.DONDATHANGs.Add(donhang);
            db.SaveChanges();
            // thêm vào chi tiết đơn hàng
            foreach (var sanpham in giohang)
            {
                CTDATHANG ct = new CTDATHANG();
                ct.SODH = donhang.SODH;
                ct.MaSP = sanpham.Masp;
                ct.Soluong = sanpham.SoLuong;
                ct.Dongia = (int)TinhTongTien();
                db.CTDATHANGs.Add(ct);
            }
            db.SaveChanges();
            //xóa giỏ hàng
            Session["GioHang"] = null;
            return View("HoanThanhDonHang");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var itemList = new ItemList
            {
                items = new List<Item>
                {
                    new Item
                    {
                        name = "Item Name",
                        currency = "USD",
                        price = TinhTongTienvnd().ToString(),
                        quantity = "1",
                        sku = "sku"
                    }
                }
            };

            var payer = new Payer
            {
                payment_method = "paypal"
            };

            var redirUrls = new RedirectUrls
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            var details = new Details
            {
                tax = "0",
                shipping = "0",
                subtotal = TinhTongTienvnd().ToString()
            };

            var amount = new Amount
            {
                currency = "USD",
                total = TinhTongTienvnd().ToString(), // Tổng giá trị thanh toán trong USD
                details = details
            };

            var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    description = "Invoice",
                    invoice_number = Guid.NewGuid().ToString(),
                    amount = amount,
                    item_list = itemList
                }
            };

            var payment = new Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            return payment.Create(apiContext);
        }

    }
   
    
}