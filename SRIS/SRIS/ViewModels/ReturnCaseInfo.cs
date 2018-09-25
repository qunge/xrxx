using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRIS.ViewModels
{
    public class ReturnCaseInfo
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<ReturnCaseViewInfo> data { get; set; }
    }

    public class ReturnCaseViewInfo
    {
        /// <summary>
        /// 案例登记信息ID
        /// </summary>
        [Display(Name = "案例登记信息ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  // Id字段的值由用户生成而不是由数据库生成
        public string RegisterInfoId { get; set; }

        /// <summary>
        /// 案例编号
        /// </summary>
        public string CaseCode { get; set; }

        /// <summary>
        /// 寻亲类别ID
        /// </summary>
        [Required]
        [Display(Name = "寻亲类别")]
        public string SRTypeId { get; set; }

        /// <summary>
        /// 寻亲类别名称
        /// </summary>
        public string SRTypeName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 被寻人姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "被寻人姓名")]
        public string BeSeekerName { get; set; }

        /// <summary>
        /// 领任务时间
        /// </summary>
        public string GetTaskDateTime { get; set; }

        /// <summary>
        /// 退任务时间
        /// </summary>
        [Display(Name = "退任务时间")]
        public string ReturnTaskDateTime { get; set; }

        /// <summary>
        /// 退任务原因
        /// </summary>
        [StringLength(50)]
        [Display(Name = "退任务原因")]
        public string ReturnReason { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}