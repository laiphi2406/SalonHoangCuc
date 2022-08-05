using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongViecGiaDinh.Controllers
{
    public class BangTinController : Controller
    {
        BangTinEntities _db = new BangTinEntities();
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        HoGiaDinhEntities _dbHGD = new HoGiaDinhEntities();
        // GET: BangTin
        public ActionResult Index()
        {
           List<BangTinMaping> bangTinMaping = new List<BangTinMaping>();
            var dataBangTin = (from s in _db.BangTin select s).OrderByDescending(s=> s.ThoiGianTao).ToList();
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
                bt.TenHoGiaDinh = hgd.TenHoGiaDinh;

                bangTinMaping.Add(bt);
            }
            return View("~/Views/BangTin/BangTin.cshtml", bangTinMaping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TieuDe,NoiDung,IDNguoiDang,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] BangTin bangTin)
        {
            if (ModelState.IsValid)
            {
                _db.BangTin.Add(bangTin);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bangTin);
        }
    }
}