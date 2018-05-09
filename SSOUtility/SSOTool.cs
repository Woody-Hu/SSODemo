using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SSOUtility
{
    /// <summary>
    /// sso工具
    /// </summary>
    public class SSOTool
    {
        /// <summary>
        /// 会话Key参数名
        /// </summary>
        public const string StrUseSessionKey = "sessionKey";

        /// <summary>
        /// AppId参数名
        /// </summary>
        public const string StrUseAppId = "appId";

        /// <summary>
        /// 使用的Url参数名
        /// </summary>
        public const string StrUseUseUrl = "useUrl";

        /// <summary>
        /// 使用的读写锁
        /// </summary>
        private static ReaderWriterLockSlim m_useRWLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 使用的Appid列表
        /// </summary>
        private static HashSet<string> m_useAppIdSet = new HashSet<string>();

        /// <summary>
        /// 检查会话
        /// </summary>
        /// <param name="inputSeesionId"></param>
        /// <returns></returns>
        internal static bool CheckSession(string inputSeesionId)
        {
            string useUrl = SSOConfig.UseSessionCheckApi + "?" + string.Format("{0} = {1}", StrUseSessionKey, inputSeesionId);

            return UseWebAPI<bool>(useUrl);
        }

        /// <summary>
        /// 获取会话对应封装的Json格式
        /// </summary>
        /// <param name="inputSeesionId"></param>
        /// <returns></returns>
        public static string GetSessionJson(string inputSeesionId)
        {
            return string.Empty;
        }

        /// <summary>
        /// 转发到SSO 利用AppId
        /// </summary>
        /// <param name="inputContext"></param>
        /// <param name="inputAppUrlId"></param>
        /// <param name="inputUrl"></param>
        public static void SSOLoginByAppId(ActionExecutingContext inputContext, string inputAppUrlId)
        {
            //设置转发Url
            string useRedUrl = SSOConfig.UseSSOLoginUrl + "?" + string.Format("{0}={1}", StrUseAppId, inputAppUrlId);

            inputContext.Result = new RedirectResult(useRedUrl);

        }


        /// <summary>
        /// 转发到SSO登陆页面 利用Url
        /// </summary>
        /// <param name="inputContext"></param>
        /// <param name="inputUrl"></param>
        public static void SSOLoginByUrl(ActionExecutingContext inputContext, string inputUrl)
        {
            //转义Url
            string useUrlStr = ChangeUrlStr(inputUrl);

            //设置转发Url
            string useRedUrl = SSOConfig.UseSSOLoginUrl + "?" + string.Format("{0}={1}", StrUseUseUrl, useUrlStr);

            inputContext.Result = new RedirectResult(useRedUrl);
        }

        /// <summary>
        /// 输入的Url字符串转义
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ChangeUrlStr(string inputStr)
        {
            return inputStr;
        }

        /// <summary>
        /// 回复使用的Url字符串
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string ReChangeUrlStr(string inputStr)
        {
            return inputStr;
        }

        /// <summary>
        /// 向SSO中心申请添加AppId
        /// </summary>
        /// <param name="inputAppId"></param>
        /// <param name="inputActionContext"></param>
        public static void PrepareAppApi(string inputAppId, string inputUrl)
        {
            //若没有申请过Id
            if (!IfIdIsUse(inputAppId))
            {
                AddAppid(inputAppId, inputUrl);

                try
                {
                    //进入写锁
                    m_useRWLock.EnterWriteLock();
                    //双重检查
                    if (!m_useAppIdSet.Contains(inputAppId))
                    {
                        m_useAppIdSet.Add(inputAppId);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    m_useRWLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// 使用WebAPI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputApi"></param>
        /// <returns></returns>
        public static T UseWebAPI<T>(string inputApi)
        {
            HttpClient useHttpClient = new HttpClient();

            try
            {
                //发起添加Appid请求
                var tempReturn = useHttpClient.GetAsync(inputApi).Result;

                //检验状态码
                tempReturn.EnsureSuccessStatusCode();

                T tempValue = tempReturn.Content.ReadAsAsync<T>().Result;

                return tempValue;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 输入Id是否在当前已使用列表中
        /// </summary>
        /// <param name="inputAppId"></param>
        /// <returns></returns>
        private static bool IfIdIsUse(string inputAppId)
        {

            bool returnValue = false;
            try
            {
                m_useRWLock.EnterReadLock();
                returnValue = m_useAppIdSet.Contains(inputAppId);
            }
            catch (Exception)
            {
                ;
            }
            finally
            {
                m_useRWLock.ExitReadLock();
            }

            return returnValue;


        }

        /// <summary>
        /// 添加AppId
        /// </summary>
        /// <param name="inputAppId"></param>
        /// <param name="inputActionContext"></param>
        private static void AddAppid(string inputAppId, string inputUrl)
        {
            string useUrl = inputUrl;

            HttpClient useHttpClient = new HttpClient();

            //制作请求字符串
            string useRequestString = SSOConfig.UseAddAppApi + "?" +
                string.Format("{0}={1}&{2}={3}", StrUseAppId, inputAppId, StrUseUseUrl, useUrl);

            //使用的WebApi
            UseWebAPI<bool>(useRequestString);
        }

    }
}
