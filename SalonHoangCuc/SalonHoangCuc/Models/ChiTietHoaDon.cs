using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongViecGiaDinh.Models
{
    public class ChiTietHoaDon
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int ID_DichVu { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int SoLuong { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string DonGia { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string TongTien { get; set; }
    }

    public class ChiTietHoaDonMaping
    {
        public int ID { get; set; }
        public int ID_DichVu { get; set; }
        public int SoLuong { get; set; }
        public string DonGia { get; set; }
        public string TongTien { get; set; }
    }
}