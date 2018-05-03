﻿using SSODemo.Service;
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
    public class PassportControler: ApiController
    {
        ISessionService m_useSeesionService;

        /// <summary>
        /// 检查会话
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        [HttpGet]
        public bool Check(string sessionKey)
        {
            return m_useSeesionService.CheckSession(sessionKey);
        }
    }
}