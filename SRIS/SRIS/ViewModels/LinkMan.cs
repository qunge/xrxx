using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRIS.ViewModels
{
    public class LinkManModel
    {
        /// <summary>
        /// 联系人ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  // Id字段的值由用户生成而不是由数据库生成
        [Display(Name = "联系人ID")]
        public string LinkManID { get; set; }

        /// <summary>
        /// 登记案例信息
        /// </summary>
        public string RegisterInfoId { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [Required(ErrorMessage ="联系人姓名必填")]
        [StringLength(50)]
        [Display(Name = "联系人姓名")]
        public string LinkManName { get; set; }

        /// <summary>
        /// 性别(0:空，1：男，2：女)
        /// </summary>
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 与被寻人关系
        /// </summary>
        [StringLength(50)]
        [Display(Name = "与被寻人关系")]
        public string Relationship { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [StringLength(18)]
        [Display(Name = "身份证号码")]
        public string IdCardNo { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [StringLength(50)]
        [Display(Name = "职业")]
        public string Career { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        [Display(Name = "联系人地址")]
        public string Address { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [StringLength(11)]
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "电话号码")]
        public string TelPhone { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        [StringLength(50)]
        [Display(Name = "QQ")]
        public string QQ { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        [StringLength(50)]
        [Display(Name = "微信")]
        public string WeiXin { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 其他联系方式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "其他联系方式")]
        public string OtherLink { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
        

    }
}