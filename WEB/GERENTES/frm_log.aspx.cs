using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_log : System.Web.UI.Page
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
                // if (context.Session["frm_usuario.aspx"].ToString() == "1")
                //{
                if (!IsPostBack)
                {
                    txt_fechaIni.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txt_fechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    Session["Filtro"] = false;
                    Session["ordenar"] = "";
                    fn_llenarGrid();
                }
                /* }
                 else
                 {
                     panCrear.Visible = false;
                     panGrid.Visible = false;
                     fn_alerta(2, "Error: Debe registrarse para poder ver esta página");
                 }*/
            }
            catch (Exception ex)
            {
                //Response.Redirect("frm_login.aspx");
            }

        }

        protected void btn_cerrarPopMen_Click(object sender, EventArgs e)
        {
            pn_alerta.Visible = false;
        }
        public void fn_alerta(String tipo_error, String mensaje)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SomestartupScript", "myFunction('" + mensaje + "', '" + tipo_error + "');", true);
        }

        public void fn_llenarGrid()
        {
            try
            {
                bool filtro = (bool)(context.Session["Filtro"]);
                if (!filtro)
                {
                    WS_GERENTES.cl_salida ms = ws.slt_gn_log(txt_fechaIni.Text.Trim(), txt_fechaFin.Text.Trim());
                    if (ms.ai_num_error == 0)
                    {
                        dt = ms.adt_datos.Tables[0];
                        Session["data"] = dt;
                    }
                    else
                        throw new Exception(ms.as_error);
                }
                else
                    dt = (DataTable)(context.Session["data"]);

                if (dt.Rows.Count == 0)
                {
                    fn_alerta("AZ", "No se encontraon registros para su búsqueda.");
                    pn_alerta.Visible = true;
                }
                DataView dataView = new DataView(dt);
                dataView.Sort = (string)(context.Session["ordenar"]);
                grid.ShowHeader = true;
                grid.DataSource = dataView;
                grid.DataBind();
                Session["data"] = dataView.ToTable();
            }
            catch (Exception ex)
            {
                fn_alerta("RJ", ex.Message);
            }
        }


        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt_grid = (DataTable)Session["data"];
            grid.PageIndex = e.NewPageIndex;
            grid.DataSource = dt_grid;
            grid.DataBind();

        }
        protected void grid_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            fn_llenarGrid();
        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            if (ws.dlt_gn_log() > 0)
                fn_alerta("VR", "Logs limpiados con éxito.");
            fn_llenarGrid();

        }
    }
}