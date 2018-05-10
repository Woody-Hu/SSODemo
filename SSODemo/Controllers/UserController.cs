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
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Login(User inputUser)
        {
            if (null != Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey])
            {
                return new ContentResult() { Content =  Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey].Value };
            }
            else if (!UseUserService.Check(inputUser.Name,inputUser.PassWord))
            {
                return new ContentResult() { Content = string.Format("登录失败") };
            }
            else
            {
                var tempSessionId = Guid.NewGuid().ToString();

                AuthSession tempKey = new AuthSession() { SessionKey = tempSessionId, UserName = inputUser.Name };

                UseSessionService.Create(tempKey);

                Response.Cookies.Add(new HttpCookie(SSOUtility.SSOTool.StrUseSessionKey, tempSessionId));

                return new ContentResult() { Content = tempSessionId };

            }

            
        }
    }
}