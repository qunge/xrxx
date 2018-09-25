using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.SPI;
using SRIS.Framework;
using SRIS.Model;

namespace SRIS.DAL
{
    /// <summary>
    /// 登记案例的信息操作类
    /// </summary>
    public class RegisterInfoDal : IRegisterInfo
    {
        /// <summary>
        /// 获取所有的登记案例的信息
        /// </summary>
        /// <returns></returns>
        public List<RegisterModel> GetAllCaseInfo(string userId, int page, int limit, Dictionary<string, string> whereDic, out int pageCount)
        {
            using (var db = new SRISDBEntities())
            {
                //List<RegisterInfo> list = db.RegisterInfo
                //    .Where(n => n.UserInfo.UserInfoID == userId && n.IsReturnTask == 0 && n.IsBBHJ == "0" && n.IsDelete == 0 && n.IsSuccess == 0)
                //    //.Include(t => t.SRType)
                //    .OrderByDescending(s => s.GetTaskDateTime)
                //    .ToList();

                List<RegisterModel> list = (from n in db.RegisterInfo
                                           join p in db.SRType
                                           on n.SRTypeID equals p.SRTypeID
                                           where n.UserInfoID == userId && n.IsReturnTask == 0 && n.IsBBHJ == 0 && n.IsDelete == 0 && n.IsSuccess == 0
                                           orderby n.GetTaskDateTime descending
                                           select new RegisterModel() { RegisterInfo=n,SRTypeName=p.SRTypeName}).ToList();

                if (whereDic != null)
                {
                    if (!string.IsNullOrEmpty(whereDic["caseCode"]))
                    {
                        list = list.Where(n => n.RegisterInfo.CaseCode.Contains(whereDic["caseCode"])).ToList();
                    }
                    if (!string.IsNullOrEmpty(whereDic["srTypeId"]))
                    {
                        list = list.Where(n => n.RegisterInfo.SRTypeID == whereDic["srTypeId"]).ToList();
                    }
                    if (!string.IsNullOrEmpty(whereDic["beSeekerName"]))
                    {
                        list = list.Where(n => n.RegisterInfo.BeSeekerName.Contains(whereDic["beSeekerName"])).ToList();
                    }
                    if (!string.IsNullOrEmpty(whereDic["getTaskDate"]))
                    {
                        list = list.Where(n => n.RegisterInfo.GetTaskDateTime == Convert.ToDateTime(whereDic["getTaskDate"])).ToList();
                    }
                }
                pageCount = list.Count;
                if (page > 0 && limit > 0)
                {
                    list = list.Skip((page - 1) * limit).Take(limit).ToList();
                }
                return list;
            }

        }

        /// <summary>
        /// 获取所有的登记案例的信息
        /// </summary>
        /// <returns></returns>
        public List<RegisterModel> GetAllCaseInfo(string userId)
        {
            using (var db = new SRISDBEntities())
            {
                List<RegisterModel> list = (from n in db.RegisterInfo
                                            join p in db.SRType
                                            on n.SRTypeID equals p.SRTypeID
                                            where n.UserInfoID == userId && n.IsReturnTask == 0 && n.IsBBHJ == 0 && n.IsDelete == 0 && n.IsSuccess == 0
                                            orderby n.GetTaskDateTime descending
                                            select new RegisterModel() { RegisterInfo = n, SRTypeName = p.SRTypeName }).ToList();
                return list;
            }

        }

        /// <summary>
        /// 通过案例ID获取案例信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public RegisterInfo GetRegisterInfoById(string Id)
        {
            using (var db = new SRISDBEntities())
            {
                return db.RegisterInfo.Find(Id);
            }
        }

        /// <summary>
        /// 获取所有的案例类型
        /// </summary>
        /// <returns></returns>
        public List<SRType> GetSRType()
        {
            using (var db = new SRISDBEntities())
            {
                List<SRType> SRTypeList = db.SRType.ToList();
                return SRTypeList;
            }
        }

        /// <summary>
        /// 创建一条案例信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateRegister(RegisterInfo model)
        {
                using (var db = new SRISDBEntities())
                {
                    db.RegisterInfo.Add(model);
                    FollowDetails fdModel = new FollowDetails() {
                        CaseId = model.RegisterInfoID,
                        CreateDataTime = DateTime.Now,
                        Id = System.Guid.NewGuid().ToString(),
                        ImageUrl = "",
                        MesText = "添加案例"
                        
                    };
                    db.FollowDetails.Add(fdModel);

                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
        }

        /// <summary>
        /// 通过案例类型ID获取案例类型
        /// </summary>
        /// <param name="id">例类型ID</param>
        /// <returns></returns>
        public SRType GetSRTypeById(string id)
        {
            using (var db = new SRISDBEntities())
            {
                return db.SRType.Where(n => n.SRTypeID == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过案例ID获取联系人信息
        /// </summary>
        /// <param name="CaseId">案例ID</param>
        /// <returns></returns>
        public LinkMan GetLinManModelById(string CaseId)
        {
            using (var db = new SRISDBEntities())
            {
                LinkMan model = db.LinkMan.Where(n => n.RegisterInfoId == CaseId).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 创建联系人信息
        /// </summary>
        /// <param name="model">联系人信息实体类</param>
        /// <returns></returns>
        public bool CreateLinkMan(LinkMan model)
        {
            using (var db = new SRISDBEntities())
            {
                db.LinkMan.Add(model);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改联系人信息
        /// </summary>
        /// <param name="model">联系人信息实体类</param>
        /// <returns></returns>
        public bool UpdateLinkMan(LinkMan model)
        {
            using (var db = new SRISDBEntities())
            {
                LinkMan linkManModel = db.LinkMan.Find(model.LinkManID);
                linkManModel.Address = model.Address;
                linkManModel.Birthday = model.Birthday;
                linkManModel.Career = model.Career;
                linkManModel.CreateDateTime = model.CreateDateTime;
                linkManModel.Email = model.Email;
                linkManModel.IdCardNo = model.IdCardNo;
                linkManModel.LinkManName = model.LinkManName;
                linkManModel.OtherLink = model.OtherLink;
                linkManModel.Phone = model.Phone;
                linkManModel.QQ = model.QQ;
                linkManModel.Relationship = model.Relationship;
                linkManModel.Remark = model.Remark;
                linkManModel.Sex = model.Sex;
                linkManModel.TelPhone = model.TelPhone;
                linkManModel.WeiXin = model.WeiXin;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 修改案例信息
        /// </summary>
        /// <param name="model">案例信息实体类</param>
        /// <returns></returns>
        public bool UpdateRegisterInfo(RegisterInfo model)
        {
            using (var db = new SRISDBEntities())
            {
                RegisterInfo md = db.RegisterInfo.Find(model.RegisterInfoID);
                md.BeSeekerName = model.BeSeekerName;
                md.CaseCode = model.CaseCode;
                md.GetTaskDateTime = model.GetTaskDateTime;
                md.PostLink = model.PostLink;
                md.RegisterLink = model.RegisterLink;
                md.Remarks = model.Remarks;
                md.SRTypeID = model.SRTypeID;
                md.Title = model.Title;
                md.IsReturnTask = model.IsReturnTask;
                md.ReturnReason = model.ReturnReason;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 删除案例登记信息
        /// </summary>
        /// <param name="id">案例登记ID</param>
        /// <returns></returns>
        public bool DelRegisterInfo(string id)
        {
            using (var db = new SRISDBEntities())
            {
                RegisterInfo model = db.RegisterInfo.Find(id);
                model.IsDelete = 1;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 设置为宝贝回家案例
        /// </summary>
        /// <param name="id">要设置的案例ID</param>
        /// <returns></returns>
        public bool Bbhj(string id)
        {
            using (var db = new SRISDBEntities())
            {
                var tran = db.Database.BeginTransaction();
                try
                {
                    // 修改案例状态为宝贝回家案例
                    RegisterInfo model = db.RegisterInfo.Find(id);
                    model.IsBBHJ = 1;

                    // 宝贝回家表添加数据
                    BBHJInfo bbModel = new BBHJInfo()
                    {
                        BBHJCode = "",
                        BBHJInfoID = System.Guid.NewGuid().ToString(),
                        CreateDateTime = DateTime.Now,
                        GuideTime = DateTime.Now,
                        RegisterInfoID = id,
                        Remark = "",
                        Volunteer = ""
                    };
                    db.BBHJInfo.Add(bbModel);

                    db.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    return false;
                }

            }
        }

        /// <summary>
        /// 设置为成功案例
        /// </summary>
        /// <param name="id">案例ID</param>
        /// <returns></returns>
        public bool SetSuccess(string id)
        {
            using (var db = new SRISDBEntities())
            {
                RegisterInfo model = db.RegisterInfo.Find(id);
                if (model != null)
                {
                    model.IsSuccess = 1;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
