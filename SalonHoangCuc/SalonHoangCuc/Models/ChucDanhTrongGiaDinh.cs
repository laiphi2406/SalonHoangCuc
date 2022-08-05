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
    public class ChucDanhTrongGiaDinh
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Tên chức danh")]
        public string TenChucDanh { get; set; }
    }
}