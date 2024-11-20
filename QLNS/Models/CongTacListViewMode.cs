using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNS.Models
{
    public class CongTacListViewMode
    {
        public IEnumerable<CongTacModel> CongTacList { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}