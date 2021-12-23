using QLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QLBH.Controllers
{
    public class GioHangController : Controller
    {
        private QLBHEntities db = new QLBHEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            List<CartItem> gioHang = Session["giohang"] as List<CartItem>;
            return View(gioHang);
        }

        public RedirectToRouteResult AddToCart(string maSP)
        {
            if(Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem>();
            }
            List<CartItem> gioHang = Session["giohang"] as List<CartItem>;
            if(gioHang.FirstOrDefault(m => m.MaSP == maSP) == null)
            {
                SanPham sp = db.SanPhams.Find(maSP);
                CartItem item = new CartItem();
                item.MaSP = maSP;
                item.TenSP = sp.TenSP;
                item.DonGia = Convert.ToDouble(sp.Dongia);
                item.SoLuong = 1;
                gioHang.Add(item);

            }
            else
            {
                CartItem item = gioHang.FirstOrDefault(m => m.MaSP == maSP);
                item.SoLuong++;
            }
            Session["giohang"] = gioHang;
            return RedirectToAction("Index");

        }

        public RedirectToRouteResult Update(string maSP, int txtSoLuong)
        {
            List<CartItem> gioHang = Session["giohang"] as List<CartItem>;

            CartItem item = gioHang.FirstOrDefault(m => m.MaSP == maSP);

            if(item != null)
            {
                item.SoLuong = txtSoLuong;
                Session["giohang"] = gioHang;
            }


            return RedirectToAction("Index");

        }

        public RedirectToRouteResult Delete(string maSP, int txtSoLuong)
        {
            List<CartItem> gioHang = Session["giohang"] as List<CartItem>;

            CartItem item = gioHang.FirstOrDefault(m => m.MaSP == maSP);

            if (item != null)
            {
                gioHang.Remove(item);
                Session["giohang"] = gioHang;
            }


            return RedirectToAction("Index");

        }

        public ActionResult Order(string Email, string Phone)
        {
            List<CartItem> gioHang = Session["giohang"] as List<CartItem>;
            string msg = "<html><body><table<caption>Thong tin dat hang</caption>";
            msg += "<tr><th>STT</th><th>Ten hang</th><th>So luong</th><th>Don gia</th><th></th>";
            int i = 0;
            double tongtien = 0;
            foreach(var item in gioHang)
            {
                i++;
                msg += "<tr>";
                msg += "<td>"+ i.ToString() +"</td>";
                msg += "<td>" + item.TenSP +"</td>";
                msg += "<td>" + item.SoLuong + "</td>";
                msg += "<td>" + item.DonGia + "</td>";
                msg += "<td>" + item.ThanhTien.ToString("#,###") + "</td>";
                msg += "<td>" + item.SoLuong.ToString("#,###") + "</td>";
                msg += "<tr>";
                tongtien += item.ThanhTien;
            }

            msg += "<tr><th colspan='5'> Tong cong" + tongtien.ToString("#,###") + "</th></tr>";
            msg += "</table></body></html>";

            MailMessage mail = new MailMessage("vongovantien@gmail.com", Email, "Thong tin dat hang", msg );
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("vongovantien", "vongovantien30820");
            mail.IsBodyHtml = true;
            client.Send(mail);
            return RedirectToAction("Index", "Home");

        }
    }
}