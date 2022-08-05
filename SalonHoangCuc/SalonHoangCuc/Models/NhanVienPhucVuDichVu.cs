using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongViecGiaDinh.Models
{
    public class NhanVienPhucVuDichVu
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int IDChiTieHoaDon { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public int IDNhanVien { get; set; }
        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string IDDichVu { get; set; }
    }

    public class NhanVienPhucVuDichVuMaping
    {
        public int ID { get; set; }
        public int IDChiTieHoaDon { get; set; }
        public int IDNhanVien { get; set; }
        public string IDDichVu { get; set; }
    }
}