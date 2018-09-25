using System;
using System.Web.Mvc;
using SRIS.SPI;
using SRIS.ViewModels;
using Newtonsoft.Json;
using SRIS.Framework;

namespace SRIS.Controllers
{
    public class AccountController : Controller
    {
        IUserInfo IUserInfo;
        public AccountController()
        {
            IUserInfo = new SRIS.DAL.UserInfoDal();
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserInfos userInfo)
        {
             UserInfo userInfoModel = IUserInfo.GetUserInfoByUserName(userInfo.UserName);
            // UserInfo userInfoModel = IUserInfo.GetUserInfoByUserName("涅炎");
            if (userInfoModel != null)
            {
                 string pwd = Common.EncryptDecrypt.MD5Encoding(userInfo.PassWord, userInfoModel.Salt);
               // string pwd = Common.EncryptDecrypt.MD5Encoding("qunge7758521", userInfoModel.Salt);
                if (pwd == userInfoModel.PassWord)
                {
                    ViewBag.Message = "登录成功";
                    Session.Timeout = 300;
                    Session["userName"] = userInfoModel.UserName;
                    Session["userId"] = userInfoModel.UserInfoID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "用户名或密码错误";
                    return View(userInfo);
                }
            }
            else
            {
                ViewBag.Message = "用户名或密码错误";
                return View(userInfo);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(string parame)
        {
            try
            {
                UserInfos userInfoParame = JsonConvert.DeserializeObject<UserInfos>(parame);
                UserInfo userInfo = IUserInfo.GetUserInfoByUserName(userInfoParame.UserName);
                if (userInfo != null)
                {
                    return Json(new { state = false, message = "用户名重复" });
                }
                string salt = Common.EncryptDecrypt.CreateSalt();
                UserInfo uf = new UserInfo()
                {
                    UserInfoID = System.Guid.NewGuid().ToString(),
                    Salt = salt,
                    PassWord = Common.EncryptDecrypt.MD5Encoding(userInfoParame.PassWord, salt),
                    UserName = userInfoParame.UserName,
                    CreateDateTime = DateTime.Now
                };
                bool result = IUserInfo.Register(uf);
                if (result)
                {
                    return Json(new { state = true, message = "注册成功" });
                }
                else
                {
                    return Json(new { state = false, message = "注册失败" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { state = false, message = ex.Message });
            }
        }

    }
}