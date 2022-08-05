using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongViecGiaDinh.Models
{
    public class NhomDichVu
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Tên nhóm dịch vụ")]
        [Required(ErrorMessage = "Tên nhóm dịch vụ không được để trống")]
        public string TenNhomDichVu { get; set; }
    }

    public class NhomDichVuMaping
    {
        public int ID { get; set; }
        public string TenNhomDichVu { get; set; }
    }
}