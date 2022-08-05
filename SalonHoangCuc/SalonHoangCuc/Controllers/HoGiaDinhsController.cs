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
using Newtonsoft.Json;
using PagedList;

namespace CongViecGiaDinh.Controllers
{
    public class HoGiaDinhsController : Controller
    {
        private HoGiaDinhEntities db = new HoGiaDinhEntities();
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        private TinhTpEntities ttp = new TinhTpEntities();
        private QuanHuyenEntities qh = new QuanHuyenEntities();
        private XaPhuongEntities xp = new XaPhuongEntities();
        private ChucDanhEntities cd = new ChucDanhEntities();
        private RoleEntities role = new RoleEntities();
        // GET: HoGiaDinhs
        public ActionResult Index(int? page)
        {
            var a = Session["role"].ToString();

            if (int.Parse(Session["role"].ToString()) == 1)
            {
                if (page == null) page = 1;
                var links = (from l in db.HoGiaDinh
                             select l).OrderBy(x => x.ID);
                List<HoGiaDinhMapping> HoGiaDinhMappinglst = new List<HoGiaDinhMapping>();
                var dataThanhVien = (from s in _dbTV.ThanhVien select s).ToList();
                foreach (var item in links)
                {
                    var tv = dataThanhVien.Where(t => t.ID == item.IDChuHo).FirstOrDefault();
                    HoGiaDinhMapping hoGiaDinhMapping = new HoGiaDinhMapping();
                    hoGiaDinhMapping.ID = item.ID;
                    hoGiaDinhMapping.DiaChi = item.DiaChi;
                    hoGiaDinhMapping.TenHoGiaDinh = item.TenHoGiaDinh;
                    hoGiaDinhMapping.IDChuHo = item.IDChuHo;
                    hoGiaDinhMapping.MaHoGiaDinh = item.MaHoGiaDinh;
                    hoGiaDinhMapping.ThoiGianTao = item.ThoiGianTao;
                    hoGiaDinhMapping.NguoiTao = item.NguoiTao;
                    hoGiaDinhMapping.SoDienThoai = item.SoDienThoai;
                    hoGiaDinhMapping.SoLuongThanhVien = item.SoLuongThanhVien;
                    hoGiaDinhMapping.TenChuHo = tv == null ? "" : tv.HoTen;

                    HoGiaDinhMappinglst.Add(hoGiaDinhMapping);
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(HoGiaDinhMappinglst.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return PartialView("~/Views/Login/Error404.cshtml");
            }
        }

        public ActionResult PheDuyetThanhVien(int? page)
        {
            var a = Session["ChuHo"].ToString();
            var dtTV = _dbTV.ThanhVien.ToList();
            var tv_lst = (from tv in dtTV select tv).ToList();
            if (int.Parse(Session["ChuHo"].ToString()) == 1)
            {
                if (page == null) page = 1;
                int IDHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
                var data = _dbTV.ThanhVien.Where(x => x.IDHoGiaDinh == IDHoGiaDinh && x.PheDuyet == false).ToList();

                var dataView = (from e in data
                                join d in cd.ChucDanh.ToList()
                                on e.ChucDanhGiaDinh equals d.ID into empDept
                                from ed in empDept.DefaultIfEmpty()
                                select new TenThanhVien
                                {
                                    ID = e.ID,
                                    Name = e.HoTen,
                                    SoDienThoai = e.SoDienThoai,
                                    TenChucDanh = ed == null ? "" : ed.TenChucDanh
                                }).ToList();

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(dataView.ToPagedList(pageNumber, pageSize));
            }
            // nếu là admin thì select ra phê duyệt thành viên
            else if (int.Parse(Session["role"].ToString()) == 1)
            {
                if (page == null) page = 1;
                var data = _dbTV.ThanhVien.Where(x => x.IDHoGiaDinh == 0 && x.PheDuyet == false).ToList();

                var dataView = (from e in data
                                join d in cd.ChucDanh.ToList()
                                on e.ChucDanhGiaDinh equals d.ID into empDept
                                from ed in empDept.DefaultIfEmpty()
                                select new TenThanhVien
                                {
                                    ID = e.ID,
                                    Name = e.HoTen,
                                    SoDienThoai = e.SoDienThoai,
                                    TenChucDanh = ed == null ? "" : ed.TenChucDanh
                                }).ToList();

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(dataView.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return PartialView("~/Views/Login/Error404.cshtml");
            }
        }

        public ActionResult ChiTietGiaDinh()
        {
            var dtHGD = db.HoGiaDinh.ToList();
            var dtCD = cd.ChucDanh.ToList();
            var dtTV = _dbTV.ThanhVien.ToList();
            var dtTTP = ttp.TinhTp.ToList();
            var dtQH = qh.QuanHuyens.ToList();
            var dtXP = xp.Xaphuong.ToList();
            var dtR = role.Roles.ToList();
            int iduser = int.Parse(Session["idUser"].ToString());
            //lấy ra thành viên theo iduser
            var tv_item = dtTV.Where(x => x.ID == iduser).FirstOrDefault();
            //lấy ra hộ ra đình theo IdHoGiaDinh của bảng thành viên
            var hgd_item = dtHGD.Where(x => x.ID == tv_item.IDHoGiaDinh).FirstOrDefault();
            //nếu thành viên ko ở hộ gia đình nào => select ra thông tin thành viên đó
            if (hgd_item == null)
            {
                var datatv = (from tv in dtTV
                              where tv.ID == iduser
                              select new ThanhVien()
                              {
                                  ID = tv.ID,
                                  HoTen = tv.HoTen,
                                  TenTaiKhoan = tv.TenTaiKhoan,
                                  SoDienThoai = tv.SoDienThoai,
                                  Role = tv.Role
                              }).FirstOrDefault();
                return View("~/Views/HoGiaDinhs/ChiTietThanhVien.cshtml", datatv);
            }

            //select ra thông tin thành viên trong gia đình
            List<TenThanhVien> tv_lst = new List<TenThanhVien>();
            var tv_lsts = (from hgd in dtHGD
                           join tv in dtTV on hgd.ID equals tv.IDHoGiaDinh
                           join cd in dtCD on tv.ChucDanhGiaDinh equals cd.ID
                           join r in dtR on tv.ChucDanhHeThong equals r.ID
                           where hgd_item.ID == tv.IDHoGiaDinh
                           select new
                           {
                               id = tv.ID,
                               name = tv.HoTen,
                               cd = cd.TenChucDanh,
                               roleName = r.RoleName
                           }).ToList();
            foreach (var item in tv_lsts)
            {
                TenThanhVien tentv = new TenThanhVien();
                tentv.ID = item.id;
                tentv.Name = item.name;
                tentv.TenChucDanh = item.cd;
                tentv.TenChucDanhHeThong = item.roleName;
                tv_lst.Add(tentv);

            }

            //select ra thông tin hô gia đình, thành viên trong gia đình
            var data = (from hgd in dtHGD
                        join tv in dtTV on hgd.ID equals tv.IDHoGiaDinh
                        //join cd in dtCD on tv.ChucDanhGiaDinh equals cd.ID
                        join ttp in dtTTP on hgd.ThanhPho equals ttp.ID
                        join qh in dtQH on hgd.QuanHuyen equals qh.ID
                        join xp in dtXP on hgd.XaPhuong equals xp.ID
                        where hgd_item.ID == tv.IDHoGiaDinh
                        select new ChiTietHoGiaDinh()
                        {
                            MaHoGiaDinh = hgd.MaHoGiaDinh,
                            //ChucDanh = cd.TenChucDanh,
                            DiaChi = hgd.DiaChi,
                            SoDienThoai = hgd.SoDienThoai,
                            SoThanhVien = hgd.SoLuongThanhVien,
                            TenGiaDinh = hgd.TenHoGiaDinh,
                            Tinh = ttp.Name,
                            Huyen = qh.Name,
                            Xa = xp.Name,
                            TenThanhViens = tv_lst
                        }).FirstOrDefault();
            return View(data);

        }
        // GET: HoGiaDinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoGiaDinh hoGiaDinh = db.HoGiaDinh.Find(id);
            if (hoGiaDinh == null)
            {
                return HttpNotFound();
            }
            return View(hoGiaDinh);
        }

        // GET: HoGiaDinhs/Create
        public ActionResult Create()
        {
            var dtTV = _dbTV.ThanhVien.ToList();
            var dtHGD = db.HoGiaDinh.ToList();
            ViewBag.TinhTp = (from l in ttp.TinhTp
                              select l).OrderBy(x => x.ID).ToList();

            //var hgd_lst = dtHGD.Select(x => x.IDChuHo).ToList();

            ViewBag.tv = dtTV.Where(x => x.IDHoGiaDinh == 0 && x.PheDuyet == true).ToList();

            return View();
        }
        //get quan_huyen by TinhTpId
        public string GetQuanHuyen(int TinhTpId)
        {
            var tinhtp = qh.QuanHuyens.Where(x => x.TinhTpId == TinhTpId).ToArray();
            return JsonConvert.SerializeObject(tinhtp);
        }
        //get xa_phuong by QuanHuyenId
        public string GetXaPhuong(int QuanHuyenId)
        {
            var xaphuong = xp.Xaphuong.Where(x => x.HuyenId == QuanHuyenId).ToArray();
            return JsonConvert.SerializeObject(xaphuong);
        }
        // POST: HoGiaDinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string TenHoGiaDinh, string MaHoGiaDinh, int SoLuongThanhVien, string DiaChi, string SoDienThoai, int IDChuHo, string ThanhPho, string QuanHuyen, string XaPhuong)
        {
            HoGiaDinh hoGiaDinh = new HoGiaDinh();
            hoGiaDinh.TenHoGiaDinh = TenHoGiaDinh;
            hoGiaDinh.MaHoGiaDinh = MaHoGiaDinh;
            hoGiaDinh.SoLuongThanhVien = SoLuongThanhVien;
            hoGiaDinh.DiaChi = DiaChi;
            hoGiaDinh.SoDienThoai = SoDienThoai;
            hoGiaDinh.IDChuHo = IDChuHo;
            hoGiaDinh.ThanhPho = int.Parse(ThanhPho);
            hoGiaDinh.QuanHuyen = int.Parse(QuanHuyen);
            hoGiaDinh.XaPhuong = int.Parse(XaPhuong);
            hoGiaDinh.NguoiTao = 3;
            hoGiaDinh.ThoiGianTao = DateTime.Now;
            hoGiaDinh.NguoiSua = 1;
            if (ModelState.IsValid)
            {
                var a = db.HoGiaDinh.Add(hoGiaDinh);
                var xxx = db.SaveChanges();
                if(xxx == 1)
                {
                    ThanhVien thanhvien = _dbTV.ThanhVien.Find(a.IDChuHo);
                    thanhvien.IDHoGiaDinh = a.ID;
                    _dbTV.Entry(thanhvien).State = EntityState.Modified;
                    _dbTV.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }

            return RedirectToAction("Create");
        }

        // GET: HoGiaDinhs/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoGiaDinh hoGiaDinh = db.HoGiaDinh.Find(id);
            if (hoGiaDinh == null)
            {
                return HttpNotFound();
            }
            return View(hoGiaDinh);
        }

        // POST: HoGiaDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenHoGiaDinh,MaHoGiaDinh,SoLuongThanhVien,DiaChi,SoDienThoai,IDChuHo,ThanhPho,QuanHuyen,XaPhuong,ThoiGianTao,NguoiTao,ThoiGianSua,NguoiSua,IsDelete")] HoGiaDinh hoGiaDinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoGiaDinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoGiaDinh);
        }

        // GET: HoGiaDinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoGiaDinh hoGiaDinh = db.HoGiaDinh.Find(id);
            if (hoGiaDinh == null)
            {
                return HttpNotFound();
            }
            return View(hoGiaDinh);
        }

        // POST: HoGiaDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoGiaDinh hoGiaDinh = db.HoGiaDinh.Find(id);
            db.HoGiaDinh.Remove(hoGiaDinh);
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
            HoGiaDinh hoGiaDinh = db.HoGiaDinh.Find(id);
            db.HoGiaDinh.Remove(hoGiaDinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTable(int? id)
        {
            ViewBag.TinhTp = (from l in ttp.TinhTp
                              select l).OrderBy(x => x.ID).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoGiaDinh hoGiaDinh = db.HoGiaDinh.Find(id);
            if (hoGiaDinh == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/HoGiaDinhs/Edit.cshtml", hoGiaDinh);
        }

        [HttpPost]
        public ActionResult SaveEditTable(int ID, int NguoiTao, DateTime? ThoiGianTao, string TenHoGiaDinh, string MaHoGiaDinh, int SoLuongThanhVien, string DiaChi, string SoDienThoai, int IDChuHo, string ThanhPho, string QuanHuyen, string XaPhuong)
        {
            HoGiaDinh hoGiaDinh = new HoGiaDinh();
            hoGiaDinh.ID = ID;
            hoGiaDinh.TenHoGiaDinh = TenHoGiaDinh;
            hoGiaDinh.MaHoGiaDinh = MaHoGiaDinh;
            hoGiaDinh.SoLuongThanhVien = SoLuongThanhVien;
            hoGiaDinh.DiaChi = DiaChi;
            hoGiaDinh.SoDienThoai = SoDienThoai;
            hoGiaDinh.IDChuHo = IDChuHo;
            hoGiaDinh.ThanhPho = int.Parse(ThanhPho);
            hoGiaDinh.QuanHuyen = int.Parse(QuanHuyen);
            hoGiaDinh.XaPhuong = int.Parse(XaPhuong);
            hoGiaDinh.NguoiTao = NguoiTao;
            hoGiaDinh.ThoiGianTao = ThoiGianTao;
            hoGiaDinh.NguoiSua = 1;
            hoGiaDinh.ThoiGianSua = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(hoGiaDinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoGiaDinh);
        }

        [HttpPost]
        public ActionResult XacNhanPheDuyetThanhVien(int? id)
        {
            ThanhVien thanhVien = _dbTV.ThanhVien.Where(i => i.ID == id).ToList().FirstOrDefault();
            thanhVien.PheDuyet = true;
            if (ModelState.IsValid)
            {
                _dbTV.Entry(thanhVien).State = EntityState.Modified;
                _dbTV.SaveChanges();
            }
            return RedirectToAction("PheDuyetThanhVien");
        }

    }
}
