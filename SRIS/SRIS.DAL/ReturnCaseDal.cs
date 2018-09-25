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
    /// 已退任务操作类
    /// </summary>
    public class ReturnCaseDal:IReturnCase
    {
        /// <summary>
        /// 获取已退任务
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="page">当前页号</param>
        /// <param name="limit">分页大小</param>
        /// <param name="whereDic">查询条件</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public List<RegisterModel> GetAllCaseInfo(string userId, int page, int limit, Dictionary<string, string> whereDic, out int pageCount)
        {
            using (var db = new SRISDBEntities())
            {
                List<RegisterModel> list = (from n in db.RegisterInfo
                                            join p in db.SRType
                                            on n.SRTypeID equals p.SRTypeID
                                            where n.UserInfoID == userId && n.IsReturnTask == 1 && n.IsDelete == 0
                                            orderby n.GetTaskDateTime descending
                                            select new RegisterModel() { RegisterInfo = n, SRTypeName = p.SRTypeName }).ToList();

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
    }
}
