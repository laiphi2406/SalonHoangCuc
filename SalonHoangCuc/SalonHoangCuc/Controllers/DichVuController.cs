using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CongViecGiaDinh.Controllers
{
    public class DichVuController : Controller
    {
        Entities.DichVuEntities _db = new DichVuEntities();
        // GET: DichVu
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in _db.DichVu
                         select l).OrderBy(x => x.ID);
            List<DichVuMaping> DichVuMappinglst = new List<DichVuMaping>();

            foreach (var item in links)
            {
                DichVuMaping dichVuMapping = new DichVuMaping();
                dichVuMapping.ID = item.ID;
                dichVuMapping.TenDichVu = item.TenDichVu;
                dichVuMapping.TenTiengAnh = item.TenTiengAnh;
                dichVuMapping.Gia = item.Gia;
                dichVuMapping.ID_NhomDichVu = item.ID_NhomDichVu;

                DichVuMappinglst.Add(dichVuMapping);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(DichVuMappinglst.ToPagedList(pageNumber, pageSize));
        }
    }
}