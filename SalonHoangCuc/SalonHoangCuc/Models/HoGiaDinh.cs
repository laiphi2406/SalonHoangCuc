using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Models
{
    public class HoGiaDinh
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Tên hộ gia đình")]
        public string TenHoGiaDinh { get; set; }
        [Display(Name = "Mã hộ gia đình")]
        public string MaHoGiaDinh { get; set; }
        [Display(Name = "Số lượng thành viên")]
        public int SoLuongThanhVien { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Chủ hộ")]
        public int IDChuHo { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public int ThanhPho { get; set; }
        [Display(Name = "Quận/Huyện")]
        public int QuanHuyen { get; set; }
        [Display(Name = "Xã/Phường")]
        public int XaPhuong { get; set; }
        [Display(Name = "Thời gian tạo")]
        public DateTime? ThoiGianTao { get; set; }
        [Display(Name = "Người tạo")]
        public int NguoiTao { get; set; }
        [Display(Name = "Thời gian sửa")]
        public DateTime? ThoiGianSua { get; set; }
        [Display(Name = "Người sửa")]
        public int NguoiSua { get; set; }
        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }
    }

    public class HoGiaDinhMapping
    {
        public int ID { get; set; }
        public string TenHoGiaDinh { get; set; }
        public string MaHoGiaDinh { get; set; }
        public int SoLuongThanhVien { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public int IDChuHo { get; set; }
        public string TenChuHo { get; set; }
        public int NguoiTao { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }

    public class BieuDoDiem
    {
        public int IDHoGiaDinh { get; set; }
        public string[] TieuDe { get; set; }
        public int[] DiemSo { get; set; }
    }

    public class BieuDoCongViec
    {
        public int IDHoGiaDinh { get; set; }
        public string[] TieuDe { get; set; }
        public int[] CongViec { get; set; }
    }

    public class BieuDoTron
    {
        public int IDHoGiaDinh { get; set; }
        public string[] TieuDe { get; set; }
        public int[] CongViec { get; set; }
    }

    public class DanhSachTop5
    {
        public int IDHoGiaDinh { get; set; }
        public string TenHoGiaDinh { get; set; }
        public int SoDiemHienTai { get; set; }
        public int SoThanhVien { get; set; }
        public int ViTri { get; set; }
    }


    public class DataDashboard
    {
        public BieuDoDiem BieuDoDiem { get; set; }
        public BieuDoCongViec BieuDoCongViec { get; set; }
        public BieuDoTron BieuDoTron { get; set; }
        public List<ViecLam> ViecLams { get; set; }

        public List<DanhSachTop5> DanhSachTop5s { get; set; }
        public int ViTriCuaBan { get; set; }
    }
    public class ChiTietHoGiaDinh
    {
        [Display(Name = "Mã hộ gia đình")]
        public string MaHoGiaDinh { get; set; }
        [Display(Name = "Tên gia đình")]
        public string TenGiaDinh { get; set; }
        public List<TenThanhVien> TenThanhViens { get; set; }

        [Display(Name = "Số Lượng Thành Viên")]
        public int SoThanhVien { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Tỉnh/Thành Phố")]
        public string Tinh { get; set; }
        [Display(Name = "Quận/Huyện")]
        public string Huyen { get; set; }
        [Display(Name = "Xã/Phường")]
        public string Xa { get; set; }
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Chức Danh")]
        public string ChucDanh { get; set; }

    }
    public class TenThanhVien
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TenChucDanh { get; set; }
        public string TenChucDanhHeThong { get; set; }
        public string SoDienThoai { get; set; }
    }

}