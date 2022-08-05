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

namespace CongViecGiaDinh.Controllers
{
    public class BangTinsTestController : Controller
    {
        private BangTinEntities _db = new BangTinEntities();
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        HoGiaDinhEntities _dbHGD = new HoGiaDinhEntities();

        // GET: BangTinsTest
        public ActionResult Index()
        {

            List<BangTinMaping> bangTinMaping = new List<BangTinMaping>();
            var dataBangTin = (from s in _db.BangTin select s).ToList();
            var dataThanhVien = (from s in _dbTV.ThanhVien select s).ToList();
            var dataHoGiaDinh = (from s in _dbHGD.HoGiaDinh select s).ToList();

            foreach (var item in dataBangTin)
            {
                var tv = dataThanhVien.Where(t => t.ID == item.IDNguoiDang).FirstOrDefault();
                var hgd = dataHoGiaDinh.Where(t => t.ID == tv.IDHoGiaDinh).FirstOrDefault();

                BangTinMaping bt = new BangTinMaping();
                bt.TieuDe = item.TieuDe;
                bt.NoiDung = item.NoiDung;
                bt.IDNguoiDang = item.IDNguoiDang;
                bt.ThoiGianTao = item.ThoiGianTao;
                bt.TenNguoiDang = tv.HoTen;
                bt.TenHoGiaDinh = hgd == null ? "" : hgd.TenHoGiaDinh;

                bangTinMaping.Add(bt);
            }

            return View(bangTinMaping);
        }

        // GET: BangTinsTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangTin bangTin = _db.BangTin.Find(id);
            if (bangTin == null)
            {
                return HttpNotFound();
            }
            return View(bangTin);
        }

        // GET: BangTinsTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BangTinsTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TieuDe,NoiDung,IDNguoiDang,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] BangTin bangTin)
        {
            bangTin.ThoiGianTao = DateTime.Now;
            bangTin.ThoiGianSua = DateTime.Now;

            Random rnd = new Random();
            bangTin.IDNguoiDang = rnd.Next(1, 4);
            bangTin.NguoiTao = rnd.Next(1, 4);
            bangTin.NguoiSua = rnd.Next(1, 4);

            if (ModelState.IsValid)
            {
                _db.BangTin.Add(bangTin);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bangTin);
        }

        // GET: BangTinsTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangTin bangTin = _db.BangTin.Find(id);
            if (bangTin == null)
            {
                return HttpNotFound();
            }
            return View(bangTin);
        }

        // POST: BangTinsTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TieuDe,NoiDung,IDNguoiDang,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] BangTin bangTin)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(bangTin).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bangTin);
        }

        // GET: BangTinsTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangTin bangTin = _db.BangTin.Find(id);
            if (bangTin == null)
            {
                return HttpNotFound();
            }
            return View(bangTin);
        }

        // POST: BangTinsTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BangTin bangTin = _db.BangTin.Find(id);
            _db.BangTin.Remove(bangTin);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
