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
    public class ReturnCaseController : Controller
    {
        IRegisterInfo IRegisterInfo;
        IReturnCase IReturnCase;
        public ReturnCaseController()
        {
            IRegisterInfo = new DAL.RegisterInfoDal();
            IReturnCase = new DAL.ReturnCaseDal();
        }
        // GET: ReturnCase
        public ActionResult Index()
        {
            return View();
        }

        public string GetReturnCase(int page, int limit, string whereStr)
        {
            int pageCount;
            Dictionary<string, string> whereDic = null;
            if (!string.IsNullOrEmpty(whereStr))
            {
                whereDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(whereStr);
            }
            string userId = Session["userId"].ToString();
            List<RegisterModel> registerModelList = IReturnCase.GetAllCaseInfo(userId, page, limit, whereDic, out pageCount);
            List<ReturnCaseViewInfo> list = new List<ReturnCaseViewInfo>();
            foreach (RegisterModel item in registerModelList)
            {
                ReturnCaseViewInfo model = new ReturnCaseViewInfo() {
                    BeSeekerName = item.RegisterInfo.BeSeekerName,
                    GetTaskDateTime = item.RegisterInfo.GetTaskDateTime.ToString("D"),
                    RegisterInfoId = item.RegisterInfo.RegisterInfoID,
                    Remarks = item.RegisterInfo.Remarks,
                    ReturnReason = item.RegisterInfo.ReturnReason,
                    ReturnTaskDateTime = Convert.ToDateTime(item.RegisterInfo.ReturnTaskDateTime).ToString("D"),
                    SRTypeName=item.SRTypeName,
                    Title=item.RegisterInfo.Title,
                    SRTypeId=item.RegisterInfo.SRTypeID,
                    CaseCode=item.RegisterInfo.CaseCode
                };
                list.Add(model);
            }
            ReturnCaseInfo returnCaseInfo = new ReturnCaseInfo() {
                code=0,
                count=pageCount,
                data=list,
                msg=""
            };
            return JsonConvert.SerializeObject(returnCaseInfo);
        }

        public ActionResult AddReturnTask()
        {
            return View();
        }

        public string AddReturnCase(string id,string returnReason)
        {
            var obj = new { status = false, message = "" };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    if (!string.IsNullOrEmpty(returnReason))
                    {
                        RegisterInfo info = IRegisterInfo.GetRegisterInfoById(id);
                        info.ReturnReason = returnReason;
                        info.IsReturnTask = 1;
                        info.ReturnTaskDateTime = DateTime.Now;
                        if (IRegisterInfo.UpdateRegisterInfo(info))
                        {
                            obj = new { status = true, message = "退任务成功" };
                        }
                        else
                        {
                            obj = new { status = false, message = "退任务失败" };
                        }
                    }
                    else
                    {
                        obj = new { status = false, message = "退任务原因不能为空" };
                    }
                }
                else
                {
                    obj = new { status = false, message = "案例登记ID不能为空" };
                }
            }
            catch (Exception ex)
            {
                obj = new { status = false, message = ex.Message };
            }
            return JsonConvert.SerializeObject(obj);
        }
    }
}