using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Models
{
    public class ViecLam
    {
        public int ID { get; set; }
        public int? VieclamId { get; set; }
        public string TenCongViecThuongXuyen { get; set; }
        public string TenTieuChi { get; set; }
        public bool TrangThai { get; set; }
        public int Diem { get; set; }

    }
}