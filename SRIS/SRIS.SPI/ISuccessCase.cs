using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Framework;
using SRIS.Model;

namespace SRIS.SPI
{
    /// <summary>
    /// 成功案例操作类
    /// </summary>
    public interface ISuccessCase
    {
        /// <summary>
        /// 获取成功案例信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="page">当前页号</param>
        /// <param name="limit">分页大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="whereDic">查询条件</param>
        /// <returns></returns>
        List<RegisterModel> GetList(string userId, int page, int limit, Dictionary<string, string> whereDic, out int pageCount);
    }
}
