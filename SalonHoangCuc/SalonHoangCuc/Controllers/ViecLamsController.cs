using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CongViecGiaDinh.Controllers
{
    public class ViecLamsController : Controller
    {
        private LamViecEntities db = new LamViecEntities();
        private CongViecThuongXuyenEntities cvtx = new CongViecThuongXuyenEntities();
        private TieuChiEntities tc = new TieuChiEntities();
        // GET: LamViecs
        public ActionResult Index(int? page)
        {
            int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            var dtTC = tc.TieuChi.ToList();
            var dtTX = cvtx.CongViecThuongXuyen.ToList();
            //var dtDB = db.LamViec.Where(c => c.IDHoGiaDinh == idHoGiaDinh).Select(s => new { IdHoGiaDinh = s.IDHoGiaDinh, IDCongViecTX = s.IDCongViecTX, NgayThucHien = s.ThoiGianTao.ToString("dd/MM/yyyy") }).ToList();
            var dtDB = (from s in db.LamViec.Where(c => c.IDHoGiaDinh == idHoGiaDinh).ToList()
                        select new
                        {
                            id = s.ID,
                            IdHoGiaDinh = s.IDHoGiaDinh,
                            IDCongViecTX = s.IDCongViecTX,
                            NgayThucHien = s.ThoiGianTao.ToString("dd/MM/yyyy")
                        }).ToList();
            //lay thoi gian dau ngay
            string dtDauNgay = DateTime.Now.ToString("dd/MM/yyyy");
            //lay cong viec da lam trong ngay
            var CVDL_lst = dtDB.Where(x => x.NgayThucHien == dtDauNgay).ToList();
            //maping tieu chi voi cong viec tx
            var ctCVTX_lst = (from tx in dtTX
                              join tc in dtTC on tx.TieuChi equals tc.ID
                              select new
                              {
                                  IDCongViec = tx.ID,
                                  TenCongViec = tx.TenCongViec,
                                  TenTieuChi = tc.TenTieuChi,
                                  Diem = tc.Diem,
                                  TrangThai = 0
                              }).ToList();
            // so sánh với danh sách công việc thường xuyên
            List<ViecLam> viecLam_lst = new List<ViecLam>();
            foreach (var item in ctCVTX_lst)
            {
                ViecLam vl_item = new ViecLam();
                var dtVl = CVDL_lst.Where(x => x.IDCongViecTX == item.IDCongViec).ToList();

                vl_item.ID = item.IDCongViec;
                vl_item.TenCongViecThuongXuyen = item.TenCongViec;
                vl_item.TenTieuChi = item.TenTieuChi;
                
                if (dtVl.Count() > 0)
                {
                    vl_item.TrangThai = true;
                    vl_item.VieclamId = dtVl.Select(x => x.id).FirstOrDefault();
                }
                else
                {
                    vl_item.TrangThai = false;
                }
                viecLam_lst.Add(vl_item);
            }

            //lay cong viec theo  ngay hien tai

            //var links = from lv in dtDB
            //            join cvt in dtTX on lv.IDCongViecTX equals cvt.ID
            //            join tcc in dtTC on cvt.TieuChi equals tcc.ID
            //            //where lv.ThoiGianTao >= DateTime.Now.AddHours(-DateTime.Now.Hour)
            //            select new { lv, cvt, tcc };
            //var data = links.Select(x => new ViecLam()
            //{
            //    ID = x.lv.ID,
            //    TenCongViecThuongXuyen = x.cvt.TenCongViec,
            //    TenTieuChi = x.tcc.TenTieuChi,
            //    TrangThai = x.lv.TrangThai
            //}).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(viecLam_lst.ToPagedList(pageNumber, pageSize));
            //return View(db.LamViec.ToList());
        }

        public ActionResult UploadImage(int id , string name)
        {
            LamViecc lamviec = new LamViecc();
            lamviec.IDCongViecTX = id;
            CongViecThuongXuyen congviectx = cvtx.CongViecThuongXuyen.Find(id);
            lamviec.TenCongViecTX = congviectx.TenCongViec;
            return View(lamviec);
        }
        //thêm mới ảnh và description cho phần làm việc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUploadImage(int IDCongViecTX, HttpPostedFileBase Image , string description)
        {

             LamViec lamviec = new LamViec();
            lamviec.IDHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            lamviec.IDCongViecTX = IDCongViecTX;
            lamviec.NguoiSua = 1;
            lamviec.NguoiTao = 1;
            lamviec.ThoiGianSua = DateTime.Now;
            lamviec.ThoiGianTao = DateTime.Now;
            lamviec.TrangThai = true;
            lamviec.Description = description;
            if(Image != null && Image.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(Image.FileName);
                string url = Server.MapPath("/Content/Images/" + fileName);
                Image.SaveAs(url);
                lamviec.UrlImage = "Images/" + fileName;
            }
            
            if (ModelState.IsValid)
            {
                db.LamViec.Add(lamviec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("uploadImage");
        }

        [HttpPost]
        public ActionResult Detail(int id , string name)
        {
            var dtLV = db.LamViec.ToList();
            var dtTX = cvtx.CongViecThuongXuyen.ToList();
            var data = (from lv in dtLV
                        join cvtx in dtTX on lv.IDCongViecTX equals cvtx.ID
                        where id == lv.ID
                        select new LamViecc()
                        {
                            IDCongViecTX = lv.IDCongViecTX,
                            TenCongViecTX = name,
                            Description = lv.Description,
                            UrlImage = lv.UrlImage
                        }).FirstOrDefault();
                  
          return View(data);
        }

    }
}

