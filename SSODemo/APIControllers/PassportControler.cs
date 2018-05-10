using SSODemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity.Attributes;

namespace SSODemo.APIControllers
{
    /// <summary>
    /// 会话检查接口
    /// </summary>
    public class PassportController: ApiController
    {
        [Dependency]
        public ISessionService UseSeesionService { set; get; }

        /// <summary>
        /// 检查会话
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public bool Get(string sessionKey)
        {
            return UseSeesionService.CheckSession(sessionKey);
        }
    }
}