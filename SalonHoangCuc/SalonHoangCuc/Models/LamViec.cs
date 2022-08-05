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
    public class LamViec
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Hộ gia đình")]
        public int IDHoGiaDinh { get; set; }

        [Display(Name = "Công việc thường xuyên")]
        public int? IDCongViecTX { get; set; }

        [Display(Name = "Công việc không thường xuyên")]
        public int? IDCongViecKTX { get; set; }
        [Display(Name = "Trạng thái công việc")]
        public bool TrangThai { get; set; }

        [Display(Name = "Thời gian tạo")]
        public DateTime ThoiGianTao { get; set; }
        [Display(Name = "Người tạo")]
        public int NguoiTao { get; set; }
        [Display(Name = "Thời gian sửa")]
        public DateTime? ThoiGianSua { get; set; }
        [Display(Name = "Người sửa")]
        public int NguoiSua { get; set; }

        [Display(Name = "Upload ảnh làm việc")]
        public string UrlImage { get; set; }
        
        [Display(Name = "Mô Tả Công Việc")]
        public string Description { get; set; }
        
        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }
    }
    public class LamViecc
    {
        public int? IDCongViecTX { get; set; }
        [Display(Name = "Tên Công Việc Thường Xuyên:")]
        public string  TenCongViecTX { get; set; }

        [Display(Name = "Ảnh Đã Tải Lên")]
        public string UrlImage { get; set; }
        [Display(Name = "Mô Tả Công Việc:")]
        public string Description { get; set; }
    }
}