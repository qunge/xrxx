using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Framework;

namespace SRIS.SPI
{
    /// <summary>
    /// 案例跟进信息操作类
    /// </summary>
    public interface IFollowDetails
    {
        /// <summary>
        /// 通过案例ID获取该案例的跟进信息
        /// </summary>
        /// <param name="id">案例ID</param>
        /// <returns></returns>
        List<SRIS.Model.FollowDetailsModel> GetList(string id);

        /// <summary>
        /// 新建跟进信息
        /// </summary>
        /// <param name="imageUrl">上传图片</param>
        /// <param name="gjxx">跟进信息</param>
        /// <param name="id">案例ID</param>
        /// <returns></returns>
        bool CreateFollowDetail(string imageUrl, string gjxx, string id);

        /// <summary>
        /// 删除跟进信息
        /// </summary>
        /// <param name="id">跟进信息ID</param>
        /// <returns></returns>
        bool DelFollowDetail(string id);

        /// <summary>
        /// 通过跟进信息ID获取跟进信息
        /// </summary>
        /// <param name="id">跟进信息ID</param>
        /// <returns></returns>
        SRIS.Model.FollowDetailsModel GetModel(string id);

        /// <summary>
        /// 修改案例跟进信息
        /// </summary>
        /// <param name="model">案例跟进信息实体类</param>
        /// <returns></returns>
        bool UpdateFollowDetails(FollowDetails model);
    }
}
