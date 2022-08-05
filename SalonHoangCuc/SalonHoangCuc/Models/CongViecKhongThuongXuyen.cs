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
    public class CongViecKhongThuongXuyen
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Mã công việc không thường xuyên")]
        public string MaCongViecKhongThuongXuyen { get; set; }
        [Display(Name = "Tên công việc")]
        public string TenCongViec { get; set; }
        [Display(Name = "Người được giao")]
        public int IDNguoiDuocGiao { get; set; }
        [Display(Name = "Hộ gia đình")]
        public int? IDHoGiaDinh { get; set; }
        [Display(Name = "Thời gian tạo")]
        public DateTime? ThoiGianTao { get; set; }
        [Display(Name = "Người tạo")]
        public int? NguoiTao { get; set; }
        [Display(Name = "Thời gian sửa")]
        public DateTime? ThoiGianSua { get; set; }
        [Display(Name = "Người sửa")]
        public int? NguoiSua { get; set; }
        public bool IsDelete { get; set; }

        [Display(Name = "Ảnh Đã Tải Lên")]
        public string UrlImage { get; set; }
        [Display(Name = "Mô Tả Công Việc:")]
        public string Description { get; set; }
    }

    public class CongViecKhongThuongXuyenView
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Mã công việc không thường xuyên")]
        public string MaCongViecKhongThuongXuyen { get; set; }
        [Display(Name = "Tên công việc")]
        public string TenCongViec { get; set; }
        [Display(Name = "Người được giao")]
        public int IDNguoiDuocGiao { get; set; }
        [Display(Name = "Hộ gia đình")]
        public int? IDHoGiaDinh { get; set; }
        [Display(Name = "Thời gian tạo")]
        public DateTime? ThoiGianTao { get; set; }
        [Display(Name = "Người tạo")]
        public int? NguoiTao { get; set; }
        [Display(Name = "Thời gian sửa")]
        public DateTime? ThoiGianSua { get; set; }
        [Display(Name = "Người sửa")]
        public int? NguoiSua { get; set; }
        public bool TrangThai { get; set; }
        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }

        [Display(Name = "Ảnh Đã Tải Lên")]
        public string UrlImage { get; set; }
        [Display(Name = "Mô Tả Công Việc:")]
        public string Description { get; set; }
    }
}