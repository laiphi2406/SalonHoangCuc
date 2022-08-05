using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongViecGiaDinh.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        BangTinEntities _db = new BangTinEntities();
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        HoGiaDinhEntities _dbHGD = new HoGiaDinhEntities();
        KhachHangEntities _dbKH = new KhachHangEntities();
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in _dbKH.KhachHang
                         select l).Where(x => x.IsDelete == false).OrderBy(x => x.ID);
            List<KhachHangMaping> KhachHangMappinglst = new List<KhachHangMaping>();
            foreach (var item in links)
            {
                KhachHangMaping khachHangEntities = new KhachHangMaping();
                khachHangEntities.ID = item.ID;
                khachHangEntities.SoDienThoai = item.SoDienThoai;
                khachHangEntities.TenKhachHang = item.TenKhachHang;
                khachHangEntities.SoTienDaChiTieu = item.SoTienDaChiTieu;
                khachHangEntities.NgaySua = item.NgaySua;
                khachHangEntities.GioiTinh = item.GioiTinh;
                khachHangEntities.IsDelete = item.IsDelete;

                KhachHangMappinglst.Add(khachHangEntities);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(KhachHangMappinglst.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult DeleteConfirmedTable(int id)
        {
            KhachHang khachHang = _dbKH.KhachHang.Find(id);
            khachHang.IsDelete = true;
            //_dbKH.KhachHang.(khachHang);
            _dbKH.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}