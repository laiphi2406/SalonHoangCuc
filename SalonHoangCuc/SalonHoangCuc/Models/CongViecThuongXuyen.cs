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
    public class CongViecThuongXuyen
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Mã công việc thường xuyên")]
        public string MaCongViecThuongXuyen { get; set; }
        [Display(Name = "Tên công việc")]
        public string TenCongViec { get; set; }
        [Display(Name = "Chọn Tiêu chí")]
        public int TieuChi { get; set; }
        [Display(Name = "Thời gian tạo")]
        public DateTime? ThoiGianTao { get; set; }
        [Display(Name = "Người tạo")]
        public int? NguoiTao { get; set; }
        [Display(Name = "Thời gian sửa")]
        public DateTime? ThoiGianSua { get; set; }
        [Display(Name = "Người sửa")]
        public int? NguoiSua { get; set; }
        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }
    }
}