using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class UngVienModel
    {
        public int MaUngVien { get; set; }
        public int MaUT { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string DuongDanCV { get; set; }
        public DateTime NgayUngTuyen { get; set; }
        public string TrangThai { get; set; }
    }
}