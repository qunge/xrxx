using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRIS.Model
{
    /// <summary>
    /// 跟进详情实体类
    /// </summary>
    public class FollowDetailsModel
    {
        /// <summary>
        /// 跟进ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 案例ID
        /// </summary>
        public string CaseId { get; set; }

        /// <summary>
        /// 跟进信息
        /// </summary>
        public string MesText { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDataTime { get; set; }

    }
}
