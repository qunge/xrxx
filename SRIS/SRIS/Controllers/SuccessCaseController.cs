using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SRIS.SPI;
using SRIS.Framework;
using SRIS.ViewModels;
using SRIS.Model;

namespace SRIS.Controllers
{
    public class SuccessCaseController : Controller
    {
        ISuccessCase ISuccessCase;
        public SuccessCaseController()
        {
            ISuccessCase = new SRIS.DAL.SuccessCaseDal();
        }
        // GET: SuccessCase
        public ActionResult Index()
        {
           
            return View();
        }

        public string GetSuccessCaseList(int page, int limit, string whereStr)
        {
            string userId = Session["userId"].ToString();
            int pageCount = 0;
            Dictionary<string, string> whereDic =null;
            if (!string.IsNullOrEmpty(whereStr))
            {
                whereDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(whereStr);
            }
            List<RegisterModel> registerInfoList = ISuccessCase.GetList(userId, page, limit, whereDic, out pageCount);
            List<SRIS.ViewModels.RegisterViewModel> list = new List<ViewModels.RegisterViewModel>();
            foreach (RegisterModel item in registerInfoList)
            {
                SRIS.ViewModels.RegisterViewModel model = new ViewModels.RegisterViewModel();
                model.BeSeekerName = item.RegisterInfo.BeSeekerName;
                model.CaseCode = item.RegisterInfo.CaseCode;
                model.getTaskDate = item.RegisterInfo.GetTaskDateTime.ToString("D");
                model.GetTaskDateTime = item.RegisterInfo.GetTaskDateTime;
                model.PostLink = item.RegisterInfo.PostLink;
                model.RegisterInfoId = item.RegisterInfo.RegisterInfoID;
                model.RegisterLink = item.RegisterInfo.RegisterLink;
                model.Remarks = item.RegisterInfo.Remarks;
                model.SRTypeName = item.SRTypeName;
                model.Title = item.RegisterInfo.Title;
                list.Add(model);
            }
            RegisterCaseInfo info = new RegisterCaseInfo() {
                code = 0,
                count = pageCount,
                data = list,
                msg = ""
            };
            return JsonConvert.SerializeObject(info);
        }
    }
}