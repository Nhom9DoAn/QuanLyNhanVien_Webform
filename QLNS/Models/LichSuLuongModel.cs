using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class LichSuLuongModel
    {
        public int MaLichSuLuong { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayThayDoi { get; set; }
        public string LoaiThayDoi { get; set; }
        public int SoTienThayDoi { get; set; }
        public int LuongCB { get; set; }
        public string GhiChu { get; set; }
    }
}