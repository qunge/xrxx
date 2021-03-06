﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRIS.Model
{
    /// <summary>
    /// 宝贝回家案例实体类
    /// </summary>
    public class BBHJCaseModel
    {
        /// <summary>
        /// 宝贝回家案例ID
        /// </summary>
        public string BBHJInfoID { get; set; }
        /// <summary>
        /// 登记案例ID
        /// </summary>
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
        public DateTime GetTaskDate
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
        public DateTime GuideTime
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
        [Display(Name="跟进志愿者：")]
        public string Volunteer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注：")]
        public string Remarks { get; set; }
    }
}
