using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongViecGiaDinh.Models
{
    public class HoaDon
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string TienGoc { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string ThueVAT { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string ChietKhauHoaDon { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string MaPOS { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string TongTien { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int TrangThaiHoaDon { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public DateTime ThoiGianLap { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public DateTime? ThoiGianThanhToan { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int NguoiLapHoaDon { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int LoaiThanhToan { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int IDKhachHang { get; set; }


    }

    public class HoaDonMaping
    { 
        public int ID { get; set; }
        public string TienGoc { get; set; }
        public string ThueVAT { get; set; }
        public string ChietKhauHoaDon { get; set; }
        public string MaPOS { get; set; }
        public string TongTien { get; set; }
        public int TrangThaiHoaDon { get; set; }
        public DateTime ThoiGianLap { get; set; }
        public DateTime? ThoiGianThanhToan { get; set; }
        public int NguoiLapHoaDon { get; set; }
        public int LoaiThanhToan { get; set; }
        public int IDKhachHang { get; set; }
    }
}