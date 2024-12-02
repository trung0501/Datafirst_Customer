using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace De01.Models
{
    public class DLTimkiem
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string Diachi { get; set; }
        public string TenBao { get; set; }
        public DateTime Ngaydat { get; set; }
        public int Soluong { get; set; }
        public long Dongia { get; set; }

    }
}