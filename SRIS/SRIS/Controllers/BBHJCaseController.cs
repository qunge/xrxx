using System.Web.Mvc;
using SRIS.SPI;
using System.Collections.Generic;
using SRIS.Model;
using Newtonsoft.Json;
using SRIS.Framework;
using SRIS.Common;
using System;

namespace SRIS.Controllers
{
[Authorize]
    public class BBHJCaseController : Controller
    {
        IRegisterInfo IRegisterInfo;
        IBBHJInfo IBBHJInfo;
        public BBHJCaseController()
        {
            IBBHJInfo = new DAL.BBHJCaseDal();
            IRegisterInfo = new DAL.RegisterInfoDal();
        }
        // GET: BBHJCase
        public ActionResult Index()
        {
            return View();
        }

        // 获取所有宝贝回家案例
        public string GetBBHJCase()
        {
            List<BBHJCaseModel> list = IBBHJInfo.GetAllBBHJCase();
            BBHJCaseViewModel model = new BBHJCaseViewModel()
            {
                code = 0,
                count = list.Count,
                data = list,
                msg = ""
            };
            return JsonConvert.SerializeObject(model);
        }


        public ActionResult EditBBHJInfo(string id)
        {
            BBHJInfo bbhjInfo = IBBHJInfo.GetBBHJInfoByID(id);
            if (!string.IsNullOrEmpty(bbhjInfo.BBHJCode) && !string.IsNullOrEmpty(bbhjInfo.Volunteer))
            {
                BBHJCaseModel model = new BBHJCaseModel()
                {
                    BBHJInfoID = bbhjInfo.BBHJInfoID,
                    BBHJCode = bbhjInfo.BBHJCode,
                    Volunteer = bbhjInfo.Volunteer,
                    Remarks = bbhjInfo.Remark
                };
                return View(model);
            }
            else
            {
                BBHJCaseModel model = new BBHJCaseModel()
                {
                    BBHJInfoID = id
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditBBHJInfo(BBHJCaseModel model)
        {
            if (ModelState.IsValid)
            {
                if (IBBHJInfo.UpdateBBHJInfoById(model))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


    }
}