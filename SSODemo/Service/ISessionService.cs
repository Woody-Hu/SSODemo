using SSODemo.Entity;
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
        /// 储值会话
        /// </summary>
        /// <param name="inputSession"></param>
        void Create(AuthSession inputSession);

        /// <summary>
        /// 检查会话
        /// </summary>
        /// <param name="inputSessionId"></param>
        /// <returns></returns>
        bool CheckSession(string inputSessionId);

        /// <summary>
        /// 删除会话
        /// </summary>
        /// <param name="inputSessionId"></param>
        void Delete(string inputSessionId);

        /// <summary>
        /// 获取会话封装
        /// </summary>
        /// <param name="inputSessionId"></param>
        /// <returns></returns>
        AuthSession Get(string inputSessionId);
    }
}
