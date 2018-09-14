using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SRIS.ViewModels
{
    public class FollowDetailsInfo
    {
        public string Id { get; set; }
        public string CaseId { get; set; }

        [Display(Name = "跟进信息")]
        public string MesText { get; set; }
        public string ImageUrl { get; set; }
        public System.DateTime CreateDataTime { get; set; }
    }
}