using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class CongTacModel
    {
        public int MaCT { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string DiaDiem { get; set; }
        public string MucDich { get; set; }
        public string BieuMau { get; set; }
        public string TrangThai { get; set; }
    }
}