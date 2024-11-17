using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class ThuongPhatModel
    {
        public int MaTP { get; set; }
        public int MaNV { get; set; }
        public DateTime Ngay { get; set; }
        public string Loai { get; set; }
        public int SoTien { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
    }

}