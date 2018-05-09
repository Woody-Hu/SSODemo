using SSODemo.Entity;
using SSODemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity.Attributes;

namespace SSODemo.APIControllers
{
    public class LoginController: ApiController
    {
        [Dependency]
        public IUserService UseUserService { set; get; }

        [Dependency]
        public ISessionService UseSessionService { set; get; }


        public bool Get(string name,string pass)
        {
            if (null != HttpContext.Current.Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey])
            {
                return true;
            }
            else if (UseUserService.Check(name,pass))
            {
                var tempSessionId = Guid.NewGuid().ToString();

                AuthSession tempKey = new AuthSession() { SessionKey = tempSessionId, UserName = name };

                UseSessionService.Create(tempKey);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie(SSOUtility.SSOTool.StrUseSessionKey, tempSessionId));

            }
            else
            {
                return false;
            }


            return true;
        }
    }
}