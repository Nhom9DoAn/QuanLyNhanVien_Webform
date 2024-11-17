using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class HopDongModel
    {
        public int MaHD { get; set; }
        public int MaNV { get; set; }
        public string LoaiHD { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string BieuMau { get; set; }
        public string TinhTrang { get; set; }
    }

}