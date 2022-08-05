using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using PagedList;

namespace CongViecGiaDinh.Controllers
{
    public class TieuChisController : Controller
    {
        private TieuChiEntities db = new TieuChiEntities();

        // GET: TieuChis
        public ActionResult Index(int? page)
        {
            var a = Session["role"].ToString();

            if (int.Parse(Session["role"].ToString()) == 1)
            {
                if (page == null) page = 1;
                var links = (from l in db.TieuChi
                             select l).OrderBy(x => x.ID);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(links.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return PartialView("~/Views/Login/Error404.cshtml");
            }
        }

        // GET: TieuChis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TieuChi tieuChi = db.TieuChi.Find(id);
            if (tieuChi == null)
            {
                return HttpNotFound();
            }
            return View(tieuChi);
        }

        // GET: TieuChis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TieuChis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string TenTieuChi, int Diem)
        {
            TieuChi tieuChi = new TieuChi();
            tieuChi.TenTieuChi = TenTieuChi;
            tieuChi.Diem = Diem;
            tieuChi.NguoiTao = 2;
            tieuChi.NguoiSua = 1;
            tieuChi.ThoiGianTao = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.TieuChi.Add(tieuChi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tieuChi);
        }

        // GET: TieuChis/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TieuChi tieuChi = db.TieuChi.Find(id);
            if (tieuChi == null)
            {
                return HttpNotFound();
            }
            return View(tieuChi);
        }

        // POST: TieuChis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string TenTieuChi, int Diem)
        {
            TieuChi tieuChi = new TieuChi();
            if (ModelState.IsValid)
            {
                db.Entry(tieuChi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tieuChi);
        }

        // GET: TieuChis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TieuChi tieuChi = db.TieuChi.Find(id);
            if (tieuChi == null)
            {
                return HttpNotFound();
            }
            return View(tieuChi);
        }

        // POST: TieuChis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TieuChi tieuChi = db.TieuChi.Find(id);
            db.TieuChi.Remove(tieuChi);
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
        [HttpPost]
        public ActionResult DeleteConfirmedTable(int id)
        {
            TieuChi tieuChi = db.TieuChi.Find(id);
            db.TieuChi.Remove(tieuChi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTable(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TieuChi tieuChi = db.TieuChi.Find(id);
            if (tieuChi == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/TieuChis/Edit.cshtml", tieuChi);
        }
        [HttpPost]
        public ActionResult SaveEditTable(int ID, int NguoiTao, DateTime? ThoiGianTao, string TenTieuChi, int Diem)
        {
            TieuChi tieuChi = new TieuChi();
            tieuChi.ID = ID;
            tieuChi.TenTieuChi = TenTieuChi;
            tieuChi.Diem = Diem;
            tieuChi.NguoiTao = NguoiTao;
            tieuChi.NguoiSua = 1;
            tieuChi.ThoiGianSua = DateTime.Now;
            tieuChi.ThoiGianTao = ThoiGianTao;
            if (ModelState.IsValid)
            {
                db.Entry(tieuChi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tieuChi);
        }
    }
}
