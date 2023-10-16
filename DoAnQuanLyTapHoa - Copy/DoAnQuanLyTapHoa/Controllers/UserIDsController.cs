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
    public class UserIDsController : Controller
    {
        private QuanLyTapHoaFinalEntities1 db = new QuanLyTapHoaFinalEntities1();

        // GET: UserIDs
        public ActionResult Index()
        {
            return View(db.UserID.ToList());
        }

        // GET: UserIDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserID userID = db.UserID.Find(id);
            if (userID == null)
            {
                return HttpNotFound();
            }
            return View(userID);
        }

        // GET: UserIDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserIDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaUser,TenUser,RoleUser,Password")] UserID userID)
        {
            if (ModelState.IsValid)
            {
                db.UserID.Add(userID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userID);
        }

        // GET: UserIDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserID userID = db.UserID.Find(id);
            if (userID == null)
            {
                return HttpNotFound();
            }
            return View(userID);
        }

        // POST: UserIDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaUser,TenUser,RoleUser,Password")] UserID userID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userID).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userID);
        }

        // GET: UserIDs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserID userID = db.UserID.Find(id);
            if (userID == null)
            {
                return HttpNotFound();
            }
            return View(userID);
        }

        // POST: UserIDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserID userID = db.UserID.Find(id);
            db.UserID.Remove(userID);
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
