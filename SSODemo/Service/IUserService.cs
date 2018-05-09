using SSODemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSODemo.Service
{
    /// <summary>
    /// 登陆接口
    /// </summary>
    public interface IUserService
    {
        bool Check(string inputName, string inputPass);

        User Get(string inputName);

    }
}
