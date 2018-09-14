using SRIS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRIS.SPI
{
    public interface IUserInfo
    {
        /// <summary>
        /// 新用户注册
        /// </summary>
        /// <param name="model">用户注册信息实体类</param>
        /// <returns></returns>
        bool Register(UserInfo model);

        /// <summary>
        /// 通过用户名查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        UserInfo GetUserInfoByUserName(string userName);
    }
}
