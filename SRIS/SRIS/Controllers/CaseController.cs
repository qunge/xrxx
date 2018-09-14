using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRIS.Framework;
using SRIS.SPI;
using SRIS.DAL;
using Newtonsoft.Json;
using SRIS.ViewModels;
using SRIS.Model;
using System.Data;
using log4net;
using System.IO;

namespace SRIS.Controllers
{
    public class CaseController : Controller
    {
        IRegisterInfo IRegisterInfo;
        IFollowDetails IFollowDetails;
        public CaseController()
        {
            IRegisterInfo = new DAL.RegisterInfoDal();
            IFollowDetails = new DAL.FollowDetailsDal();
        }
        // GET: Case
        // 全部案例页面
        public ActionResult AllCase()
        {
            return View();
        }

        /// <summary>
        /// 获取所有案例信息
        /// </summary>
        /// <returns></returns>
        public string GetAllCase(int page, int limit, string whereStr)
        {
                int pageCount;
                // 案例类别下拉列表
                List<SelectListItem> itemList = new List<SelectListItem>();
                Dictionary<string, string> whereDic = null;
                if (!string.IsNullOrEmpty(whereStr))
                {
                    whereDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(whereStr);
                }
                string userId = Session["userId"].ToString();
                List<RegisterModel> list = IRegisterInfo.GetAllCaseInfo(userId, page, limit, whereDic, out pageCount);
                List<RegisterViewModel> modelList = new List<RegisterViewModel>();
                foreach (RegisterModel item in list)
                {
                    RegisterViewModel model = new RegisterViewModel();
                    model.BeSeekerName = item.RegisterInfo.BeSeekerName;
                    model.CaseCode = item.RegisterInfo.CaseCode;
                    model.getTaskDate = item.RegisterInfo.GetTaskDateTime.ToShortDateString();
                    model.IsBBHJ = item.RegisterInfo.IsBBHJ;
                    model.IsReturnTask = item.RegisterInfo.IsReturnTask;
                    model.PostLink = item.RegisterInfo.PostLink;
                    model.RegisterInfoId = item.RegisterInfo.RegisterInfoID;
                    model.RegisterLink = item.RegisterInfo.RegisterLink;
                    model.Remarks = item.RegisterInfo.Remarks;
                    model.SRTypeName = item.SRTypeName;
                    model.Title = item.RegisterInfo.Title;
                    modelList.Add(model);
                }
                RegisterCaseInfo data = new RegisterCaseInfo()
                {
                    code = 0,
                    count = pageCount,
                    data = modelList,
                    msg = ""
                };
               
                return JsonConvert.SerializeObject(data);
           
        }

        /// <summary>
        /// 创建案例
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateCase(string id = "")
        {
            RegisterInfo model = IRegisterInfo.GetRegisterInfoById(id);
            if (model == null)
            {
                SRTypelist();
                return View();
            }
            else
            {
                RegisterViewModel viewModel = new RegisterViewModel()
                {
                    BeSeekerName = model.BeSeekerName,
                    CaseCode = model.CaseCode,
                    GetTaskDateTime = model.GetTaskDateTime,
                    //getTaskDate=model.GetTaskDateTime.ToShortDateString(),
                    PostLink = model.PostLink,
                    RegisterInfoId = model.RegisterInfoID,
                    RegisterLink = model.RegisterLink,
                    Remarks = model.Remarks,
                    SRTypeId = model.SRTypeID,
                    Title = model.Title
                };
                SRTypelist();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult CreateCase(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegisterInfo md = IRegisterInfo.GetRegisterInfoById(model.RegisterInfoId);
                    if (md == null)
                    {
                        // 创建
                        RegisterInfo registerinfoModel = new RegisterInfo()
                        {
                            BeSeekerName = model.BeSeekerName,
                            CaseCode = model.CaseCode,
                            CreateDateTime = DateTime.Now,
                            DNAState = null,
                            GetTaskDateTime = model.GetTaskDateTime,
                            IsBBHJ = "0",
                            IsReturnTask = 0,
                            PostLink = "",
                            RegisterLink = model.RegisterLink,
                            RegisterInfoID = Guid.NewGuid().ToString(),
                            Remarks = model.Remarks,
                            ReturnReason = "",
                            ReturnTaskDateTime = null,
                            SRTypeID = model.SRTypeId,
                            Title = model.Title,
                            UserInfoID = Session["userId"].ToString()
                        };
                        if (IRegisterInfo.CreateRegister(registerinfoModel))
                        {
                            return RedirectToAction("AllCase");
                        }
                    }
                    else
                    {
                        // 修改
                        md.BeSeekerName = model.BeSeekerName;
                        md.CaseCode = model.CaseCode;
                        md.GetTaskDateTime = model.GetTaskDateTime;
                        md.PostLink = "";
                        md.RegisterLink = model.RegisterLink;
                        md.PostLink = model.PostLink == null ? "" : model.PostLink;
                        md.Remarks = model.Remarks;
                        md.SRTypeID = model.SRTypeId;
                        md.Title = model.Title;
                        md.RegisterInfoID = model.RegisterInfoId;
                        if (IRegisterInfo.UpdateRegisterInfo(md))
                        {
                            return RedirectToAction("AllCase");
                        }
                    }



                }
                SRTypelist();
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                SRTypelist();
                return View(model);
            }
        }

        private void SRTypelist()
        {
            // 获取所有的案例类型
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<SRType> SRTypeList = IRegisterInfo.GetSRType();
            SRTypeList.ForEach(o => {
                SelectListItem item = new SelectListItem()
                {
                    Value = o.SRTypeID.ToString(),
                    Text = o.SRTypeName
                };
                itemList.Add(item);
            });
            SelectList select = new SelectList(itemList, "Value", "Text");
            ViewBag.select = select;
        }

        public ActionResult EditCase(string caseId)
        {
            LinkMan model = IRegisterInfo.GetLinManModelById(caseId);
            if (model == null)
            {
                return View();
            }
            else
            {
                LinkManModel viewModel = new LinkManModel()
                {
                    Address = model.Address,
                    Birthday = model.Birthday,
                    Career = model.Career,
                    Email = model.Email,
                    IdCardNo = model.IdCardNo,
                    LinkManName = model.LinkManName,
                    OtherLink = model.OtherLink,
                    Phone = model.Phone,
                    QQ = model.QQ,
                    Relationship = model.Relationship,
                    Remark = model.Remark,
                    Sex = model.Sex,
                    TelPhone = model.TelPhone,
                    WeiXin = model.WeiXin,
                    LinkManID = model.LinkManID
                };
                return View(viewModel);
            }
        }

        public ActionResult EditLinManInfo(string id)
        {
            LinkMan model = IRegisterInfo.GetLinManModelById(id);
            if (model == null)
            {
                LinkManModel viewModel = new LinkManModel()
                {
                    RegisterInfoId = id
                };
                return View(viewModel);
            }
            else
            {
                LinkManModel viewModel = new LinkManModel()
                {
                    Address = model.Address,
                    Birthday = model.Birthday,
                    Career = model.Career,
                    Email = model.Email,
                    IdCardNo = model.IdCardNo,
                    LinkManName = model.LinkManName,
                    OtherLink = model.OtherLink,
                    Phone = model.Phone,
                    QQ = model.QQ,
                    Relationship = model.Relationship,
                    Remark = model.Remark,
                    Sex = model.Sex,
                    TelPhone = model.TelPhone,
                    WeiXin = model.WeiXin,
                    LinkManID = model.LinkManID,
                    RegisterInfoId = model.RegisterInfoId
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditLinManInfo(LinkManModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LinkMan LinManmodel = IRegisterInfo.GetLinManModelById(model.RegisterInfoId);

                    if (LinManmodel == null)
                    {
                        // 新建
                        LinkMan linkMan = new LinkMan()
                        {
                            Address = model.Address,
                            Birthday = model.Birthday,
                            Career = model.Career,
                            CreateDateTime = DateTime.Now,
                            Email = model.Email,
                            IdCardNo = model.IdCardNo,
                            LinkManID = System.Guid.NewGuid().ToString(),
                            LinkManName = model.LinkManName,
                            OtherLink = model.OtherLink,
                            Phone = model.Phone,
                            QQ = model.QQ,
                            RegisterInfoId = model.RegisterInfoId,
                            Relationship = model.Relationship,
                            Remark = model.Remark,
                            Sex = model.Sex,
                            TelPhone = model.TelPhone,
                            WeiXin = model.WeiXin
                        };
                        if (IRegisterInfo.CreateLinkMan(linkMan))
                        {
                            ViewBag.Message = "添加联系人成功";
                            return View("EditLinManInfo");
                        }
                    }
                    else
                    {
                        // 编辑信息
                        LinManmodel.Address = model.Address;
                        LinManmodel.Birthday = model.Birthday;
                        LinManmodel.Career = model.Career;
                        LinManmodel.CreateDateTime = DateTime.Now;
                        LinManmodel.Email = model.Email;
                        LinManmodel.IdCardNo = model.IdCardNo;
                        LinManmodel.LinkManID = model.LinkManID;
                        LinManmodel.LinkManName = model.LinkManName;
                        LinManmodel.OtherLink = model.OtherLink;
                        LinManmodel.Phone = model.Phone;
                        LinManmodel.QQ = model.QQ;
                        LinManmodel.RegisterInfoId = model.RegisterInfoId;
                        LinManmodel.Relationship = model.Relationship;
                        LinManmodel.Remark = model.Remark;
                        LinManmodel.Sex = model.Sex;
                        LinManmodel.TelPhone = model.TelPhone;
                        LinManmodel.WeiXin = model.WeiXin;
                        if (IRegisterInfo.UpdateLinkMan(LinManmodel))
                        {
                            ViewBag.Message = "修改联系人成功";
                            return View("EditLinManInfo");
                        }
                    }
                }
                ViewBag.Message = "修改联系人失败";
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(model);
            }
        }

        /// <summary>
        /// 退任务
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnTask()
        {
            return View();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">要删除任务的ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DelRegisterInfo(string Id)
        {
            var obj = new { state = false, message = "" };
            try
            {
                if (IRegisterInfo.DelRegisterInfo(Id))
                {
                    obj = new { state = true, message = "删除成功" };
                }
                else
                {
                    obj = new { state = false, message = "删除失败" };
                }
            }
            catch (Exception e)
            {
                obj = new { state = false, message = e.Message };
            }
            return Json(obj);
        }

        /// <summary>
        /// 设置为宝贝回家案例
        /// </summary>
        /// <param name="id">案例编号</param>
        /// <returns></returns>
        public JsonResult ydbbhj(string id)
        {
            var obj = new { state = false, message = "" };
            if (!string.IsNullOrEmpty(id))
            {
                if (IRegisterInfo.Bbhj(id))
                {
                    obj = new { state = true, message = "设置成功" };
                }
                else
                {
                    obj = new { state = false, message = "设置失败" };
                }
            }
            else
            {
                obj = new { state = false, message = "案例编号不能为空" };
            }
            return Json(obj);
        }

        /// <summary>
        /// 设置为成功案例
        /// </summary>
        /// <param name="id">案例编号</param>
        /// <returns></returns>
        public JsonResult SetSuccess(string id)
        {
            var obj = new { state=false,message=""};
            if (!string.IsNullOrEmpty(id))
            {
                if (IRegisterInfo.SetSuccess(id))
                {
                    obj = new { state = true, message = "设置成功" };
                }
                else
                {
                    obj = new { state = false, message = "设置失败" };
                }
            }
            else
            {
                obj = new { state = false, message = "案例编号不能为空" };
            }
            return Json(obj);
        }

        //public FileResult CaseExport()
        //{
        //    // 获取需要导出的数据
        //    int pageCount = 0;
        //    List<RegisterModel> list = IRegisterInfo.GetAllCaseInfo(Session["userId"].ToString(), 0, 0, null, out pageCount);
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("cName");

        //    foreach (var info in list.)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr[] = ;
        //        dt.Rows.Add(dr);
        //    }

        //}



        public ActionResult FollowDetails()
        {
            return View();
        }

        
        public string GetFollowDetails(string id)
        {
            var obj = new { status = false, data = new List<SRIS.Model.FollowDetailsModel>()  };
            List<SRIS.Model.FollowDetailsModel> list = IFollowDetails.GetList(id);
            if (list.Count > 0)
            {
                obj = new { status = true, data = list };
                
            }

            return JsonConvert.SerializeObject(obj);
        }


        public ActionResult AddFollowDetails()
        {
            return View();
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = "/Upload/" + filePathName
            });

        }

        public string CreateFollow(string imageUrl,string gjxx,string id)
        {
            var obj = new { status = false, message = "" };
            if (!string.IsNullOrEmpty(gjxx))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    bool result = IFollowDetails.CreateFollowDetail(imageUrl, gjxx, id);
                    if (result)
                    {
                        obj = new { status = true, message = "添加跟进信息成功" };
                    }
                    else
                    {
                        obj = new { status = false, message = "添加跟进信息失败" };
                    }
                }
                else
                {
                    obj = new { status = false, message = "没有获取到案例ID" };
                }
            }
            else
            {
                obj = new { status = false, message = "跟进信息不能为空" };
            }
            return JsonConvert.SerializeObject(obj);
        }

        public string DelFollowDetails(string id)
        {
            var obj = new { status = false, message = "" };
            try
            {
                bool result = IFollowDetails.DelFollowDetail(id);
                if (result)
                {
                    obj = new { status = true, message = "删除成功" };
                }
                else
                {
                    obj = new { status = false, message = "删除失败" };
                }
            }
            catch (Exception e)
            {
                obj = new { status = false, message = e.Message };
            }
            return JsonConvert.SerializeObject(obj);
        }

        public ViewResult UpdateFollowDetails(string id)
        {
            FollowDetailsModel md = IFollowDetails.GetModel(id);
            SRIS.ViewModels.FollowDetailsInfo model = new FollowDetailsInfo() {
                CaseId=md.CaseId,
                CreateDataTime=md.CreateDataTime,
                Id=md.Id,
                ImageUrl=md.ImageUrl,
                MesText=md.MesText
            };
            return View(model);
        }

        [HttpPost]
        public string UpdateFollowDetails(string imageUrl,string gjxx,string id)
        {
            var obj = new { status = false, message = "" };
            SRIS.Framework.FollowDetails model = new Framework.FollowDetails() {
                Id=id,
                ImageUrl=imageUrl,
                MesText=gjxx
            };
            if (IFollowDetails.UpdateFollowDetails(model))
            {
                obj = new { status = true, message = "修改成功" };
            }
            else
            {
                obj = new { status = false, message = "修改失败" };
            }
            return JsonConvert.SerializeObject(obj);
        }
    }
}