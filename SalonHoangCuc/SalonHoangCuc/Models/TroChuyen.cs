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
    public class TroChuyen
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "IDHoGiaDinh")]
        public int IDHoGiaDinh { get; set; }
        [Display(Name = "IDNguoiGui")]
        public int IDNguoiGui { get; set; }
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string NoiDung { get; set; }        
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

    public class TroChuyenMaping
    {
        public string NoiDung { get; set; }
        public int IDNguoiDang { get; set; }
        public string TenNguoiGui { get; set; }
        public string TenHoGiaDinh { get; set; }
        public string IDHoGiaDinh { get; set; }
        public DateTime? ThoiGianTao { get; set; }
    }
}