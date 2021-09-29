using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFormsAuthentication.ViewModel;
using MvcFormsAuthentication.Data;
using MvcFormsAuthentication.Models;
using System.Web.Security;
using System.Net;
using MvcFormsAuthentication.ViewModels;
using MvcFormsAuthentication.Service;

namespace MvcFormsAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccounIndexContext _ctx;
        public AccountController()
        {
            _ctx = new AccounIndexContext();
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginVM)
        {
            //一.未通過Model驗證
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }

            //二.通過Model驗證後,使用HtmlEncode將帳密做HTML編碼,去除有害的字元
            string name = HttpUtility.HtmlEncode(loginVM.Name);
            string password = HttpUtility.HtmlEncode(HttpUtility.HtmlEncode(loginVM.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表紀錄
            Account user = _ctx.Accounts.Where(x => x.Name.ToUpper() == name && x.Password == password).FirstOrDefault();
            //Account user = _ctx.Accounts.Single(x => x.Name.ToUpper() == name && x.Password == password);
            //Account user2 = _ctx.Accounts.SingleOrDefault(x => x.Name.ToUpper() == name.ToUpper() && x.Password == password);

            //找不到則彈回Login頁
            if(user == null)
            {
                ModelState.AddModelError("Password", "無效的帳號或密碼!");

                return View(loginVM);
            }


            //1.建立FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.Name.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: loginVM.Remember,// 是否要記住我 true or false
            userData: user.Id.ToString(), //可以放使用者角色名稱
            cookiePath: FormsAuthentication.FormsCookiePath);

            //2.加密Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            //4.取得original URL.
            var url = FormsAuthentication.GetRedirectUrl(name, true);

            //5.導向original URL
            return Redirect(url);

        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public ActionResult UserProfile(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Account account = _ctx.Accounts.Find(id);
            if(account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(Account account)
        {
            account.Name = HttpUtility.HtmlEncode(account.Name);
            account.Password = HttpUtility.HtmlEncode(account.Password);
            account.Email = HttpUtility.HtmlEncode(account.Email);
            account.Mobile= HttpUtility.HtmlEncode(account.Mobile);

            using (var tran = _ctx.Database.BeginTransaction())
            {
                try
                {
                    _ctx.Entry(account).State = System.Data.Entity.EntityState.Modified;
                    _ctx.SaveChanges();
                    tran.Commit();
                    return Content("寫入資料成功");
                }
                catch(Exception ex)
                {
                    tran.Rollback();

                    return Content("寫入資料失敗: " + ex.ToString());;
                }
            }
                
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            string name = HttpUtility.HtmlEncode(registerVM.Name);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(registerVM.Password));
            string email = HttpUtility.HtmlEncode(registerVM.Email);
            string mobile = HttpUtility.HtmlEncode(registerVM.Mobile);
            //View Model -> Data Model
            Account account = new Account
            {
                Name = name,
                Password = password,
                Email = email,
                Mobile = mobile
            };
            using (var tran = _ctx.Database.BeginTransaction())
            {
                try
                {
                    _ctx.Accounts.Add(account);
                    _ctx.SaveChanges();
                    tran.Commit();
                    return Content("新增帳號成功");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return Content("新增帳號失敗:" + ex.ToString());
                }
            }
        }
    }
}