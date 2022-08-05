using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Models
{
    public class DichVu
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Tên dịch vụ")]
        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        public string TenDichVu { get; set; }

        [Display(Name = "Tên tiếng anh")]
        [Required(ErrorMessage = "Tên tiếng anh không được để trống")]
        public string TenTiengAnh { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá không được để trống")]
        public string Gia { get; set; }


        [Display(Name = "Nhóm dịch vụ")]

        [Required(ErrorMessage = "Nhóm không được để trống")]
        public int ID_NhomDichVu { get; set; }
    }

    public class DichVuMaping
    {
        public int ID { get; set; }
        public string TenDichVu { get; set; }
        public string TenTiengAnh { get; set; }
        public string Gia { get; set; }
        public int ID_NhomDichVu { get; set; }
    }
}