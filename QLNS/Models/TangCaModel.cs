using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class TangCaModel
    {
        public int MaTangCa { get; set; }
        public int MaNV { get; set; }
        public DateTime Ngay { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public float SoGio { get; set; }
        public float HeSo { get; set; }
        public string TrangThai { get; set; }
    }

}