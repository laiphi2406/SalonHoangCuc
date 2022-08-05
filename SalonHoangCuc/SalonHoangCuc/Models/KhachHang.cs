using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Models
{
    public class KhachHang
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Mã khách hàng")]
        public string TenKhachHang { get; set; }
        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SoTienDaChiTieu { get; set; }
        [Display(Name = "Số tiền đã chi tiêu")]
        public DateTime? NgaySua { get; set; }
        [Display(Name = "Ngày sửa")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public bool GioiTinh { get; set; }
        public bool IsDelete { get; set; }
    }

    public class KhachHangMaping
    {
        public int ID { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string SoTienDaChiTieu { get; set; }
        public bool GioiTinh { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? NgaySua { get; set; }
    }
}