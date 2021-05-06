﻿using Model.DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTinTuc.Common;
using Model.EntityFramework;

namespace WebDocTinTuc.Areas.Admin.Controllers
{
    public class PostManagerController : BaseAdminController
    {
        // GET: Admin/PostManager
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new PostDao();
            var model = dao.ListAll(searchString, page, pageSize);
            ViewBag.searchstring = searchString;
            return View(model);
        }


        //Tất cả bài đăng chờ duyệt
        public ActionResult PostWaiting(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new PostDao();
            var model = dao.ListAllWaiting(searchString, page, pageSize);
            ViewBag.searchstring = searchString;
            return View(model);
        }



        [HttpGet]

        public ActionResult Create()
        {
            GetTypeForPost();
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BAIDANG bd)
        {
            if (ModelState.IsValid)
            {
                var DAO = new PostDao();
                bd.UrlRequire = RewriteURL.RewriteUrl(bd.TenBaiDang);
                bd.TrangThaiBaiDang = "chờ duyệt";
                int idtk = int.Parse(Session["USER_ID"].ToString());
                bd.IDTaiKhoan = idtk;
                bd.NgayDang = DateTime.Now;
                bd.IDBaiDang = 0;
                long idpost = DAO.CreatePost(bd);
            }
            SetAlert("Soạn bài đăng thành công", "thanhcong");
            return RedirectToAction("PostWaiting", "PostManager");
        }



        //CẬP NHẬT

        [HttpGet]
        public ActionResult UpdatePost(int id)
        {
            var dao = new PostDao();
            GetTypeForPost();
            var user = dao.ViewDetail(id);
            return View(user);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(BAIDANG tk)
        {
            if (ModelState.IsValid)
            {
                var DAO = new PostDao();
                //var passmd5 = Encryptor.MD5Hash(tk.MatKhau);
                //tk.MatKhau = passmd5;
                var result = DAO.updatePost(tk);
                if (result)
                {
                    return RedirectToAction("Index", "AccountManager");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }

            }
            return View("Index");
        }

        public ActionResult Preview(long id)
        {
            var dao = new PostDao();
            ViewBag._k_news = dao.NewsFindByID(id);
            //var dao1 = new ViewsDao();
            //dao1.AddViews(id);
            return View();
        }


        public ActionResult UnAcept(long id)
        {
            var dao = new PostDao();
            ViewBag._k_news = dao.NewsFindByID(id);
            return View();
        }


        //DUyệt bài
        public ActionResult DuyetBai(long id)
        {
            var dao = new PostDao();
            dao.DuyetBai(id);
            SetAlert("Duyệt bài đăng thành công", "thanhcong");
            return RedirectToAction("Index", "PostManager");
        }
        //Bỏ duyệt bài
        public ActionResult BoDuyetBai(long id)
        {
            var dao = new PostDao();
            dao.BoDuyetBai(id);
            SetAlert("Bỏ duyệt bài đăng thành công", "thanhcong");
            return RedirectToAction("PostWaiting", "PostManager");
        }



        //Lấy ra danh sách tất cả bài đăng của tôi
        public ActionResult MyPost(string searchString, int page = 1, int pageSize = 5)
        {
            var postdao = new PostDao();
            var userSession = new UserInfo();
            int idtk = int.Parse(Session["USER_ID"].ToString());
            var model = postdao.MyPost(searchString, page, pageSize, idtk);
            if (model.Count() == 0)
            {
                //Response.Write("<script>alert('Tài khoản của bạn hiện chưa có bài đăng nào!');window.location.href='Index'; </script>");
                SetAlert("Tài khoản này hiện chưa có bài đăng nào", "canhbao");
            }
            else
                ViewBag.searchstring = searchString;
            return View(model);
        }


        //lấy ra danh sách thể loại cho bài đăng
        public void GetTypeForPost(long? selectedid = null)
        {
            var dao = new TypeDao();
            ViewBag.IDTheLoai = new SelectList(dao.ListTypeForCreatePost(), "IDTheLoai", "TenTheLoai", selectedid);

        }

        //Xóa bài đăng

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new PostDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}