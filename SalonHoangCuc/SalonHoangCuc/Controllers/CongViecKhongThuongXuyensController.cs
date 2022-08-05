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
    public class CongViecKhongThuongXuyensController : Controller
    {
        private CongViecKhongThuongXuyenEntities db = new CongViecKhongThuongXuyenEntities();
        private LamViecEntities lv = new LamViecEntities();
        private ThanhVienEntities tv = new ThanhVienEntities();
        // GET: CongViecKhongThuongXuyens
        public ActionResult Index(int? page)
        {
            int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            var dtKTX = db.CongViecKhongThuongXuyen.ToList();
            var dtLV = lv.LamViec.ToList();
            var dtDB = dtKTX.Where(c => c.IDHoGiaDinh == idHoGiaDinh).ToList();
            // Lấy ra ds công việc không thường xuyên theo hộ gia đình
            List<CongViecKhongThuongXuyenView> viecLam_lst = new List<CongViecKhongThuongXuyenView>();
            foreach (var item in dtDB)
            {
                CongViecKhongThuongXuyenView cvktx_item = new CongViecKhongThuongXuyenView();

                cvktx_item.ID = item.ID;
                cvktx_item.TenCongViec = item.TenCongViec;
                cvktx_item.IDNguoiDuocGiao = item.IDNguoiDuocGiao;
                cvktx_item.MaCongViecKhongThuongXuyen = item.MaCongViecKhongThuongXuyen;
                if (string.IsNullOrEmpty(item.UrlImage) || string.IsNullOrEmpty(item.Description))
                {
                    cvktx_item.TrangThai = false;
                }
                else
                {
                    cvktx_item.TrangThai = true;
                }
                viecLam_lst.Add(cvktx_item);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(viecLam_lst.ToPagedList(pageNumber, pageSize));
            //return View(db.LamViec.ToList());
        }

        // GET: CongViecKhongThuongXuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecKhongThuongXuyen congViecKhongThuongXuyen = db.CongViecKhongThuongXuyen.Find(id);
            if (congViecKhongThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View(congViecKhongThuongXuyen);
        }

        // GET: CongViecKhongThuongXuyens/Create
        public ActionResult Create()
        {
            var dtTV = tv.ThanhVien.ToList();
            int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            ViewBag.ThanhVien = (from tv in dtTV
                                 where tv.IDHoGiaDinh == idHoGiaDinh
                                 select tv).ToList();
            return View();
        }

        // POST: CongViecKhongThuongXuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string MaCongViecKhongThuongXuyen, string TenCongViec, int IDNguoiDuocGiao)
        {
            int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            CongViecKhongThuongXuyen cv = new CongViecKhongThuongXuyen();
            cv.IDHoGiaDinh = idHoGiaDinh;
            cv.MaCongViecKhongThuongXuyen = MaCongViecKhongThuongXuyen;
            cv.TenCongViec = TenCongViec;
            cv.IDNguoiDuocGiao = IDNguoiDuocGiao;
            cv.NguoiTao = 1;
            cv.NguoiSua = 1;
            cv.ThoiGianTao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.CongViecKhongThuongXuyen.Add(cv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cv);
        }

        // GET: CongViecKhongThuongXuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecKhongThuongXuyen congViecKhongThuongXuyen = db.CongViecKhongThuongXuyen.Find(id);
            if (congViecKhongThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View(congViecKhongThuongXuyen);
        }

        // POST: CongViecKhongThuongXuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string MaCongViecThuongXuyen, string TenCongViec, int IDHoGiaDinh, int IDNguoiDuocGiao)
        {
            CongViecKhongThuongXuyen cv = new CongViecKhongThuongXuyen();
            cv.IDHoGiaDinh = IDHoGiaDinh;
            cv.MaCongViecKhongThuongXuyen = MaCongViecThuongXuyen;
            cv.TenCongViec = TenCongViec;
            cv.IDNguoiDuocGiao = IDNguoiDuocGiao;
            cv.NguoiTao = 1;
            cv.NguoiSua = 1;
            cv.ThoiGianSua = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(cv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cv);
        }

        // GET: CongViecKhongThuongXuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViecKhongThuongXuyen congViecKhongThuongXuyen = db.CongViecKhongThuongXuyen.Find(id);
            if (congViecKhongThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View(congViecKhongThuongXuyen);
        }

        // POST: CongViecKhongThuongXuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongViecKhongThuongXuyen congViecKhongThuongXuyen = db.CongViecKhongThuongXuyen.Find(id);
            db.CongViecKhongThuongXuyen.Remove(congViecKhongThuongXuyen);
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
            CongViecKhongThuongXuyen congViecKhongThuongXuyen = db.CongViecKhongThuongXuyen.Find(id);
            db.CongViecKhongThuongXuyen.Remove(congViecKhongThuongXuyen);
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
            CongViecKhongThuongXuyen congViecKhongThuongXuyen = db.CongViecKhongThuongXuyen.Find(id);
            if (congViecKhongThuongXuyen == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/CongViecKhongThuongXuyens/Edit.cshtml", congViecKhongThuongXuyen);
        }
        [HttpPost]
        public ActionResult SaveEdit(DateTime? ThoiGianTao, int ID, string MaCongVieckhongThuongXuyen, string TenCongViec, int IDHoGiaDinh, int IDNguoiDuocGiao)
        {
            CongViecKhongThuongXuyen cv = new CongViecKhongThuongXuyen();
            cv.ID = ID;
            cv.IDHoGiaDinh = IDHoGiaDinh;
            cv.MaCongViecKhongThuongXuyen = MaCongVieckhongThuongXuyen;
            cv.TenCongViec = TenCongViec;
            cv.IDNguoiDuocGiao = IDNguoiDuocGiao;
            cv.NguoiTao = 1;
            cv.NguoiSua = 1;
            cv.ThoiGianSua = DateTime.Now;
            cv.ThoiGianTao = ThoiGianTao;
            if (ModelState.IsValid)
            {
                db.Entry(cv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cv);
        }

        public ActionResult UploadImage(int id, string name)
        {
            CongViecKhongThuongXuyen cvktx = new CongViecKhongThuongXuyen();
            cvktx.ID = id;
            cvktx.TenCongViec = name;
            return View(cvktx);
        }
        //thêm mới ảnh và description cho phần làm việc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUploadImage(int ID, HttpPostedFileBase Image, string description)
        {
            CongViecKhongThuongXuyen congviecktx = db.CongViecKhongThuongXuyen.Find(ID);
            congviecktx.Description = description;
            if (Image != null && Image.ContentLength > 0)
            {
                string fileName = System.IO.Path.GetFileName(Image.FileName);
                string url = Server.MapPath("/Content/Images/" + fileName);
                Image.SaveAs(url);
                congviecktx.UrlImage = "Images/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(congviecktx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("uploadImage");
        }

        [HttpPost]
        public ActionResult Chitietcvdalam(int id, string name)
        {
            var dtCVKTX = db.CongViecKhongThuongXuyen.ToList();
            var data = (from cvktx in dtCVKTX
                        where id == cvktx.ID
                        select new CongViecKhongThuongXuyen()
                        {
                            ID = cvktx.ID,
                            TenCongViec = name,
                            Description = cvktx.Description,
                            UrlImage = cvktx.UrlImage
                        }).FirstOrDefault();

            return View(data);
        }
    }

}
