using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Model;
using SRIS.Framework;

namespace SRIS.SPI
{
    public interface IBBHJInfo
    {
        /// <summary>
        /// 获取所有的宝贝回家案例
        /// </summary>
        /// <returns></returns>
        List<BBHJCaseModel> GetAllBBHJCase(string userid, int page, int limit, Dictionary<string, string> whereDic, out int pageCount);

        /// <summary>
        /// 创建宝贝回家案例
        /// </summary>
        /// <param name="model">宝贝回家案例实体类</param>
        /// <returns></returns>
        bool CreateBBHJCase(BBHJInfo model);
    }
}
