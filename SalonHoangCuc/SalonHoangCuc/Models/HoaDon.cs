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

        [Display(Name = "Tiền Gốc")]
        [Required(ErrorMessage = "Tiền Gốc không được để trống")]
        public string TienGoc { get; set; }

        [Display(Name = "Thuế VAT")]
        [Required(ErrorMessage = "Thuế VAT không được để trống")]
        public string ThueVAT { get; set; }

        [Display(Name = "Chiết khấu hóa đơn")]
        [Required(ErrorMessage = "Chiết khấu hóa đơn không được để trống")]
        public string ChietKhauHoaDon { get; set; }

        [Display(Name = "Mã POS")]
        [Required(ErrorMessage = "Mã POS không được để trống")]
        public string MaPOS { get; set; }

        [Display(Name = "Tổng tiền")]
        [Required(ErrorMessage = "Tổng Tiền không được để trống")]
        public string TongTien { get; set; }

        [Display(Name = "Trạng thái hóa đơn")]
        [Required(ErrorMessage = "Trạng thái hóa đơn không được để trống")]
        public int TrangThaiHoaDon { get; set; }

        [Display(Name = "Thời gian lập")]
        [Required(ErrorMessage = "Thời gian lập không được để trống")]
        public DateTime ThoiGianLap { get; set; }

        [Display(Name = "Thời gian thanh toán")]
        [Required(ErrorMessage = "Thời gian thanh toán không được để trống")]
        public DateTime? ThoiGianThanhToan { get; set; }

        [Display(Name = "Người lập hóa đơn")]
        [Required(ErrorMessage = "Người lập hóa đơn không được để trống")]
        public int NguoiLapHoaDon { get; set; }

        [Display(Name = "Loại thanh toán")]
        [Required(ErrorMessage = "Loại thanh toán không được để trống")]
        public int LoaiThanhToan { get; set; }

        [Display(Name = "ID khách hàng")]
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