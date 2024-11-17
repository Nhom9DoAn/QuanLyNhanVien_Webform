using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class ChamCongModel
    {
        public int MaChamCong { get; set; }
        public int MaNV { get; set; }
        public DateTime Ngay { get; set; }
        public TimeSpan GioVao { get; set; }
        public TimeSpan GioRa { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }

}