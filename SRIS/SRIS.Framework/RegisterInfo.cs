//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRIS.Framework
{
    using System;
    using System.Collections.Generic;
    
    public partial class RegisterInfo
    {
        public string RegisterInfoID { get; set; }
        public string CaseCode { get; set; }
        public string Title { get; set; }
        public string BeSeekerName { get; set; }
        public System.DateTime GetTaskDateTime { get; set; }
        public string RegisterLink { get; set; }
        public string PostLink { get; set; }
        public Nullable<int> DNAState { get; set; }
        public int IsReturnTask { get; set; }
        public Nullable<System.DateTime> ReturnTaskDateTime { get; set; }
        public string ReturnReason { get; set; }
        public string IsBBHJ { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public string SRTypeID { get; set; }
        public string UserInfoID { get; set; }
        public int IsDelete { get; set; }
        public int IsSuccess { get; set; }
    }
}