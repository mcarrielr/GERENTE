using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_perfilUsuario : System.Web.UI.Page
    {
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
    private DataTable dt = new DataTable();
        private String ordenarUsu = " 1 asc";
        HttpContext context = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (context.Session["frm_perfilUsuario.aspx"].ToString() == "1")
                {
                    if (!IsPostBack)
                {
                    Session["Filtro"] = false;
                    Session["ordenar"] = "";
                    panCrear.Visible = false;
                    fn_llenarGrid();
                }
                }
                else
                {
                    panCrear.Visible = false;
                    panGrid.Visible = false;
                    fn_alerta("RJ", "Error: Debe registrarse para poder ver esta página");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("frm_login.aspx");
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
                        dt = ws.slt_si_perfil_usuario().Tables[0];
                        Session["data"] = dt;                    
                }
                else
                    dt = (DataTable)(context.Session["data"]);

                if (dt.Rows.Count == 0)
                {
                    fn_alerta("RJ", "No se encontraon registros para su búsqueda.");
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
                fn_alerta("RJ", "Error: " + ex.Message.ToString().Replace("'", ""));
            }
        }

        public void fn_limpiar()
        {
            panCrear.Visible = true;
            panGrid.Visible = false;
            context.Session["CODIGO_PUS"] = 0;
            txt_nombre.Text = "";
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
        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)(context.Session["data"]);
            context.Session["CODIGO_PUS"] = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_PUS"].ToString();
            txt_nombre.Text = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["DESCRIPCION_PUS"].ToString();
            panCrear.Visible = true;
            panGrid.Visible = false;
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            panCrear.Visible = false;
            panGrid.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (context.Session["CODIGO_PUS"].ToString() == "0")
                {
                    WS_GERENTES.cl_salida ms = ws.int_si_perfil_usuario(txt_nombre.Text.Trim());
                    if (ms.ai_num_error == 0)
                    {
                        fn_alerta("VR", "Usuario creado con éxito.");
                        panCrear.Visible = false;
                        panGrid.Visible = true;
                    }
                    else
                        throw new Exception(ms.as_error);
                }
                else
                {
                    WS_GERENTES.cl_salida ms = ws.upd_si_perfil_usuario(Int32.Parse(context.Session["CODIGO_PUS"].ToString()), txt_nombre.Text.Trim());
                    if (ms.ai_num_error == 0)
                    {
                        fn_alerta("VR", "Usuario actualizado con éxito.");
                        panCrear.Visible = false;
                        panGrid.Visible = true;
                    }
                    else
                        throw new Exception(ms.as_error);
                }
            }
            catch (Exception ex)
            {
                fn_alerta("RJ", ex.Message);
            }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            fn_limpiar();
        }
    }
}
