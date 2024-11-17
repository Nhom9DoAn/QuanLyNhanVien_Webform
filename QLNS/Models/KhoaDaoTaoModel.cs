using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class KhoaDaoTaoModel
    {
        public int MaKhoaDaoTao { get; set; }
        public string TenKhoaHoc { get; set; }
        public string DonViDaoTao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int ChiPhi { get; set; }
        public string TrangThai { get; set; }
    }

}