using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_ejecucionManual : System.Web.UI.Page
    {
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
        HttpContext context = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (context.Session["frm_ejecucionManual.aspx"].ToString() == "1")
                {
                    if (!IsPostBack)
            {
            }
                }
                else
                {
                    panGrid.Visible = false;
                    fn_alerta("RJ", "Error: Debe registrarse para poder ver esta página");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("frm_login.aspx");
            }
        }

        public void fn_alerta(String tipo_error, String mensaje)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SomestartupScript", "myFunction('" + mensaje + "', '" + tipo_error + "');", true);
        }

        protected void btn_ejecutar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            WS_GERENTES.cl_salida ms = ws.fn_ejectaDemonio(Session["SES_NOMBRE_USR"].ToString());
            if (ms.ai_num_error == 0)
            {
                fn_alerta("VR", ms.as_error);
            }
            else
            {
                fn_alerta("RJ", ms.as_error);
            }
        }

        protected void btn_ejecutaExt_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            WS_GERENTES.cl_salida ms = ws.fn_cargaAcrchivo_extractor(DateTime.Now.ToString("yyyyMMdd"));
            if (ms.ai_num_error == 0)
            {
                fn_alerta("VR", ms.as_error);
            }
            else
            {
                fn_alerta("RJ", ms.as_error);
            }
        }

        protected void btn_ejecutaProy_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            WS_GERENTES.cl_salida ms = ws.fn_cargaAcrchivo_proyeccion(DateTime.Now.ToString("yyyyMMdd"));
            if (ms.ai_num_error == 0)
            {
                fn_alerta("VR", ms.as_error);
            }
            else
            {
                fn_alerta("RJ", ms.as_error);
            }
        }

        protected void btn_ejectuaVent_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            WS_GERENTES.cl_salida ms = ws.fn_cargaAcrchivo_ventas(DateTime.Now.ToString("yyyyMMdd"));
            if (ms.ai_num_error == 0)
            {
                fn_alerta("VR", ms.as_error);
            }
            else
            {
                fn_alerta("RJ", ms.as_error);
            }
        }
    }
}