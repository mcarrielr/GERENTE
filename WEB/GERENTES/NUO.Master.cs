using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class NUO : System.Web.UI.MasterPage
    {
        private const string AntiCsrfTokenKey = "__AntiCsrfToken";
        private const string AntiCsrfUserNameKey = "__AntiCsrfUserName";
        private string _antiCsrfTokenValue;
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
        HttpContext context = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
            fn_menu();
            if (Session["ERROR"].ToString() != "")
                ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "myFunction('" + Session["ERROR"].ToString() + "', 'RJ');", true);

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            var csrfCookie = Request.Cookies[AntiCsrfTokenKey];
            Guid csrfCookieValue;
            if (csrfCookie != null && Guid.TryParse(csrfCookie.Value, out csrfCookieValue))
            {
                // Use the anti-CSRF token from the cookie
                _antiCsrfTokenValue = csrfCookie.Value;
                Page.ViewStateUserKey = _antiCsrfTokenValue;

            }
            else
            {
                // Generate a new anti-CSRF token and save to the cookie
                _antiCsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiCsrfTokenValue;

                var responseCookie = new HttpCookie(AntiCsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiCsrfTokenValue
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
                // Set anti-CSRF token (using ViewState)
                ViewState[AntiCsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiCsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;

            }
            else
            {
                // Validate the anti-CSRF token and check
                if ((string)ViewState[AntiCsrfTokenKey] != _antiCsrfTokenValue
                    || (string)ViewState[AntiCsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-CSRF token failed.");
                }
            }
        }
        public void fn_menu()
        {
            try
            {
                string GRUPO = string.Empty;
                DataSet a1 = new DataSet();
                a1 = ws.slt_pagina_x_grupo(Convert.ToInt32(Session["SES_CODIGO_USR"].ToString()));
                pn_menu.Controls.Add(new LiteralControl("<nav id='menu'> <header class='major'> <h2>Menu</h2></header>"));
                pn_menu.Controls.Add(new LiteralControl("<ul>"));
                if (a1.Tables[0].Rows.Count > 0)
                {
                    pn_menu.Controls.Add(new LiteralControl("<li><a href='frm_inicio.aspx'>Inicio</a></li>"));
                    foreach (DataRow dr in a1.Tables[0].Rows)
                    {
                        if (GRUPO != dr["GRUPO_PAG"].ToString())
                        {
                            GRUPO = dr["GRUPO_PAG"].ToString();
                            DataSet dt_items = ws.slt_pagina_x_usuario(Convert.ToInt32(Session["SES_CODIGO_USR"].ToString()), dr["GRUPO_PAG"].ToString());
                            if (dt_items.Tables[0].Rows.Count > 1)
                            {
                                pn_menu.Controls.Add(new LiteralControl("<li> <span class='opener'>" + dr["GRUPO_PAG"].ToString() + " </span> <ul>"));
                                foreach (DataRow dr1 in dt_items.Tables[0].Rows)
                                {
                                    pn_menu.Controls.Add(new LiteralControl("<li><a href='" + dr1["link_PAG"].ToString() + "'>" + dr1["NOMBRE_PAG"].ToString() + "</a></li> "));
                                    Session.Add(dr1["link_PAG"].ToString(), 1);
                                }
                                pn_menu.Controls.Add(new LiteralControl("</ul></li>"));
                            }
                            else
                            {
                                Session.Add(dt_items.Tables[0].Rows[0].ItemArray[2].ToString(), 1);
                                pn_menu.Controls.Add(new LiteralControl("<li><a href='" + dt_items.Tables[0].Rows[0]["link_PAG"].ToString() + "'>" + dt_items.Tables[0].Rows[0]["NOMBRE_PAG"].ToString() + "</a></li>"));
                            }
                        }
                    }
                    pn_menu.Controls.Add(new LiteralControl("<li><a href='frm_login.aspx'><span class='icon-block'></span>Cerrar Sesión</a></li>"));
                }
                pn_menu.Controls.Add(new LiteralControl("</ul> </nav>"));
            }
            catch { Response.Redirect("frm_login.aspx"); }
        }
    }
}