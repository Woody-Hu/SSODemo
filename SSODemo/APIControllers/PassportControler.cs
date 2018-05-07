using SSODemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SSODemo.APIControllers
{
    /// <summary>
    /// 会话检查接口
    /// </summary>
    public class PassportController: ApiController
    {
        ISessionService m_useSeesionService;

        /// <summary>
        /// 检查会话
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public bool Get(string sessionKey)
        {
            return m_useSeesionService.CheckSession(sessionKey);
        }

        public string Get()
        {
            string useStr = "AAAAA";
            if (null != HttpContext.Current.Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey])
            {
                useStr = HttpContext.Current.Request.Cookies[SSOUtility.SSOTool.StrUseSessionKey].Value;
            }
            return useStr;
        }
    }
}