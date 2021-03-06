using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebDocTinTuc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //Map cho trang đọc báo
            routes.MapRoute(
                name: "DocBao",
                url: "doc-bao/{UrlRequire}-{id}",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }
            );



            //Map cho trang đọc báo theo người đăng
            routes.MapRoute(
                name: "nguoidang",
                url: "Nguoi-dang/{TenTaiKhoan}-{id}",
                defaults: new { controller = "News", action = "DocBaoTheoNguoiDang", id = UrlParameter.Optional }
            );

            //Map cho trang thể loại bài báo
            routes.MapRoute(
                name: "TheLoai",
                url: "{UrlRequire}",
                defaults: new { controller = "TheLoai", action = "Index", id = UrlParameter.Optional }
            );

            //Map cho đăng kí
            routes.MapRoute(
                name: "DangKi",
                url: "DangKi",
                defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "WebDocTinTuc.Controllers" }
            );
        }
    }
}
