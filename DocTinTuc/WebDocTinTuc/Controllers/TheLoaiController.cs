using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDocTinTuc.Controllers
{
    public class TheLoaiController : Controller
    {
        private PostDao dao = new PostDao();
        // GET: TheLoai
        public ActionResult Index(string UrlRequire, int page = 1, int pageSize = 10)
        {
            
            var model = dao.ListAllPostForType(UrlRequire, page, pageSize);
            //top 10 xem nhieu
            ViewBag._k_relative = new PostDao().top10VIew();

            //Lay ten the loai
            ViewBag.vbtentheloai = dao.theloaibaidangtentheloai(UrlRequire);
            return View(model);
        }
    }
}