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
    public class CongViecThuongXuyensController : Controller
    {
        private CongViecThuongXuyenEntities db = new CongViecThuongXuyenEntities();
        private TieuChiEntities dbb = new TieuChiEntities();
        // GET: CongViecThuongXuyens
        public ActionResult Index(int? page)
        {
            var a = Session["role"].ToString();

            if (int.Parse(Session["role"].ToString()) == 1)
            {
                if (page == null) page = 1;
                var links = (from l in db.CongViecThuongXuyen
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

        // GET: CongViecThuongXuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecThuongXuyen congViecThuongXuyen = db.CongViecThuongXuyen.Find(id);
            if (congViecThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View(congViecThuongXuyen);
        }

        // GET: CongViecThuongXuyens/Create
        public ActionResult Create()
        {
            ViewBag.Tieuchi = (from l in dbb.TieuChi
                         select l).OrderBy(x => x.ID);
            return View();
        }

        // POST: CongViecThuongXuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string MaCongViecThuongXuyen,string TenCongViec , int TieuChi)
        {
            CongViecThuongXuyen congViecThuongXuyen = new CongViecThuongXuyen();
            congViecThuongXuyen.MaCongViecThuongXuyen = MaCongViecThuongXuyen;
            congViecThuongXuyen.TenCongViec = TenCongViec;
            congViecThuongXuyen.TieuChi = TieuChi;
            congViecThuongXuyen.NguoiTao = 1;
            congViecThuongXuyen.ThoiGianTao = DateTime.Now;
            congViecThuongXuyen.NguoiSua = 1;
            if (ModelState.IsValid)
            {
                db.CongViecThuongXuyen.Add(congViecThuongXuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(congViecThuongXuyen);
        }

        // GET: CongViecThuongXuyens/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecThuongXuyen congViecThuongXuyen = db.CongViecThuongXuyen.Find(id);
            if (congViecThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View(congViecThuongXuyen);
        }

        // POST: CongViecThuongXuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaCongViecThuongXuyen,TenCongViec,TieuChi,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] CongViecThuongXuyen congViecThuongXuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congViecThuongXuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(congViecThuongXuyen);
        }

        // GET: CongViecThuongXuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecThuongXuyen congViecThuongXuyen = db.CongViecThuongXuyen.Find(id);
            if (congViecThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View(congViecThuongXuyen);
        }

        // POST: CongViecThuongXuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongViecThuongXuyen congViecThuongXuyen = db.CongViecThuongXuyen.Find(id);
            db.CongViecThuongXuyen.Remove(congViecThuongXuyen);
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
            CongViecThuongXuyen congViecThuongXuyen = db.CongViecThuongXuyen.Find(id);
            db.CongViecThuongXuyen.Remove(congViecThuongXuyen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTable(int? id)
        {
            ViewBag.Tieuchi = (from l in dbb.TieuChi
                               select l).OrderBy(x => x.ID);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecThuongXuyen congViecThuongXuyen = db.CongViecThuongXuyen.Find(id);
            if (congViecThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/CongViecThuongXuyens/Edit.cshtml", congViecThuongXuyen);
        }

        [HttpPost]
        public ActionResult SaveEdit(int ID, string MaCongViecThuongXuyen, string TenCongViec, int TieuChi, DateTime? ThoiGianTao)
        {
            CongViecThuongXuyen congViecThuongXuyen = new CongViecThuongXuyen();
            congViecThuongXuyen.ID = ID;
            congViecThuongXuyen.MaCongViecThuongXuyen = MaCongViecThuongXuyen;
            congViecThuongXuyen.TenCongViec = TenCongViec;
            congViecThuongXuyen.TieuChi = TieuChi;
            congViecThuongXuyen.NguoiTao = 1;
            congViecThuongXuyen.ThoiGianSua = DateTime.Now;
            congViecThuongXuyen.NguoiSua = 1;
            congViecThuongXuyen.ThoiGianTao = ThoiGianTao;
            if (ModelState.IsValid)
            {
                db.Entry(congViecThuongXuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(congViecThuongXuyen);
        }
    }
}
