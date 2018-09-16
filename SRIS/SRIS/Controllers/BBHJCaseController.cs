using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRIS.SPI;
using SRIS.ViewModels;
using SRIS.Framework;

namespace SRIS.Controllers
{
    public class BBHJCaseController : Controller
    {
        IBBHJInfo IBBHJInfo;
        public BBHJCaseController()
        {
            IBBHJInfo = new DAL.BBHJCaseDal();
        }
        //
        // GET: /BBHJCase/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取引导到宝贝回家的案例信息
        /// </summary>
        /// <param name="page">当前页号</param>
        /// <param name="limit">分页大小</param>
        /// <param name="whereStr">查询条件</param>
        /// <returns></returns>
        public string GetBBHJCaseList(int page, int limit, string whereStr)
        {
            int pageCount=0;
            Dictionary<string, string> whereDic = null;
            if (!string.IsNullOrEmpty(whereStr))
            {
                whereDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(whereStr);
            }
            string userId = Session["userId"].ToString();
            // 获取数据列表
            List<Model.BBHJCaseModel> bbhjCaseModelList = IBBHJInfo.GetAllBBHJCase(userId, page, limit, whereDic, out  pageCount);
            List<BBHJCaseViewInfo> bbhjCaseInfoList = new List<BBHJCaseViewInfo>();
            foreach (Model.BBHJCaseModel item in bbhjCaseModelList)
            {
                BBHJCaseViewInfo model = new BBHJCaseViewInfo() { 
                    BBHJCode=item.BBHJCode,
                    BBHJInfoID=item.BBHJInfoID,
                    BeSeekerName=item.BeSeekerName,
                    CaseCode=item.CaseCode,
                    GetTaskDate=item.GetTaskDate.ToString("D"),
                    GuideTime=item.GuideTime.ToString("D"),
                    RegisterInfoId=item.RegisterInfoId,
                    RegisterLink=item.RegisterLink,
                    Remarks=item.Remarks,
                    SRTypeID=item.SRTypeID,
                    SRTypeName=item.SRTypeName,
                    Title=item.Title,
                    Volunteer=item.Volunteer
                    
                };
                bbhjCaseInfoList.Add(model);
               
            }
             BBHJCaseInfo bbhjCaseInfo = new BBHJCaseInfo() { 
                    code=0,
                    count=pageCount,
                    data = bbhjCaseInfoList,
                    msg=""
                };
             return JsonConvert.SerializeObject(bbhjCaseInfo);
        }

        public ActionResult AddBBHJ(string id)
        {
            BBHJCaseViewInfo model = new BBHJCaseViewInfo() { 
                RegisterInfoId=id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBBHJ(BBHJCaseViewInfo model)
        {
            if (ModelState.IsValid)
            {
                BBHJInfo bbhjInfo = new BBHJInfo() { 
                    BBHJCode=model.BBHJCode,
                    BBHJInfoID=System.Guid.NewGuid().ToString(),
                    CreateDateTime=DateTime.Now,
                    GuideTime=DateTime.Now,
                    RegisterInfoID=model.RegisterInfoId,
                    Remark=model.Remarks,
                    Volunteer=model.Volunteer
                };
                if (IBBHJInfo.CreateBBHJCase(bbhjInfo))
                    return View("Index");
            }
            return View(model);
        }
	}
}