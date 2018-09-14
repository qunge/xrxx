using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Framework;
using SRIS.SPI;

namespace SRIS.DAL
{
    /// <summary>
    /// 用户信息操作类
    /// </summary>
    public class UserInfoDal:IUserInfo
    {
        /// <summary>
        /// 新用户注册
        /// </summary>
        /// <param name="model">用户注册信息实体类</param>
        /// <returns></returns>
        public bool Register(UserInfo model)
        {
            using (var db = new SRISDBEntities())
            {
                db.UserInfo.Add(model);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 通过用户名查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public UserInfo GetUserInfoByUserName(string userName)
        {
            using (var db = new SRISDBEntities())
            {
                var userInfo = db.UserInfo.Where(n => n.UserName == userName).SingleOrDefault();
                return userInfo;
            }
        }
    }
}
