using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDocTinTuc.Areas.Admin.Controllers
{
    public class UserHomeController : BaseUserController
    {
        // GET: Admin/UserHome
        public ActionResult Index()
        {
            return View();
        }
    }
}