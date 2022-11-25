using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_cambioContr : System.Web.UI.Page
    {
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
        private DataTable dt = new DataTable();
        private String ordenarUsu = " 1 asc";
        HttpContext context = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //ClientScript.RegisterStartupScript(GetType(), "", "myFunction('hola', 'RJ');", true);
                if (context.Session["frm_cambioContr.aspx"].ToString() == "1")
                {
                    if (!IsPostBack)
                {
                    div_actual.Visible = true;
                    div_contra.Visible = false;
                    btn_Guardar.Visible = false;
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

        protected void btn_siguiente_Click(object sender, EventArgs e)
        {
            try
            {
                WS_GERENTES.cl_salida ms = ws.slt_val_Contrasenia(Int32.Parse(context.Session["SES_CODIGO_USR"].ToString()), txt_contraseniaActual.Text.Trim());
                if (ms.ai_num_error == 0)
                {

                    btn_siguiente.Visible = false;
                    btn_Guardar.Visible = true;
                    div_actual.Visible = false;
                    div_contra.Visible = true;
                }
                else
                    throw new Exception(ms.as_error);
            }
            catch (Exception ex)
            {
                btn_siguiente.Visible = true;
                btn_Guardar.Visible = false;
                div_actual.Visible = true;
                div_contra.Visible = false;
                fn_alerta("RJ", ex.Message);
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (txt_contrasenia.Text.Trim() == txt_verContrasenia.Text.Trim())
            {
                if (ws.upd_contra(Int32.Parse(context.Session["SES_CODIGO_USR"].ToString()), txt_contrasenia.Text.Trim()) > 0)
                {
                    div_actual.Visible = true;
                    div_contra.Visible = false;
                    btn_Guardar.Visible = false;
                    btn_siguiente.Visible = true;

                    fn_alerta("VR", "Contraseña cambiada con éxito.");
                }
            }
            else
            {
                txt_verContrasenia.Text = "";
                txt_contrasenia.Text = "";
                fn_alerta("RJ", "Las contraseñas no coinciden.");
            }
        }
    }
}