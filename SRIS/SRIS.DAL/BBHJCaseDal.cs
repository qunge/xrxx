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
        public List<BBHJCaseModel> GetAllBBHJCase()
        {
            using (var db = new SRISDBEntities())
            {
                List<BBHJCaseModel> list = (from n in db.RegisterInfo
                                            join b in db.BBHJInfo on n.RegisterInfoID equals b.RegisterInfoID
                                            join t in db.SRType on n.SRTypeID equals t.SRTypeID
                                            where (n.IsBBHJ == "1" && n.IsDelete == 0 && n.IsReturnTask == 0 && n.IsSuccess == 0)
                                            orderby b.CreateDateTime descending
                                            select new BBHJCaseModel() {
                                                BBHJCode =b.BBHJCode,
                                                BeSeekerName =n.BeSeekerName,
                                                CaseCode=n.CaseCode,
                                                gTD=n.GetTaskDateTime,
                                                gT=b.GuideTime,
                                                RegisterLink=n.RegisterLink,
                                                Remarks=b.Remark,
                                                SRTypeName=t.SRTypeName,
                                                Title=n.Title,
                                                Volunteer=b.Volunteer,
                                                RegisterInfoId=n.RegisterInfoID,
                                                BBHJInfoID=b.BBHJInfoID
                                            }).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据案例编号ID修改宝贝案例信息
        /// </summary>
        /// <param name="model">宝贝案例信息实体</param>
        /// <returns></returns>
        public bool UpdateBBHJInfoById(BBHJCaseModel model)
        {
            using (var db = new SRISDBEntities())
            {
                BBHJInfo bbhjInfo = db.BBHJInfo.Find(model.BBHJInfoID);
                bbhjInfo.BBHJCode = model.BBHJCode;
                bbhjInfo.Volunteer = model.Volunteer;
                bbhjInfo.Remark = model.Remarks;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 获取宝贝回家案例信息
        /// </summary>
        /// <param name="Id">宝贝回家案例ID</param>
        /// <returns></returns>
        public BBHJInfo GetBBHJInfoByID(string Id)
        {
            using (var db = new SRISDBEntities())
            {
                return db.BBHJInfo.Find(Id);
            }
        }
    }
}
