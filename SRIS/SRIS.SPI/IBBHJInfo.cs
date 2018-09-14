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
        List<BBHJCaseModel> GetAllBBHJCase();

        /// <summary>
        /// 根据案例编号ID修改宝贝案例信息
        /// </summary>
        /// <param name="model">宝贝案例信息实体</param>
        /// <returns></returns>
        bool UpdateBBHJInfoById(BBHJCaseModel model);

        /// <summary>
        /// 获取宝贝回家案例信息
        /// </summary>
        /// <param name="Id">宝贝回家案例ID</param>
        /// <returns></returns>
        BBHJInfo GetBBHJInfoByID(string Id);
    }
}
