using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRIS.ViewModels
{
    public class RegisterCaseInfo
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<RegisterViewModel> data { get; set; }
    }

    public class RegisterViewModel
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
        [Required]
        [StringLength(50)]
        [Display(Name = "案例编号")]
        public string CaseCode { get; set; }

        /// <summary>
        /// 寻亲类别
        /// </summary>
        [Required]
        [Display(Name ="寻亲类别")]
        public string SRTypeId { get; set; }

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "领任务时间")]
        public DateTime GetTaskDateTime { get; set; }

        /// <summary>
        /// 页面展示领任务时间
        /// </summary>
        public string getTaskDate { get; set; }
        /// <summary>
        /// 登记链接
        /// </summary>
        [Required]
        [Display(Name = "登记链接")]
        public string RegisterLink { get; set; }

        /// <summary>
        /// 发帖链接
        /// </summary>
        [Display(Name = "发帖链接")]
        public string PostLink { get; set; }

        /// <summary>
        /// DNA状态(0:未采血)
        /// </summary>
        [Display(Name = "DNA状态")]
        public int? DNAState { get; set; }

        /// <summary>
        /// 是否退任务(0:未退任务，1：已退任务)
        /// </summary>
        [Display(Name = "是否退任务")]
        public int IsReturnTask { get; set; }

        /// <summary>
        /// 退任务时间
        /// </summary>
        [Display(Name = "退任务时间")]
        public DateTime? ReturnTaskDateTime { get; set; }

        /// <summary>
        /// 退任务原因
        /// </summary>
        [StringLength(50)]
        [Display(Name = "退任务原因")]
        public string ReturnReason { get; set; }

        /// <summary>
        /// 是否引导去宝贝回家登记
        /// </summary>]
        [Display(Name = "是否引导去宝贝回家登记")]
        public string IsBBHJ { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remarks { get; set; }

    }
}