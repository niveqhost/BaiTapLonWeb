using Model.DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTinTuc.Common;

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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //Gửi thông tin về server và chờ xử lý
        [HttpPost]
        public ActionResult Register(TAIKHOAN tk)
        {
            if (ModelState.IsValid)
            {
                var DAO = new AccountDao();
                var passmd5 = Encryptor.MD5Hash(tk.MatKhau);
                tk.MatKhau = passmd5;
                tk.QuyenHan = "U";
                tk.TrangThaiNguoiDung = "bình thường";
                long id = DAO.addAccount(tk);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo tài khoản thất bại");
                }

            }
            return View("Index");
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