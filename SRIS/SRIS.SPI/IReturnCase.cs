using SRIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRIS.SPI
{
    /// <summary>
    /// 已退任务操作类接口
    /// </summary>
    public interface IReturnCase
    {
        /// <summary>
        /// 获取已退任务
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="page">当前页号</param>
        /// <param name="limit">分页大小</param>
        /// <param name="whereDic">查询条件</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        List<RegisterModel> GetAllCaseInfo(string userId, int page, int limit, Dictionary<string, string> whereDic, out int pageCount);
    }
}
