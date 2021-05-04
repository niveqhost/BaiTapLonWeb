﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebDocTinTuc.Common;

namespace WebDocTinTuc.Areas.Admin.Controllers
{
    public class BaseUserController : Controller
    {
        // GET: Admin/BaseUser
        //
        // KIỂM TRA QUYỀN NGƯỜI ĐĂNG/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ses = (UserInfo)Session[CommonConstant.USER_SESSION];
            if (ses == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string thongbao, string loai)
        {
            TempData["thongbao"] = thongbao;
            if (loai == "thanhcong")
            {
                TempData["loaithongbao"] = "alert-success";
            }
            else if (loai == "thatbai")
            { TempData["loaithongbao"] = "alert-warning"; }

            else
                if (loai == "canhbao")
            {
                TempData["loaithongbao"] = "alert-danger";
            }
        }
    }
}