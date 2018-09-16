using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Model;
using SRIS.Framework;
using SRIS.SPI;

namespace SRIS.DAL
{
    /// <summary>
    /// 宝贝回家案例操作类
    /// </summary>
    public class BBHJCaseDal:IBBHJInfo
    {
        /// <summary>
        /// 获取所有的宝贝回家案例
        /// </summary>
        /// <returns></returns>
        public List<BBHJCaseModel> GetAllBBHJCase(string userid, int page, int limit, Dictionary<string, string> whereDic, out int pageCount)
        {
            using (var db = new SRISDBEntities())
            {
                List<BBHJCaseModel> list = (from n in db.RegisterInfo
                                            join b in db.BBHJInfo on n.RegisterInfoID equals b.RegisterInfoID
                                            join t in db.SRType on n.SRTypeID equals t.SRTypeID
                                            where (n.UserInfoID==userid && n.IsBBHJ == "1" && n.IsDelete == 0 && n.IsReturnTask == 0 && n.IsSuccess == 0)
                                            orderby b.CreateDateTime descending
                                            select new BBHJCaseModel() {
                                                BBHJCode =b.BBHJCode,
                                                BeSeekerName =n.BeSeekerName,
                                                CaseCode=n.CaseCode,
                                                GetTaskDate=n.GetTaskDateTime,
                                                GuideTime=b.GuideTime,
                                                RegisterLink=n.RegisterLink,
                                                Remarks=b.Remark,
                                                SRTypeID=t.SRTypeID,
                                                SRTypeName=t.SRTypeName,
                                                Title=n.Title,
                                                Volunteer=b.Volunteer,
                                                RegisterInfoId=n.RegisterInfoID,
                                                BBHJInfoID=b.BBHJInfoID
                                            }).ToList();
                if (whereDic != null)
                {
                    if (!string.IsNullOrEmpty(whereDic["caseCode"]))
                    {
                        list = list.Where(n => n.CaseCode.Contains(whereDic["caseCode"])).ToList();
                    }
                    if (!string.IsNullOrEmpty(whereDic["srTypeId"]))
                    {
                        list = list.Where(n => n.SRTypeID == whereDic["srTypeId"]).ToList();
                    }
                    if (!string.IsNullOrEmpty(whereDic["beSeekerName"]))
                    {
                        list = list.Where(n => n.BeSeekerName.Contains(whereDic["beSeekerName"])).ToList();
                    }
                    if (!string.IsNullOrEmpty(whereDic["getTaskDate"]))
                    {
                        list = list.Where(n => n.GetTaskDate == Convert.ToDateTime(whereDic["getTaskDate"])).ToList();
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
        /// 创建宝贝回家案例
        /// </summary>
        /// <param name="model">宝贝回家案例实体类</param>
        /// <returns></returns>
        public bool CreateBBHJCase(BBHJInfo model)
        {
            using (var db = new SRISDBEntities())
            {
                db.BBHJInfo.Add(model);
                db.SaveChanges();
                return true;
            }
 
        }
    }
}
