using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBH.Models;
using System.Collections;
     
namespace QLBH.Controllers
{
    public class MenuController : Controller
    {
        private QLBHEntities db = new QLBHEntities();
        // GET: Menu
        public ActionResult Index()
        {
            var loaiSP = db.LoaiSPs.ToList();
            Hashtable arrLoaiSP = new Hashtable();
            foreach(var item in loaiSP)
            {
                arrLoaiSP.Add(item.MaLoaiSP, item.TenLoaiSP);
            }
            ViewBag.LoaiSP = arrLoaiSP;
            return PartialView("Menu");
        }
    }
}