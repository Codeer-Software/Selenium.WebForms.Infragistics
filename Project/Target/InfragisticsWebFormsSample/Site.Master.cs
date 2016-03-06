using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Infragistics.Web.UI.GridControls;

namespace InfragisticsWebFormsSample
{
    public partial class SiteMaster : MasterPage
    {
        List<Person> _Person = new List<Person>();



        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // 以下のコードは、XSRF 攻撃からの保護に役立ちます
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Cookie の Anti-XSRF トークンを使用します
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // 新しい Anti-XSRF トークンを生成し、Cookie に保存します
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Anti-XSRF トークンを設定します
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Anti-XSRF トークンを検証します
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Anti-XSRF トークンの検証が失敗しました。");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //_Person.Add(new Person() { FirstName = "敏", LastName = "松井", LastLoginDate = DateTime.Now, check1 = true, check2 = false });
            //_Person.Add(new Person() { FirstName = "達也", LastName = "石川", LastLoginDate = DateTime.Now, check1 = false, check2 = true });
            //_Person.Add(new Person() { FirstName = "太郎", LastName = "鈴木", LastLoginDate = DateTime.Now, check1 = true, check2 = true });
            //_webDataGrid.DataSource = _Person;


            //WebDataGrid
            //var aa = _webDataGrid.Rows[0].Items[0].Value;

            //Trace.Write("Trace!!");
            //Trace.Write(aa.ToString());
            //_webDataGrid.Behaviors.Activation.ActiveCell();

            //grid.get_behaviors().get_activation().set_activeCell(grid.get_rows().get_row(1).get_cell(1));



        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}