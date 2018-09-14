using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRIS.ViewModels
{
    public class UserInfos
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [Display(Name = "用户名")]
        [RegularExpression(@"^[A-Za-z0-9\u4e00-\u9fa5]+$", ErrorMessage = "用户名必须是汉字、字母或数字")]
        [StringLength(25)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [Display(Name = "密码")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "密码必须以字母开头，长度在6~18之间，只能包含字符、数字和下划线")]
        [StringLength(18)]
        public string PassWord { get; set; }

    }
}