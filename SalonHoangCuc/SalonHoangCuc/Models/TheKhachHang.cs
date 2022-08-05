using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Models
{
    public class TheKhachHang
    {
    }
    public class TheKhachHangMaping
    {
        public int ID { get; set; }
        public string MaThe { get; set; }
        public int LoaiThe { get; set; }
        public string SoDu { get; set; }
        public int ID_DichVu { get; set; }
        public int ID_KhachHang { get; set; }
        public DateTime? HanThe { get; set; }
    }
}