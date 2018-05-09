using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSODemo.Entity;
using UnityUtility;

namespace SSODemo.Service
{
    /// <summary>
    /// 用户管理实现
    /// </summary>
    [Compent(Singleton = true, RegistByClass = false)]
    public class UserServiceImp : IUserService
    {
        public bool Check(string inputName, string inputPass)
        {
            if (!string.IsNullOrWhiteSpace(inputName) && inputPass == "123456")
            {
                return true;
            }
            return false;
        }

        public User Get(string inputName)
        {
            if (!string.IsNullOrWhiteSpace(inputName))
            {
                return new User() { Name = inputName, PassWord = "123456" };
            }
            return null;
        }
    }
}