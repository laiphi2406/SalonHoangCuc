using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongViecGiaDinh.Controllers
{
    public class HomeController : Controller
    {
        HoGiaDinhEntities _db = new HoGiaDinhEntities();
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        LamViecEntities _dbLV = new LamViecEntities();
        TieuChiEntities _dbTC = new TieuChiEntities();
        CongViecKhongThuongXuyenEntities _dbKTX = new CongViecKhongThuongXuyenEntities();
        CongViecThuongXuyenEntities _dbTX = new CongViecThuongXuyenEntities();
        TinhTpEntities ttp = new TinhTpEntities();
        ChucDanhEntities cdgd = new ChucDanhEntities();
        public ActionResult Index()
        {
            DataDashboard dataDashBoard = new DataDashboard();
            DateTime ngayDauThang = DateTime.Now.AddDays((-DateTime.Now.Day) + 1);
            var dtLV = _dbLV.LamViec.ToList();
            var dtTC = _dbTC.TieuChi.ToList();
            var dtTX = _dbTX.CongViecThuongXuyen.ToList();

            var lvc = dtLV.Where(t => t.IDHoGiaDinh == int.Parse(Session["idHoGiaDinh"].ToString()) && (t.ThoiGianTao >= ngayDauThang && t.ThoiGianTao <= DateTime.Now)).ToList();

            var datatest = from s in lvc
                           join st in dtTX //inner sequence 
                               on s.IDCongViecTX equals st.ID // key selector 
                           select new
                           {
                               ID = s.ID,
                               IDCongViec = st.ID,
                               TenCongViec = st.TenCongViec,
                               NgayThucHien = s.ThoiGianTao,
                               TieuChi = st.TieuChi,
                           };
            var dataCongViecThucHien = (from s in datatest
                                        join st in _dbTC.TieuChi.ToList() //inner sequence 
                                            on s.TieuChi equals st.ID // key selector 
                                        select new
                                        {
                                            ID = s.ID,
                                            IDCongViec = s.ID,
                                            TenCongViec = s.TenCongViec,
                                            NgayThucHien = s.NgayThucHien.ToString("dd"),
                                            TieuChi = s.TieuChi,
                                            TenTieuChi = st.TenTieuChi,
                                            Diem = st.Diem
                                        }).ToList().GroupBy(c => c.NgayThucHien).Select(l => new
                                        {
                                            NgayThucHien = l.First().NgayThucHien,
                                            Diem = l.Sum(c => c.Diem),
                                            SoCongViec = l.Count()

                                        }).OrderBy(v => v.NgayThucHien).ToList();


            int IDHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            List<string> soNgay = new List<string>();
            List<int> soDiemThang = new List<int>();
            List<int> soCongViecThang = new List<int>();
            //tạo tháng
            for (int i = 0; i < DateTime.Now.Day; i++)
            {
                soNgay.Add(i + 1 + "");
            }

            foreach (var item in soNgay)
            {
                var checkNgay = dataCongViecThucHien.Where(x => int.Parse(x.NgayThucHien) == int.Parse(item)).ToList();
                if (checkNgay.Count() > 0)
                {
                    soDiemThang.Add(checkNgay[0].Diem);
                    soCongViecThang.Add(checkNgay[0].SoCongViec);
                }
                else
                {
                    soDiemThang.Add(0);
                    soCongViecThang.Add(0);
                }
            }


            BieuDoDiem bieuDoDiem = new BieuDoDiem();
            bieuDoDiem.TieuDe = soNgay.ToArray();
            bieuDoDiem.IDHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            bieuDoDiem.DiemSo = soDiemThang.ToArray();

            BieuDoCongViec bieuDoCongViec = new BieuDoCongViec();
            bieuDoCongViec.TieuDe = soNgay.ToArray();
            bieuDoCongViec.IDHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
            bieuDoCongViec.CongViec = soCongViecThang.ToArray();

            //Where(t => t.ThoiGianTao >= DateTime.Now.AddHours(-DateTime.Now.Hour) && t.ThoiGianTao <= DateTime.Now)
            var dtCongViecTrongNgayDaLam = (from s in _dbLV.LamViec.Where(c => c.IDHoGiaDinh == IDHoGiaDinh).ToList()
                                            select new
                                            {
                                                id = s.ID,
                                                IdHoGiaDinh = s.IDHoGiaDinh,
                                                IDCongViecTX = s.IDCongViecTX,
                                                NgayThucHien = s.ThoiGianTao.ToString("dd/MM/yyyy")
                                            }).ToList();

            int dataCongViecTrongNgayDaLam = dtCongViecTrongNgayDaLam.Where(x => x.NgayThucHien == DateTime.Now.ToString("dd/MM/yyyy")).ToList().Count();
            int dataCongViecTrongNgayChuaLam = _dbTX.CongViecThuongXuyen.ToList().Count() - dataCongViecTrongNgayDaLam;

            BieuDoTron bieuDoTron = new BieuDoTron();
            bieuDoTron.TieuDe = new string[] { "Thanh toán bằng thẻ", "Thanh toán tiền mặt" , "Chuyển khoản"};
            bieuDoTron.CongViec = new int[] { 2, 3,4 };


            var dttest = (from d in dtLV
                          join c in dtTX on d.IDCongViecTX equals c.ID
                          join s in dtTC on c.TieuChi equals s.ID
                          select new
                          {
                              IdGiaDinh = d.IDHoGiaDinh,
                              Diem = s.Diem,
                          }).GroupBy(x => x.IdGiaDinh).Select(x => new
                          {
                              IdGiaDinh = x.First().IdGiaDinh,
                              Diem = x.Sum(c => c.Diem)
                          }).OrderByDescending(x => x.Diem).ToList();


            var top5db = dttest.Take(5);
            List<DanhSachTop5> danhSachTop5 = new List<DanhSachTop5>();
            var dataHoGiaDinh = (from s in _db.HoGiaDinh select s).ToList();
            int ivt = 1;
            foreach (var item in top5db)
            {
                DanhSachTop5 danhSachTop51 = new DanhSachTop5();
                danhSachTop51.IDHoGiaDinh = item.IdGiaDinh;
                danhSachTop51.SoDiemHienTai = item.Diem;
                danhSachTop51.ViTri = ivt;
                danhSachTop51.TenHoGiaDinh = dataHoGiaDinh.Find(x => x.ID == item.IdGiaDinh)?.TenHoGiaDinh;
                danhSachTop51.SoThanhVien = dataHoGiaDinh.Find(x => x.ID == item.IdGiaDinh) != null ? dataHoGiaDinh.Find(x => x.ID == item.IdGiaDinh).SoLuongThanhVien : 0;
                danhSachTop5.Add(danhSachTop51);
                ivt++;
            }


            //select list công việc

            //var dtDB = db.LamViec.Where(c => c.IDHoGiaDinh == idHoGiaDinh).Select(s => new { IdHoGiaDinh = s.IDHoGiaDinh, IDCongViecTX = s.IDCongViecTX, NgayThucHien = s.ThoiGianTao.ToString("dd/MM/yyyy") }).ToList();
            var dtDB = (from s in _dbLV.LamViec.Where(c => c.IDHoGiaDinh == IDHoGiaDinh).ToList()
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
                vl_item.Diem = item.Diem;
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



            //ghép data

            dataDashBoard.BieuDoCongViec = bieuDoCongViec;
            dataDashBoard.ViecLams = viecLam_lst;
            dataDashBoard.BieuDoDiem = bieuDoDiem;
            dataDashBoard.DanhSachTop5s = danhSachTop5;
            dataDashBoard.BieuDoTron = bieuDoTron;
            dataDashBoard.ViTriCuaBan = dttest.FindIndex(x => x.IdGiaDinh == int.Parse(Session["idHoGiaDinh"].ToString())) + 1;
            return View("~/Views/Home/Index.cshtml", dataDashBoard);
        }

        [HttpPost]
        public string BieuDoDiem(int checkThang)
        {
            BieuDoDiem bieuDoDiem = new BieuDoDiem();
            if (checkThang == 1)
            {
                List<string> soNgay = new List<string>();
                List<int> soDiemThang = new List<int>();
                DateTime ngayDauThang = DateTime.Now.AddDays((-DateTime.Now.Day) + 1);
                var dtLV = _dbLV.LamViec.ToList();
                var dtTC = _dbTC.TieuChi.ToList();
                var dtTX = _dbTX.CongViecThuongXuyen.ToList();

                var lvc = dtLV.Where(t => t.IDHoGiaDinh == int.Parse(Session["idHoGiaDinh"].ToString()) && (t.ThoiGianTao >= ngayDauThang && t.ThoiGianTao <= DateTime.Now)).ToList();

                var datatest = from s in lvc
                               join st in dtTX //inner sequence 
                                   on s.IDCongViecTX equals st.ID // key selector 
                               select new
                               {
                                   ID = s.ID,
                                   IDCongViec = st.ID,
                                   TenCongViec = st.TenCongViec,
                                   NgayThucHien = s.ThoiGianTao,
                                   TieuChi = st.TieuChi,
                               };
                for (int i = 0; i < DateTime.Now.Day; i++)
                {
                    soNgay.Add(i + 1 + "");
                }
                var dataCongViecThucHien = (from s in datatest
                                            join st in _dbTC.TieuChi.ToList() //inner sequence 
                                                on s.TieuChi equals st.ID // key selector 
                                            select new
                                            {
                                                ID = s.ID,
                                                IDCongViec = s.ID,
                                                TenCongViec = s.TenCongViec,
                                                NgayThucHien = s.NgayThucHien.ToString("dd"),
                                                TieuChi = s.TieuChi,
                                                TenTieuChi = st.TenTieuChi,
                                                Diem = st.Diem
                                            }).ToList().GroupBy(c => c.NgayThucHien).Select(l => new
                                            {
                                                NgayThucHien = l.First().NgayThucHien,
                                                Diem = l.Sum(c => c.Diem),
                                                SoCongViec = l.Count()

                                            }).OrderBy(v => v.NgayThucHien).ToList();
                foreach (var item in soNgay)
                {
                    var checkNgay = dataCongViecThucHien.Where(x => int.Parse(x.NgayThucHien) == int.Parse(item)).ToList();
                    if (checkNgay.Count() > 0)
                    {
                        soDiemThang.Add(checkNgay[0].Diem);
                    }
                    else
                    {
                        soDiemThang.Add(0);
                    }
                }

                bieuDoDiem.TieuDe = soNgay.ToArray();
                bieuDoDiem.DiemSo = soDiemThang.ToArray();
            }
            else
            {
                List<string> soThang = new List<string>();
                for (int i = 0; i < DateTime.Now.Month; i++)
                {
                    soThang.Add(i + 1 + "");
                }
                int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
                // danh sách công việc
                var dtLV = (_dbLV.LamViec.Where(x => x.IDHoGiaDinh == idHoGiaDinh && x.IDCongViecTX != null).Select(s => new { s.ID, s.IDHoGiaDinh, s.ThoiGianTao.Year, s.ThoiGianTao.Month, s.IDCongViecTX }).ToList()).Where(x => x.Year == DateTime.Now.Year).ToList();
                var dtTC = _dbTC.TieuChi.ToList();
                var dtTX = _dbTX.CongViecThuongXuyen.ToList();

                //lấy tiêu chí và điểm
                var dttest = (from d in dtLV
                              join c in dtTX on d.IDCongViecTX equals c.ID
                              join s in dtTC on c.TieuChi equals s.ID
                              select new
                              {
                                  IdGiaDinh = d.IDHoGiaDinh,
                                  Thang = d.Month,
                                  Diem = s.Diem,
                              }).GroupBy(x => x.Thang).Select(x => new
                              {
                                  Thang = x.First().Thang,
                                  Diem = x.Sum(c => c.Diem)
                              }).OrderBy(x => x.Thang).ToList();
                List<int> listDiem = new List<int>();
                foreach (var item in soThang)
                {
                    var xxx = dttest.Where(x => x.Thang == int.Parse(item)).ToList();
                    if (xxx.Count > 0)
                    {
                        listDiem.Add(xxx[0].Diem);
                    }
                    else
                    {
                        listDiem.Add(0);
                    }
                }
                bieuDoDiem.TieuDe = soThang.ToArray();
                bieuDoDiem.DiemSo = listDiem.ToArray();
            }
            return JsonConvert.SerializeObject(bieuDoDiem);
        }

        [HttpPost]
        public string BieuDoSoLuong(int checkThang)
        {
            BieuDoCongViec bieuDoCongViec = new BieuDoCongViec();
            if (checkThang == 1)
            {
                List<string> soNgay = new List<string>();
                List<int> soCongViecThang = new List<int>();
                DateTime ngayDauThang = DateTime.Now.AddDays((-DateTime.Now.Day) + 1);
                var dtLV = _dbLV.LamViec.ToList();

                var lvc = dtLV.Where(t => t.IDHoGiaDinh == int.Parse(Session["idHoGiaDinh"].ToString()) && (t.ThoiGianTao >= ngayDauThang && t.ThoiGianTao <= DateTime.Now)).ToList();
                var dttest = (from s in lvc
                              select new
                              {
                                  NgayThucHien = s.ThoiGianTao.ToString("dd"),
                                  IDCongViec = s.IDCongViecTX
                              }).ToList().GroupBy(c => c.NgayThucHien).Select(l => new { NgayThucHien = l.First().NgayThucHien, SoCongViec = l.Count()}).ToList();
                for (int i = 0; i < DateTime.Now.Day; i++)
                {
                    soNgay.Add(i + 1 + "");
                }
                foreach (var item in soNgay)
                {
                    var checkNgay = dttest.Where(x => int.Parse(x.NgayThucHien) == int.Parse(item)).ToList();
                    if (checkNgay.Count() > 0)
                    {
                        soCongViecThang.Add(checkNgay[0].SoCongViec);
                    }
                    else
                    {
                        soCongViecThang.Add(0);
                    }
                }

                bieuDoCongViec.TieuDe = soNgay.ToArray();
                bieuDoCongViec.CongViec = soCongViecThang.ToArray();
            }
            else
            {
                List<string> soThang = new List<string>();
                for (int i = 0; i < DateTime.Now.Month; i++)
                {
                    soThang.Add(i + 1 + "");
                }
                int idHoGiaDinh = int.Parse(Session["idHoGiaDinh"].ToString());
                // danh sách công việc
                var dtLV = (_dbLV.LamViec.Where(x => x.IDHoGiaDinh == idHoGiaDinh && x.IDCongViecTX != null).Select(s => new { s.ID, s.IDHoGiaDinh, s.ThoiGianTao.Year, s.ThoiGianTao.Month, s.IDCongViecTX }).ToList()).Where(x => x.Year == DateTime.Now.Year).ToList();

                //lấy tiêu chí và điểm
                var dttest = (from d in dtLV
                              select new
                              {
                                  Thang = d.Month
                              }).GroupBy(x => x.Thang).Select(x => new
                              {
                                  Thang = x.First().Thang,
                                  SoLuong = x.Count()
                              }).OrderBy(x => x.Thang).ToList();
                List<int> listDiem = new List<int>();
                foreach (var item in soThang)
                {
                    var xxx = dttest.Where(x => x.Thang == int.Parse(item)).ToList();
                    if (xxx.Count > 0)
                    {
                        listDiem.Add(xxx[0].SoLuong);
                    }
                    else
                    {
                        listDiem.Add(0);
                    }
                }
                bieuDoCongViec.TieuDe = soThang.ToArray();
                bieuDoCongViec.CongViec = listDiem.ToArray();
            }
            return JsonConvert.SerializeObject(bieuDoCongViec);
        }
        public ActionResult About()
        {
            return PartialView("Index");
        }

        public ActionResult Profile()
        {
            return View("~/Views/Page/Profile.cshtml");
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult AddHoGiaDinh()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewHoGiaDinh(HoGiaDinh model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
            }
            return View();
        }

        [HttpGet]
        public ActionResult ValidateUserID(string id)
        {
            bool superficialCheck = true;

            return Json(new { success = superficialCheck },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Registration()
        {
            ViewBag.ChucDanh = (from l in cdgd.ChucDanh
                              select l).OrderBy(x => x.ID).ToList();
            return View("~/Views/Login/Registration.cshtml");
        }
    }
}