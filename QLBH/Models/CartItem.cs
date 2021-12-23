using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH.Models
{
    public class CartItem
    {
        public String MaSP { get; set; }
        public String TenSP { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien
        {
            get
            {
                return this.SoLuong * this.DonGia;
            }
        }
    }
}