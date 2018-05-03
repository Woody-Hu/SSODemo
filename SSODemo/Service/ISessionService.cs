using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSODemo.Service
{
    /// <summary>
    /// 会话服务接口
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// 检查会话
        /// </summary>
        /// <param name="inputSessionId"></param>
        /// <returns></returns>
        bool CheckSession(string inputSessionId);
    }
}
