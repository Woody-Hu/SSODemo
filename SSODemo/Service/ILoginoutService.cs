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
    public interface ILogInOutService
    {
        bool LogIn(string inputName, string inputPass);

        bool LogOut();

    }
}
