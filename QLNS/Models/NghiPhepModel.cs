using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class NghiPhepModel
    {
        public int MaNghiPhep { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int TongNgay { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
    }

}