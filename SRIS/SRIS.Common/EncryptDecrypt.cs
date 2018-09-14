using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SRIS.Common
{
    /// <summary>
    /// 数据加密解密算法
    /// </summary>
   public class EncryptDecrypt
    {
        private static object lockObj = new object();

        /// <summary>
        /// 加密数据算法
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static string Encrypt(string rawData, string salt)
        {
            try
            {
                lock (lockObj)
                {
                    byte[] middle = Encoding.UTF8.GetBytes(rawData);
                    string result = Convert.ToBase64String(middle);
                    //var subSalt = "abcdefghijklmnopqrstuvwxyz";
                    //if (salt.Length < 16) salt += subSalt.Substring(0, (16 - salt.Length));
                    StringBuilder eventNumber = new StringBuilder();
                    StringBuilder oddNumber = new StringBuilder();

                    for (int i = 0; i < result.Length / 2; i++)
                    {
                        eventNumber.Append(result[2 * i]);
                        oddNumber.Append(result[2 * i + 1]);
                    }
                    return (eventNumber.ToString() + oddNumber.ToString() + salt);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取盐值
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            try
            {
                lock (lockObj)
                {
                    //string result = "";
                    Random random = new Random();
                    int length = random.Next(6, 10);
                    string guid = Guid.NewGuid().ToString();
                    return guid.Substring(0, length);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 解密数据算法
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static string Decrypt(string rawData, string salt)
        {
            try
            {
                lock (lockObj)
                {
                    int saltLeng = salt.Length;
                    rawData = rawData.Replace(" ", "+");
                    //var subSalt="YYYYYYYYYYYYYYYY";
                    //if (salt.Length < 16) salt += subSalt.Substring(0, (16 - salt.Length));
                    rawData = rawData.Substring(0, rawData.Length - saltLeng);
                    string eventString = rawData.Substring(0, rawData.Length / 2);
                    string oddString = rawData.Substring(rawData.Length / 2);
                    StringBuilder resultString = new StringBuilder();
                    for (int i = 0; i < rawData.Length / 2; i++)
                    {
                        resultString.Append(eventString[i].ToString() + oddString[i].ToString());
                    }
                    string result = resultString.ToString();
                    byte[] middle = Convert.FromBase64String(result);
                    return Encoding.UTF8.GetString(middle);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建盾牌
        /// </summary>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string CreateShieldToken(string salt, out string guid)
        {
            try
            {
                lock (lockObj)
                {
                    guid = Guid.NewGuid().ToString();
                    string startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string flag = "0";
                    string result = guid + "|" + startTime + "|" + flag;
                    result = Encrypt(result, salt);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int DeserializeShieldToken(string token, string salt, out string newToken)
        {
            try
            {
                lock (lockObj)
                {
                    newToken = string.Empty;
                    int duration = 600;
                    if (String.IsNullOrEmpty(token))
                        return 2; //throw new ArgumentException("盾牌不能为空");
                    if (String.IsNullOrEmpty(salt))
                        return 3; //throw new ArgumentException("解析盐值不能 ");
                    string middle = Decrypt(token, salt);


                    string[] tokenArray = middle.Split('|');
                    if (tokenArray.Length != 3)
                        return 4; //throw new Exception("盾牌格式不合法,请重新申请");
                    DateTime endTime = DateTime.Now;
                    DateTime startTime = Convert.ToDateTime(tokenArray[1]);
                    TimeSpan span = endTime.Subtract(startTime);


                    if (span.TotalSeconds > duration || span.TotalSeconds < 0)
                    {
                        return 5; //throw new Exception("盾牌时间已经失效,请重新申请");
                    }
                    if (tokenArray[2] == "1")
                        return 6; //throw new Exception("盾牌已经使用过,请重新申请");
                    newToken = tokenArray[0] + "|" + tokenArray[1] + "|" + "1";

                    newToken = Encrypt(newToken, salt);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// MD5 加密字符串  
        /// </summary>  
        /// <param name="rawPass">源字符串</param>  
        /// <returns>加密后字符串</returns>  
        public static string MD5Encoding(string rawPass)
        {
            // 创建MD5类的默认实例：MD5CryptoServiceProvider  
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化  
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>  
        /// MD5盐值加密  
        /// </summary>  
        /// <param name="rawPass">源字符串</param>  
        /// <param name="salt">盐值</param>  
        /// <returns>加密后字符串</returns>  
        public static string MD5Encoding(string rawPass, object salt)
        {
            if (salt == null) return rawPass;
            return MD5Encoding(rawPass + "{" + salt.ToString() + "}");
        }
    }
}
