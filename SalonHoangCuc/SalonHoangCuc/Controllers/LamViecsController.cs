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
    public class LamViecsController : Controller
    {
        private LamViecEntities db = new LamViecEntities();

        // GET: LamViecs
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.LamViec
                         select l).OrderBy(x => x.ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
            //return View(db.LamViec.ToList());
        }

        // GET: LamViecs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LamViec lamViec = db.LamViec.Find(id);
            if (lamViec == null)
            {
                return HttpNotFound();
            }
            return View(lamViec);
        }

        // GET: LamViecs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LamViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int IDHoGiaDinh, int IDCongViecTX,int IDCongViecKTX)
        {
            LamViec lamviec = new LamViec();
            lamviec.IDHoGiaDinh = IDHoGiaDinh;
            lamviec.IDCongViecTX = IDCongViecTX;
            lamviec.IDCongViecKTX = IDCongViecKTX;
            lamviec.TrangThai = false;
            lamviec.NguoiTao = 1;
            lamviec.ThoiGianTao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.LamViec.Add(lamviec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lamviec);
        }

        // GET: LamViecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LamViec lamViec = db.LamViec.Find(id);
            if (lamViec == null)
            {
                return HttpNotFound();
            }
            return View(lamViec);
        }

        // POST: LamViecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDHoGiaDinh,IDCongViecTX,IDCongViecKTX,TrangThai,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] LamViec lamViec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lamViec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lamViec);
        }

        // GET: LamViecs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LamViec lamViec = db.LamViec.Find(id);
            if (lamViec == null)
            {
                return HttpNotFound();
            }
            return View(lamViec);
        }

        // POST: LamViecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LamViec lamViec = db.LamViec.Find(id);
            db.LamViec.Remove(lamViec);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public ActionResult DeleteConfirmedTable(int id)
        {
            LamViec lamViec = db.LamViec.Find(id);
            db.LamViec.Remove(lamViec);
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
            LamViec lamViec = db.LamViec.Find(id);
            if (lamViec == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/LamViecs/Edit.cshtml", lamViec);
        }
        [HttpPost]
        public ActionResult SaveEdit(int ID ,int IDHoGiaDinh, int IDCongViecTX, int IDCongViecKTX, bool TrangThai)
        {
            LamViec lamviec = new LamViec();
            lamviec.ID = ID;
            lamviec.IDHoGiaDinh = IDHoGiaDinh;
            lamviec.IDCongViecTX = IDCongViecTX;
            lamviec.IDCongViecKTX = IDCongViecKTX;
            lamviec.TrangThai = false;
            lamviec.NguoiSua = 1;
            lamviec.ThoiGianSua = DateTime.Now;
            lamviec.ThoiGianTao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(lamviec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lamviec);
        }
        
    }
}
