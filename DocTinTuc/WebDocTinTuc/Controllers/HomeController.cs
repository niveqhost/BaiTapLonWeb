using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDocTinTuc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag._KLastestNews = new PostDao().LastestNews();
            ViewBag._K_LastestNews25 = new PostDao().SecondToFourthNews();



            //Lấy các bài đăng thể loại tình yêu
            ViewBag.tinhyeu = new PostDao().postLove();

            //Lấy các bài đăng thể loại đời sống
            ViewBag.doisong = new PostDao().postDoiSong();

            //Lấy các bài đăng thể loại phap luat
            ViewBag.phapluat = new PostDao().postPhapLuat();
            //Lấy các bài đăng thể loại the thao
            ViewBag.thethao = new PostDao().postTheThao();
            //Lấy các bài đăng thể loại suc khoe
            ViewBag.suckhoe = new PostDao().postSucKhoe();

            //top 10 xem nhieu
            ViewBag._k_relative = new PostDao().top10VIew();


            return View();
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

        //Xử lý menu
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new CateDao().ListCate();
            return PartialView(model);
        }
    }
}