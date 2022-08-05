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
    public class HoaDonsController : Controller
    {
        HoaDonEntities _db = new HoaDonEntities();
        // GET: HoaDons
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in _db.HoaDon
                         select l).OrderBy(x => x.ID);
            List<HoaDonMaping> HoaDonMappinglst = new List<HoaDonMaping>();

            foreach (var item in links)
            {
                HoaDonMaping hoaDonMapping = new HoaDonMaping();
                hoaDonMapping.ID = item.ID;
                hoaDonMapping.TienGoc = item.TienGoc;
                hoaDonMapping.ThueVAT = item.ThueVAT;
                hoaDonMapping.ChietKhauHoaDon = item.ChietKhauHoaDon;
                hoaDonMapping.MaPOS = item.MaPOS;
                hoaDonMapping.TongTien = item.TongTien;
                hoaDonMapping.TrangThaiHoaDon = item.TrangThaiHoaDon;
                hoaDonMapping.ThoiGianLap = item.ThoiGianLap;
                hoaDonMapping.ThoiGianThanhToan = item.ThoiGianThanhToan;
                hoaDonMapping.NguoiLapHoaDon = item.NguoiLapHoaDon;
                hoaDonMapping.LoaiThanhToan = item.LoaiThanhToan;
                hoaDonMapping.IDKhachHang = item.IDKhachHang;

                HoaDonMappinglst.Add(hoaDonMapping);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(HoaDonMappinglst.ToPagedList(pageNumber, pageSize));
        }
    }
}