using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRIS.ViewModels
{
    public class BBHJCaseInfo
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<BBHJCaseViewInfo> data { get; set; }
    }
    public class BBHJCaseViewInfo
    {
        /// <summary>
        /// 宝贝回家案例ID
        /// </summary>
        public string BBHJInfoID { get; set; }
        /// <summary>
        /// 登记案例ID
        /// </summary>
        [Required]
        public string RegisterInfoId { get; set; }
        /// <summary>
        /// 等着我案例编号
        /// </summary>

        public string CaseCode { get; set; }

        /// <summary>
        /// 寻亲类别ID
        /// </summary>
        public string SRTypeID { get; set; }

        /// <summary>
        /// 寻亲类别名称
        /// </summary>
        public string SRTypeName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 被寻人姓名
        /// </summary>
        public string BeSeekerName { get; set; }

        /// <summary>
        /// 领任务时间
        /// </summary>
        public string GetTaskDate
        {
            get;
            set;
        }

        /// <summary>
        /// 登记信息链接
        /// </summary>
        public string RegisterLink { get; set; }


        /// <summary>
        /// 引导到宝贝回家时间
        /// </summary>
        public string GuideTime
        {
            get;
            set;
        }

        /// <summary>
        /// 宝贝回家编号
        /// </summary>
        [Required]
        [Display(Name = "宝贝回家案例编号：")]
        public string BBHJCode { get; set; }

        /// <summary>
        /// 跟进志愿者
        /// </summary>
        [Required]
        [Display(Name = "宝贝回家跟进志愿者：")]
        public string Volunteer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注：")]
        public string Remarks { get; set; }
    }
}