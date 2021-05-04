using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocTinTuc.Common;

namespace WebDocTinTuc.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            var sesA = (UserInfo)Session[CommonConstant.ADMIN_SESSION];
            var sesU = (UserInfo)Session[CommonConstant.USER_SESSION];
            if (sesA != null)
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
            else
            {
                if (sesU != null)
                {
                    return RedirectToAction("Index", "HomeUser");
                }
            }
            return View();
        }
    }
}