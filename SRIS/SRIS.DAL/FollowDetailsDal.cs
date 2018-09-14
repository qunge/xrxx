using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRIS.Framework;
using SRIS.SPI;

namespace SRIS.DAL
{
    /// <summary>
    /// 跟进详情操作类
    /// </summary>
    public class FollowDetailsDal:IFollowDetails
    {
        /// <summary>
        /// 通过案例ID获取该案例的跟进信息
        /// </summary>
        /// <param name="id">案例ID</param>
        /// <returns></returns>
        public List<SRIS.Model.FollowDetailsModel> GetList(string id)
        {
            using (var db = new SRISDBEntities())
            {
                var list = (from n in db.FollowDetails
                            where n.CaseId == id && n.IsDel == 0
                            orderby n.CreateDataTime descending
                            select new SRIS.Model.FollowDetailsModel() {
                                CaseId=n.CaseId,
                                CreateDataTime=n.CreateDataTime,
                                Id=n.Id,
                                ImageUrl=n.ImageUrl,
                                MesText=n.MesText
                            }).ToList();

                return list;
            }
        }

        /// <summary>
        /// 新建跟进信息
        /// </summary>
        /// <param name="imageUrl">上传图片</param>
        /// <param name="gjxx">跟进信息</param>
        /// <param name="id">案例ID</param>
        /// <returns></returns>
        public bool CreateFollowDetail(string imageUrl, string gjxx, string id)
        {
            using (var db = new SRISDBEntities())
            {
                FollowDetails model = new FollowDetails() {
                    CaseId=id,
                    CreateDataTime=DateTime.Now,
                    Id=Guid.NewGuid().ToString(),
                    ImageUrl=imageUrl,
                    MesText=gjxx
                };
                db.FollowDetails.Add(model);
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
        /// 删除跟进信息
        /// </summary>
        /// <param name="id">跟进信息ID</param>
        /// <returns></returns>
        public bool DelFollowDetail(string id)
        {
            using (var db = new SRISDBEntities())
            {
                FollowDetails model = db.FollowDetails.Find(id);
                model.IsDel = 1;
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
        /// 通过跟进信息ID获取跟进信息
        /// </summary>
        /// <param name="id">跟进信息ID</param>
        /// <returns></returns>
        public SRIS.Model.FollowDetailsModel GetModel(string id)
        {
            using (var db = new SRISDBEntities())
            {
               var list = (from n in db.FollowDetails
                            where n.Id == id && n.IsDel == 0
                            select new SRIS.Model.FollowDetailsModel() {
                                CaseId=n.CaseId,
                                CreateDataTime=n.CreateDataTime,
                                Id=n.Id,
                                ImageUrl=n.ImageUrl,
                                MesText=n.MesText
                            }).ToList();
                return list[0];
            }
        }

        /// <summary>
        /// 修改案例跟进信息
        /// </summary>
        /// <param name="model">案例跟进信息实体类</param>
        /// <returns></returns>
        public bool UpdateFollowDetails(FollowDetails model)
        {
            using (var db = new SRISDBEntities())
            {
                FollowDetails dfModel = db.FollowDetails.Find(model.Id);
                dfModel.ImageUrl = model.ImageUrl;
                dfModel.MesText = model.MesText;
                db.SaveChanges();
                return true;
            }
        }

    }
}
