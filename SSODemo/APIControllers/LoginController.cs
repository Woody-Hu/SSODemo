using SSODemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SSODemo.APIControllers
{
    public class LoginController: ApiController
    {
        public ILogInOutService UseLogService { set; get; }

        public bool Get(string name,string pass)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie( SSOUtility.SSOTool.StrUseSessionKey, Guid.NewGuid().ToString()));
            return true;
        }
    }
}