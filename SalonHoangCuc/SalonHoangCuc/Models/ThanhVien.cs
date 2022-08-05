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
    public class ThanhVien
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Tên Tài Khoản")]
        public string TenTaiKhoan { get; set; }
        [Display(Name = "MatKhau")]
        public string MatKhau { get; set; }
        [Display(Name = "IDHoGiaDinh")]
        public int IDHoGiaDinh { get; set; }
        [Display(Name = "ChucDanhGiaDinh")]
        public int ChucDanhGiaDinh { get; set; }
        [Display(Name = "ChucDanhHeThong")]
        public int ChucDanhHeThong { get; set; }
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
        [Display(Name = "Role")]
        public int Role { get; set; }
        [Display(Name = "Trạng thái phê duyệt")]
        public bool PheDuyet { get; set; }
    }
}