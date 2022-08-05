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
    public class TroChuyensController : Controller
    {
        private TroChuyenEntities db = new TroChuyenEntities();
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        HoGiaDinhEntities _dbHGD = new HoGiaDinhEntities();

        // GET: TroChuyens
        public ActionResult Index()
        {
            List<TroChuyenMaping> troChuyenMaping = new List<TroChuyenMaping>();
            int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            int iduser = int.Parse(Session["idUser"].ToString());
            ViewBag.iduser = iduser;
            var dataTroChuyen = (from s in db.TroChuyen.Where(x => x.IDHoGiaDinh == idHoGiaDinh) select s).ToList();
            var dataThanhVien = (from s in _dbTV.ThanhVien select s).ToList();
            var dataHoGiaDinh = (from s in _dbHGD.HoGiaDinh select s).ToList();

            foreach (var item in dataTroChuyen)
            {
                var tv = dataThanhVien.Where(t => t.ID == item.IDNguoiGui).FirstOrDefault();
                var hgd = dataHoGiaDinh.Where(t => t.ID == tv.IDHoGiaDinh).FirstOrDefault();

                TroChuyenMaping tc = new TroChuyenMaping();
                tc.NoiDung = item.NoiDung;
                tc.IDNguoiDang = item.IDNguoiGui;
                tc.ThoiGianTao = item.ThoiGianTao;
                tc.TenNguoiGui = tv.HoTen;
                tc.TenHoGiaDinh = hgd == null? "" : hgd.TenHoGiaDinh;

                troChuyenMaping.Add(tc);
            }

            return View(troChuyenMaping);
        }

        // GET: TroChuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TroChuyen troChuyen = db.TroChuyen.Find(id);
            if (troChuyen == null)
            {
                return HttpNotFound();
            }
            return View(troChuyen);
        }

        // GET: TroChuyens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TroChuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDHoGiaDinh,IDNguoiGui,NoiDung,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] TroChuyen troChuyen)
        {
            if (ModelState.IsValid)
            {
                db.TroChuyen.Add(troChuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(troChuyen);
        }

        // GET: TroChuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TroChuyen troChuyen = db.TroChuyen.Find(id);
            if (troChuyen == null)
            {
                return HttpNotFound();
            }
            return View(troChuyen);
        }

        // POST: TroChuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDHoGiaDinh,IDNguoiGui,NoiDung,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] TroChuyen troChuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(troChuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(troChuyen);
        }

        // GET: TroChuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TroChuyen troChuyen = db.TroChuyen.Find(id);
            if (troChuyen == null)
            {
                return HttpNotFound();
            }
            return View(troChuyen);
        }

        // POST: TroChuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TroChuyen troChuyen = db.TroChuyen.Find(id);
            db.TroChuyen.Remove(troChuyen);
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
        public ActionResult Creating(string noidung)
        {
            int iduser = int.Parse(Session["idUser"].ToString());
            int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            TroChuyen troChuyen = new TroChuyen();
            troChuyen.NguoiSua = 1;
            troChuyen.NguoiTao = 1;
            troChuyen.IDNguoiGui = iduser;
            troChuyen.IDHoGiaDinh = idHoGiaDinh;
            troChuyen.NoiDung = noidung;
            troChuyen.ThoiGianSua = DateTime.Now;
            troChuyen.ThoiGianTao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.TroChuyen.Add(troChuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(troChuyen);
        }
    }
}
