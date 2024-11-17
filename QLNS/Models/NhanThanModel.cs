using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class NhanThanModel
    {
        public int MaNT { get; set; }
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string QuanHe { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
    }
}