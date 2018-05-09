using SSODemo.Entity;
using SSODemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace SSODemo.Controllers
{
    public class UserController : Controller
    {
        [Dependency]
        public IUserService UseUserService { set; get; }

        [Dependency]
        public ISessionService UseSessionService { set; get; }

        [HttpGet]
        public ActionResult Login()
        {
            if (null != Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey])
            {
                return new ContentResult() { Content = Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey] .Value};
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User inputUser)
        {
            if (null != Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey])
            {
                return new ContentResult() { Content = "已登录" };
            }

            return null;
        }
    }
}