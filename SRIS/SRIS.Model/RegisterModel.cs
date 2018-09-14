using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Framework;

namespace SRIS.Model
{
    /// <summary>
    /// 案例信息实体类
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 案例信息
        /// </summary>
        public RegisterInfo RegisterInfo { get; set; }

        /// <summary>
        /// 案例类别名称
        /// </summary>
        public string SRTypeName { get; set; }
    }
}
