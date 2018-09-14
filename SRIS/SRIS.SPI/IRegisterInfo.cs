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
    /// 案例信息操作类
    /// </summary>
    public interface IRegisterInfo
    {
        /// <summary>
        /// 获取所有的登记案例的信息
        /// </summary>
        /// <returns></returns>
        List<RegisterModel> GetAllCaseInfo(string userId, int page, int limit, Dictionary<string, string> whereDic, out int pageCount);

        /// <summary>
        /// 获取所有的登记案例的信息
        /// </summary>
        /// <returns></returns>
        List<RegisterModel> GetAllCaseInfo(string userId);

        /// <summary>
        /// 获取所有的案例类型
        /// </summary>
        /// <returns></returns>
        List<SRType> GetSRType();

        /// <summary>
        /// 创建一条案例信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CreateRegister(RegisterInfo model);

        /// <summary>
        /// 通过案例类型ID获取案例类型
        /// </summary>
        /// <param name="id">例类型ID</param>
        /// <returns></returns>
        SRType GetSRTypeById(string id);

        /// <summary>
        /// 通过案例ID获取联系人信息
        /// </summary>
        /// <param name="CaseId">案例ID</param>
        /// <returns></returns>
        LinkMan GetLinManModelById(string CaseId);

        /// <summary>
        /// 创建联系人信息
        /// </summary>
        /// <param name="model">联系人信息实体类</param>
        /// <returns></returns>
        bool CreateLinkMan(LinkMan model);

        /// <summary>
        /// 修改联系人信息
        /// </summary>
        /// <param name="model">联系人信息实体类</param>
        /// <returns></returns>
        bool UpdateLinkMan(LinkMan model);

        /// <summary>
        /// 通过案例ID获取案例信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        RegisterInfo GetRegisterInfoById(string Id);

        /// <summary>
        /// 修改案例信息
        /// </summary>
        /// <param name="model">案例信息实体类</param>
        /// <returns></returns>
        bool UpdateRegisterInfo(RegisterInfo model);

        /// <summary>
        /// 删除案例登记信息
        /// </summary>
        /// <param name="id">案例登记ID</param>
        /// <returns></returns>
        bool DelRegisterInfo(string id);

        /// <summary>
        /// 设置为宝贝回家案例
        /// </summary>
        /// <param name="id">要设置的案例ID</param>
        /// <returns></returns>
        bool Bbhj(string id);

        /// <summary>
        /// 设置为成功案例
        /// </summary>
        /// <param name="id">案例ID</param>
        /// <returns></returns>
        bool SetSuccess(string id);
    }
}
