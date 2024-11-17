using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class TGDaoTaoModel
    {
        public int MaNV { get; set; }
        public int MaKhoaDaoTao { get; set; }
        public DateTime NgayThamGia { get; set; }
        public string KetQua { get; set; }
        public string ChungChi { get; set; }
    }
}