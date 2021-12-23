using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBH.Models;
using System.Data.Entity;

namespace QLBH.Controllers
{
    public class HomeController : Controller
    {
        private QLBHEntities db = new QLBHEntities();
        public ActionResult Index(int maSP = 0)
        {
            if(maSP == 0)
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).ToList();
                return View(sanPhams);
            }
            else
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP).Where(s => s.MaLoaiSP == maSP).ToList();
                return View(sanPhams);
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}