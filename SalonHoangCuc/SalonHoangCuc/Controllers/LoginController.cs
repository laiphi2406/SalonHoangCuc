using CongViecGiaDinh.Entities;
using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CongViecGiaDinh.Controllers
{
    public class LoginController : Controller
    {
        ThanhVienEntities _dbTV = new ThanhVienEntities();
        HoGiaDinhEntities _db = new HoGiaDinhEntities();
        LamViecEntities _dbLV = new LamViecEntities();
        TieuChiEntities _dbTC = new TieuChiEntities();
        CongViecThuongXuyenEntities _dbTX = new CongViecThuongXuyenEntities();
        HoGiaDinhEntities _dbHGD = new HoGiaDinhEntities();
        // GET: Login
        public ActionResult Index()
        {
            Session.Clear();
            return View("~/Views/Login/Login.cshtml");
        }
        [HttpPost]
        public ActionResult CheckLogin(string username, string pass)
        {
            string a = "rpg2KA9o1Wy2hxsvhevylQ==";
            string passgiainen = Decrypt(a);

            string passMaHoa = Encrypt(pass);

            var data = _dbTV.ThanhVien.Where(t => t.TenTaiKhoan == username && t.MatKhau == passMaHoa && t.PheDuyet == true).ToList();
            DataDashboard dataDashBoard = new DataDashboard();
            if (data.Count > 0)
            {
                int idThanhVien = data[0].ID;
                if (_dbHGD.HoGiaDinh.Where(x => x.IDChuHo == idThanhVien).ToList().Count > 0)
                {
                    Session["ChuHo"] = 1;
                }
                else
                {
                    Session["ChuHo"] = 0;
                }
                Session["role"] = data[0].Role;
                Session["idHoGiaDinh"] = data[0].IDHoGiaDinh;   
                Session["idUser"] = data[0].ID;
                Session["chucdanh"] = data[0].ChucDanhGiaDinh;
                //lay thong tin bieu do 1 theo tháng
                DateTime ngayDauThang = DateTime.Now.AddDays((-DateTime.Now.Day) + 1);
                var dtLV = _dbLV.LamViec.ToList();
                var dtTC = _dbTC.TieuChi.ToList();
                var dtTX = _dbTX.CongViecThuongXuyen.ToList();

                var lvc = dtLV.Where(t => t.IDHoGiaDinh == int.Parse(data[0].IDHoGiaDinh.ToString()) && (t.ThoiGianTao >= ngayDauThang && t.ThoiGianTao <= DateTime.Now)).ToList();

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
                                            }).ToList().GroupBy(x => x.NgayThucHien).Select(x => new
                                            {
                                                NgayThucHien = x.First().NgayThucHien,
                                                Diem = x.Sum(c => c.Diem),
                                                SoCongViec = x.Count()

                                            }).OrderBy(x => x.NgayThucHien).ToList();


                int IDHoGiaDinh = data[0].IDHoGiaDinh;
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
                bieuDoDiem.IDHoGiaDinh = data[0].IDHoGiaDinh;
                bieuDoDiem.DiemSo = soDiemThang.ToArray();

                BieuDoCongViec bieuDoCongViec = new BieuDoCongViec();
                bieuDoCongViec.TieuDe = soNgay.ToArray();
                bieuDoCongViec.IDHoGiaDinh = data[0].IDHoGiaDinh;
                bieuDoCongViec.CongViec = soCongViecThang.ToArray();

                //Where(t => t.ThoiGianTao >= DateTime.Now.AddHours(-DateTime.Now.Hour) && t.ThoiGianTao <= DateTime.Now)
                var dt = DateTime.Now.AddHours(-DateTime.Now.Hour);

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
                bieuDoTron.TieuDe = new string[] { "Đã làm", "Chưa làm" };
                bieuDoTron.CongViec = new int[] { dataCongViecTrongNgayDaLam, dataCongViecTrongNgayChuaLam };


                var dttest = (from d in dtLV.Where(t => t.ThoiGianTao >= ngayDauThang && t.ThoiGianTao <= DateTime.Now).ToList()
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


                var top5db = dttest.Take(5).ToList();
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


                int Role = int.Parse(Session["Role"].ToString());
                ViewBag.role = Role;

                //ghép data

                dataDashBoard.BieuDoCongViec = bieuDoCongViec;
                dataDashBoard.BieuDoDiem = bieuDoDiem;
                dataDashBoard.ViecLams = viecLam_lst;
                dataDashBoard.DanhSachTop5s = danhSachTop5;
                dataDashBoard.BieuDoTron = bieuDoTron;
                dataDashBoard.ViTriCuaBan = dttest.FindIndex(x => x.IdGiaDinh == int.Parse(Session["idHoGiaDinh"].ToString())) + 1;
                return View("~/Views/Home/Index.cshtml", dataDashBoard);
            }
            else
            {
                return PartialView("~/Views/Login/Error404.cshtml");
            }
        }



        [HttpPost]
        public ActionResult RegistationAccount(string fullname, string phone, string username, string pass, string hodiadinh, int chucdanh)
        {
            var dtHoGiaDinh = _db.HoGiaDinh.Where(x => x.MaHoGiaDinh == hodiadinh).ToList();
            

            ThanhVien thanhVien = new ThanhVien();
            thanhVien.TenTaiKhoan = username;
            thanhVien.MatKhau = Encrypt(pass);
            thanhVien.HoTen = fullname;
            thanhVien.SoDienThoai = phone;
            thanhVien.ChucDanhHeThong = 2;
            thanhVien.Role = 2;
            thanhVien.ChucDanhGiaDinh = chucdanh;
            thanhVien.ThoiGianTao = DateTime.Now;
            thanhVien.ThoiGianSua = DateTime.Now;
            if (dtHoGiaDinh.Count > 0)
            {
                thanhVien.IDHoGiaDinh = dtHoGiaDinh.FirstOrDefault().ID;
            }
            else
            {
                thanhVien.IDHoGiaDinh = 0;
            }
            thanhVien.IsDelete = false;
            thanhVien.PheDuyet = false;
            _dbTV.ThanhVien.Add(thanhVien);
            _dbTV.SaveChanges();
            return RedirectToAction("Index");
        }

        //mã hóa
        public static string Encrypt(string _text)
        {
            string ThemMaHoa = "01010001";
            string MaHoa = "";
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(_text);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ThemMaHoa));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(ThemMaHoa);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                MaHoa = Convert.ToBase64String(resultArray, 0, resultArray.Length);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return MaHoa;
        }


        //giải mã
        public static string Decrypt(string toDecrypt)
        {
            string ThemMaHoa = "01010001";
            string Giaima = "";
            try
            {
                bool useHashing = true;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ThemMaHoa));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(ThemMaHoa);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                Giaima = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Giaima;
        }

    }

}