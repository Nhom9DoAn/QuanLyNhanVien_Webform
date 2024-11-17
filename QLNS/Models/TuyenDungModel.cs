using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class TuyenDungModel
    {
        public int MaUT { get; set; }
        public string TenViTri { get; set; }
        public int MaPhong { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string YeuCau { get; set; }
        public int NguoiPhuTrach { get; set; }
    }
}