using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SSOUtility
{
    /// <summary>
    /// 拦截器特性
    /// </summary>
    public class SSOAuthorizeAttribute:ActionFilterAttribute
    {

        /// <summary>
        /// 拦截方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string tempUseSessionKey = string.Empty;

            tempUseSessionKey = filterContext.HttpContext.Request.QueryString[SSOTool.StrUseSessionKey]; 

            //若存在查询字符串增加Cookie跳转回元位置
            if (!string.IsNullOrWhiteSpace(tempUseSessionKey))
            {
                filterContext.HttpContext.Response.Cookies.Add(new System.Web.HttpCookie(SSOTool.StrUseSessionKey, tempUseSessionKey));
                filterContext.Result = ReLocate(filterContext);
            }
            else
            {
                bool needSSO = true;

                //若存在Cookie
                if ( null != filterContext.HttpContext.Response.Cookies[SSOTool.StrUseSessionKey] )
                {
                    //Cookie有效检查
                    if (!SSOTool.CheckSession(filterContext.HttpContext.Response.Cookies[SSOTool.StrUseSessionKey].Value))
                    {
                        filterContext.HttpContext.Response.Cookies.Remove(SSOTool.StrUseSessionKey);
                    }
                    //若需要SSO登陆
                    else
                    {
                        needSSO = false;
                    }
                }

                //若需要SSO登陆
                if (needSSO)
                {

                }


            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 结果重定向
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private ActionResult ReLocate(ActionExecutingContext filterContext)
        {
            string useUrl = filterContext.HttpContext.Request.Url.ToString();
            StringBuilder useStringBuilder = new StringBuilder();
            useStringBuilder.Append(useUrl);
          

            List<KeyValuePair<string, string>> lstUseQueryString = new List<KeyValuePair<string, string>>();

            //重新制作查询字符窗
            foreach (var oneKey in filterContext.HttpContext.Request.QueryString.AllKeys)
            {
                if (oneKey.Equals(SSOTool.StrUseSessionKey) || string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString[oneKey]))
                {
                    continue;
                }
                lstUseQueryString.Add(new KeyValuePair<string, string>(oneKey, filterContext.HttpContext.Request.QueryString[oneKey]));
            }

            //制作查询字符串
            if (lstUseQueryString.Count > 0)
            {
                useStringBuilder.Append("?");
            }


            foreach (var oneKVP in lstUseQueryString)
            {
                useStringBuilder.Append(string.Format("{0}={1}", oneKVP.Key, oneKVP.Value));
                useStringBuilder.Append("&");
            }

            string useStr = useStringBuilder.ToString();

            //清楚末端连接符
            if (lstUseQueryString.Count > 0)
            {
                useStr = useStr.TrimEnd('&');
            }

            return new RedirectResult(useStr);
        }
    }
}
