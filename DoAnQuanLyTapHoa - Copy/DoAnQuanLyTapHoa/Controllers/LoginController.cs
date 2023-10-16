using DoAnQuanLyTapHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace DoAnQuanLyTapHoa.Controllers
{
    public class LoginController : Controller
    {
        private QuanLyTapHoaFinalEntities1 db = new QuanLyTapHoaFinalEntities1();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAu(UserID userID)
        {
            var check = db.UserID.Where(m => m.MaUser == userID.MaUser).FirstOrDefault();
            int cost = 12;
            

            String ps = BCrypt.Net.BCrypt.HashString(userID.Password, cost);
            bool test = BCrypt.Net.BCrypt.Verify(userID.Password, ps);

            if (test == false && check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["TaiKhoan"] = userID.MaUser;
                Session["MatKhau"] = userID.Password;
                Session["RoleUser"] = userID.RoleUser;

                if (check.RoleUser /*!= null && userID.RoleUser.ToString().Trim()*/ == "admin")
                {

                    return RedirectToAction("Index", "TrangChu"); ;
                }

                return RedirectToAction("ProductList", "SanPhams");
            }
        }
        
        public ActionResult RegisterUser(UserID userID, String pass )
        {
            if (ModelState.IsValid)
            {
               

                var check_ID = db.UserID.Where(s => s.MaUser == userID.MaUser).FirstOrDefault();
                if (check_ID == null) //chua co id
                {
                    int cost = 12;
                    userID.Password = BCrypt.Net.BCrypt.HashString(userID.Password, cost);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    userID.RoleUser = "nv";
                    db.UserID.Add(userID);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Login");

                }
                else { ViewBag.ErrorRegister = " "; }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }

}
