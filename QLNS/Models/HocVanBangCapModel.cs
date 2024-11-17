using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class HocVanBangCapModel
    {
        public int MaHVBC { get; set; }  
        public int MaNV { get; set; }  
        public string TenTruong { get; set; } 
        public string ChuyenNganh { get; set; }  
        public string BangCap { get; set; } 
        public int NamTotNghiep { get; set; }  
        public float DiemTB { get; set; } 
        public string HinhThucHoc { get; set; }  
        public string GhiChu { get; set; }
    }
}