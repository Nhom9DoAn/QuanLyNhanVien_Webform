using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class NhanVienModel
    {
        public int MaNV { get; set; }
        public int MaPB { get; set; }
        public int MaCV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public int LuongCB { get; set; } = 0;
        public string TrangThai { get; set; }
        public string SoYeuLyLich { get; set; }
        public int NguoiQuanLy { get; set; }
        public byte[] Hinh { get; set; }
        public string TinhTrangHonNhan { get; set; }
        public string DanToc { get; set; }
        public int MaChamCong { get; set; }
    }
}