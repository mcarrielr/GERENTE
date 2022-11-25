using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Web.Security.AntiXss;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace GERENTES
{
    public partial class frm_login : System.Web.UI.Page
    {
        private const string AntiCsrfTokenKey = "__AntiCsrfToken";
        private const string AntiCsrfUserNameKey = "__AntiCsrfUserName";
        private string _antiCsrfTokenValue;
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
        HttpContext context = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
           // informativo.Visible = false;
            if (!IsPostBack)
            {
                Session.Clear();
            }
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
        protected void btn_ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                Regex validation=new Regex(@"^[a-zA-Z0-9_\-]");
                WS_GERENTES.cl_salida ms = ws.slt_logeo(txt_alias.Text.Trim(), txt_clave.Text.Trim());                
                if (ms.ai_num_error == 0)
                {
                        Session.Clear();
                        context.Session.Clear();
                    //System.Configuration.ConfigurationManager.AppSettings["bodegasSaltarProceso"].Split(',');
                    Session["ERROR"] = "";
                    if (validation.IsMatch(ms.adt_datos.Tables[0].Rows[0]["APELLIDO_USR"].ToString()) && ms.adt_datos.Tables[0].Rows[0]["APELLIDO_USR"].ToString() != "" && validation.IsMatch(ms.adt_datos.Tables[0].Rows[0]["NOMBRE_USR"].ToString()) && ms.adt_datos.Tables[0].Rows[0]["NOMBRE_USR"].ToString() != "")
                    {
                        context.Session.Add("SES_NOMBRE_USR", ms.adt_datos.Tables[0].Rows[0]["APELLIDO_USR"].ToString() + " " + ms.adt_datos.Tables[0].Rows[0]["NOMBRE_USR"].ToString());
                    }
                    if (validation.IsMatch(ms.adt_datos.Tables[0].Rows[0]["SES_CODIGO_USR"].ToString()) && ms.adt_datos.Tables[0].Rows[0]["SES_CODIGO_USR"].ToString() != "" )
                    {
                        context.Session.Add("SES_CODIGO_USR", ms.adt_datos.Tables[0].Rows[0]["CODIGO_USR"].ToString());
                    }
                    
                    if (validation.IsMatch(ms.adt_datos.Tables[0].Rows[0]["CODIGO_CEN"].ToString()) &&  ms.adt_datos.Tables[0].Rows[0]["CODIGO_CEN"].ToString() != "" && validation.IsMatch(ms.adt_datos.Tables[0].Rows[0]["DESCRIPCION_CEN"].ToString()) && ms.adt_datos.Tables[0].Rows[0]["DESCRIPCION_CEN"].ToString() != "")
                    {
                        context.Session.Add("SES_CODIGO_CEN", ms.adt_datos.Tables[0].Rows[0]["CODIGO_CEN"].ToString());
                        context.Session.Add("SES_DESCRIPCION_CEN", ms.adt_datos.Tables[0].Rows[0]["DESCRIPCION_CEN"].ToString());
                    }
                    else
                    {
                        context.Session.Add("SES_DESCRIPCION_CEN", "");
                        context.Session.Add("SES_CODIGO_CEN", 0);
                    }
                    Random ram = new Random();
                    Response.Redirect("frm_inicio.aspx?r=" + ram.ToString());
                }
                else
                {
                    //informativo.Visible = true;
                    //Image1.ImageUrl = "~/images/iconos/error.png";
                    //AntiXssEncoder.HtmlEncode(ms.as_error, false);
                    lbl_mensaje.Text = AntiXssEncoder.HtmlEncode(ms.as_error, false);
                }
            }
            catch(Exception ex)
            {
                //informativo.Visible = true;
                //Image1.ImageUrl = "~/images/iconos/error.png";
                lbl_mensaje.Text = "Error de conexión con el Servidor.";
            }
        }
    }
}