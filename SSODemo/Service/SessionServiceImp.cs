using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSODemo.Entity;
using UnityUtility;
using System.Threading;

namespace SSODemo.Service
{
    /// <summary>
    /// 会话管理实现
    /// </summary>
    [Compent(Singleton = true, RegistByClass = false)]
    public class SessionServiceImp : ISessionService
    {
        private Dictionary<string, AuthSession> m_useDic;

        private ReaderWriterLockSlim m_useLock;

        /// <summary>
        /// 会话检查
        /// </summary>
        /// <param name="inputSessionId"></param>
        /// <returns></returns>
        public bool CheckSession(string inputSessionId)
        {
            try
            {
                m_useLock.EnterReadLock();
                return m_useDic.ContainsKey(inputSessionId);
            }
            finally
            {
                m_useLock.ExitReadLock();
            }
           
        }

        public void Create(AuthSession inputSession)
        {
            try
            {
                m_useLock.EnterWriteLock();
                if (!string.IsNullOrWhiteSpace(inputSession.SessionKey) && !m_useDic.ContainsKey(inputSession.SessionKey))
                {
                    m_useDic.Add(inputSession.SessionKey, inputSession);
                }
            }
            finally
            {
                m_useLock.ExitWriteLock();
            }
        }

        public void Delete(string inputSessionId)
        {
            try
            {
                m_useLock.EnterWriteLock();
                if (!string.IsNullOrWhiteSpace(inputSessionId) && !m_useDic.ContainsKey(inputSessionId))
                {
                    m_useDic.Remove(inputSessionId);
                }
            }
            finally
            {
                m_useLock.ExitWriteLock();
            }
        }

        public AuthSession Get(string inputSessionId)
        {
            try
            {
                m_useLock.EnterReadLock();

                if (!string.IsNullOrWhiteSpace(inputSessionId) && m_useDic.ContainsKey(inputSessionId))
                {
                    return m_useDic[inputSessionId];
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                m_useLock.ExitReadLock();
            }
        }
    }
}