using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class LichSuPhongBanModel
    {
        public int MaLichSu { get; set; }
        public int MaNV { get; set; }
        public int MaPB { get; set; }
        public DateTime NgayChuyen { get; set; }
        public string GhiChu { get; set; }
    }
}