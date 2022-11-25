using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_rol : System.Web.UI.Page
    {
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
        private DataTable dt = new DataTable();
        private String ordenarUsu = " 1 asc";
        HttpContext context = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (context.Session["frm_rol.aspx"].ToString() == "1")
                {
                    if (!IsPostBack)
                {
                    Session["Filtro"] = false;
                    Session["ordenar"] = "";
                    panCrear.Visible = false;
                    pan_pagina.Visible = false;
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
                    dt = ws.slt_si_rol().Tables[0];
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
            pan_pagina.Visible = false;
            context.Session["CODIGO_ROL"] = 0;
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
            context.Session["CODIGO_ROL"] = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_ROL"].ToString();
            context.Session["DESCRIPCION_ROL"] = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["DESCRIPCION_ROL"].ToString();
            txt_nombre.Text = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["DESCRIPCION_ROL"].ToString();
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
                if (context.Session["CODIGO_ROL"].ToString() == "0")
                {
                    WS_GERENTES.cl_salida ms = ws.int_si_rol(txt_nombre.Text.Trim());
                    if (ms.ai_num_error == 0)
                    {
                        context.Session["CODIGO_ROL"] = ms.ai_id;
                        context.Session["DESCRIPCION_ROL"] = txt_nombre.Text.Trim();
                        fn_llenarListaPagina();
                        btn_pagina.Visible = true;
                        fn_llenarGrid();
                        fn_alerta("VR", "Usuario creado con éxito.");
                        panCrear.Visible = false;
                        panGrid.Visible = false;
                        pan_pagina.Visible = true;

                    }
                    else
                        throw new Exception(ms.as_error);
                }
                else
                {
                    WS_GERENTES.cl_salida ms = ws.upd_si_rol(Int32.Parse(context.Session["CODIGO_ROL"].ToString()), txt_nombre.Text.Trim());
                    if (ms.ai_num_error == 0)
                    {
                        fn_llenarGrid();
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
            btn_pagina.Visible = false;
        }

        public void fn_llenarListaPagina()
        {
            try
            {
                lbl_rol.Text = context.Session["DESCRIPCION_ROL"].ToString();
                WS_GERENTES.cl_salida ms = ws.rolPagina(Int32.Parse(context.Session["CODIGO_ROL"].ToString()));
                if (ms.ai_num_error == 0)
                {
                    DataSet dt = new DataSet();
                    dt = ms.adt_datos2;
                    listaGrupos_Izquierda.DataSource = dt;
                    listaGrupos_Izquierda.DataTextField = "NOMBRE_PAG";
                    listaGrupos_Izquierda.DataValueField = "CODIGO_PAG";
                    listaGrupos_Izquierda.DataBind();

                    dt = new DataSet();
                    dt = ms.adt_datos;
                    listaGrupos_Derech.DataSource = dt;
                    listaGrupos_Derech.DataTextField = "NOMBRE_PAG";
                    listaGrupos_Derech.DataValueField = "CODIGO_PAG";
                    listaGrupos_Derech.DataBind();
                }
                else
                    fn_alerta("RJ", ms.as_error);
            }
            catch
            {
            }
        }

        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = listaGrupos_Izquierda.Items.Count - 1; i >= 0; i--)
            {
                if (listaGrupos_Izquierda.Items[i].Selected == true)
                {
                    int codigo_pag = Convert.ToInt16(listaGrupos_Izquierda.Items[i].Value.ToString());
                    ws.int_si_menu_rol(Int32.Parse(context.Session["CODIGO_ROL"].ToString()), codigo_pag);
                }
            }
            fn_llenarListaPagina();
        }

        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = listaGrupos_Derech.Items.Count - 1; i >= 0; i--)
            {
                if (listaGrupos_Derech.Items[i].Selected == true)
                {
                    int codigo_pag = Convert.ToInt16(listaGrupos_Derech.Items[i].Value.ToString());
                    ws.dlt_si_menu_rol(Int32.Parse(context.Session["CODIGO_ROL"].ToString()), codigo_pag);
                }
            }
            fn_llenarListaPagina();
        }

        protected void btn_cerrarPagina_Click(object sender, EventArgs e)
        {
            panGrid.Visible = true;
            panCrear.Visible = false;
            pan_pagina.Visible = false;
        }

        protected void btn_pagina_Click(object sender, EventArgs e)
        {
            fn_llenarListaPagina();
            pan_pagina.Visible = true;
            panCrear.Visible = false;
        }
    }
}

