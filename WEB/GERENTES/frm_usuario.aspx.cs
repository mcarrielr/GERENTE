using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_usuario : System.Web.UI.Page
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
                if (context.Session["frm_usuario.aspx"].ToString() == "1")
                {
                    if (!IsPostBack)
                {
                   
                    Session["Filtro"] = false;                    
                    Session["ordenar"] = "";
                    fn_cargarPerfil();
                    fn_cargarCentro();
                    fn_limpiar();
                    panCrear.Visible = false;
                    panRol.Visible = false;
                    panGrid.Visible = true;
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
                    WS_GERENTES.cl_salida ms = ws.slt_si_usuario(txt_busNombre.Text.Trim());
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
                fn_alerta("RJ","Error: "+ ex.Message.ToString().Replace("'",""));
            }
        }

        public void fn_limpiar()
        {
            panCrear.Visible = true;
            panRol.Visible = false;
            panGrid.Visible = false;
            context.Session["CODIGO_USR"] = 0;
            txt_alias.Text = "";
            txt_apellido.Text = "";
            txt_contrasenia.Text = "";
            txt_nombre.Text = "";
            lst_perfil.SelectedValue = "0";
            divReseteo.Visible = false;
            div_contrasenia.Visible = true;
        }

        public void fn_llenarListaRol()
        {
            try
            {
                WS_GERENTES.cl_salida ms = ws.rolUsuario(Int32.Parse(context.Session["CODIGO_USR"].ToString()));
                if (ms.ai_num_error == 0)
                {
                    DataSet dt = new DataSet();
                    dt = ms.adt_datos2;
                    listaGrupos_Izquierda.DataSource = dt;
                    listaGrupos_Izquierda.DataTextField = "DESCRIPCION_ROL";
                    listaGrupos_Izquierda.DataValueField = "CODIGO_ROL";
                    listaGrupos_Izquierda.DataBind();

                    dt = new DataSet();
                    dt = ms.adt_datos;
                    listaGrupos_Derech.DataSource = dt;
                    listaGrupos_Derech.DataTextField = "DESCRIPCION_ROL";
                    listaGrupos_Derech.DataValueField = "CODIGO_ROL";
                    listaGrupos_Derech.DataBind();
                }
                else
                    fn_alerta("RJ", ms.as_error);
            }
            catch
            {
            }
        }

        
        public void fn_cargarPerfil()
        {
            DataSet d = new DataSet();
            d = ws.slt_si_perfil_usuario();
            lst_perfil.DataSource = d;
            lst_perfil.DataTextField = "DESCRIPCION_PUS";
            lst_perfil.DataValueField = "CODIGO_PUS";
            lst_perfil.DataBind();
            lst_perfil.Items.Insert(0, new ListItem(".::SELECCIONAR::.", "0"));
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

        protected void CustomersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)(context.Session["data"]);
            if (e.CommandName == "ELIMINAR")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (ws.dlt_si_usuario(Int32.Parse(dt.Rows[grid.Rows[index].DataItemIndex]["CODIGO_USR"].ToString())) > 0)
                {
                    fn_alerta("VR", "Registro eliminado con éxito.");
                    fn_llenarGrid();
                }
                else
                    fn_alerta("RJ", "Error al eliminar el registro");
            }
        }
        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)(context.Session["data"]);
            context.Session["CODIGO_USR"] = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_USR"].ToString();
            txt_apellido.Text = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["APELLIDO_USR"].ToString();
            txt_alias.Text = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["ALIAS_USR"].ToString();
            txt_alias.Enabled = false;
            txt_nombre.Text = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["NOMBRE_USR"].ToString();
            lst_perfil.SelectedValue = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_PUS"].ToString();
            if (dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_CEN"].ToString() != "" && dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_CEN"].ToString() != null)
                lst_centro.SelectedValue = dt.Rows[grid.Rows[e.NewSelectedIndex].DataItemIndex]["CODIGO_CEN"].ToString();
            else
                lst_centro.SelectedValue = "";
            divReseteo.Visible = true;
            div_contrasenia.Visible = false;
            panCrear.Visible = true;
            panGrid.Visible = false;
            context.Session["frmUsuarioNEW"] = 0;
        }
        protected void gridShow_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)(context.Session["data"]);
            Context.Session["CODIGO_USR"] = dt.Rows[grid.Rows[e.RowIndex].DataItemIndex]["CODIGO_USR"].ToString();
            panRol.Visible = true;
            panGrid.Visible = false;
            fn_llenarListaRol();
            context.Session["frmUsuarioNEW"] = 0;
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
                if (context.Session["CODIGO_USR"].ToString() == "0")
                {
                    WS_GERENTES.cl_salida ms = ws.int_si_usuario(txt_nombre.Text.Trim(), txt_apellido.Text.Trim(), txt_alias.Text.Trim(), txt_contrasenia.Text.Trim(), Int32.Parse(lst_perfil.SelectedValue),lst_centro.SelectedValue);
                    if (ms.ai_num_error == 0)
                    {
                        fn_llenarGrid();
                        context.Session["frmUsuarioNEW"] = 1;
                        fn_alerta("VR", "Usuario creado con éxito.");
                        panCrear.Visible = false;
                        panGrid.Visible = false;
                        panRol.Visible = true;
                        context.Session["CODIGO_USR"] = ms.ai_id;
                        fn_llenarListaRol();
                    }
                    else
                        throw new Exception(ms.as_error);
                }
                else
                {
                    WS_GERENTES.cl_salida ms = ws.upd_si_usuario( Int32.Parse(context.Session["CODIGO_USR"].ToString()), txt_nombre.Text.Trim(), txt_apellido.Text.Trim(), txt_alias.Text.Trim(), 
                        txt_contrasenia.Text.Trim(), Int32.Parse(lst_perfil.SelectedValue), Int16.Parse(lst_cambioContrasenia.SelectedValue), lst_centro.SelectedValue);
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

        

        protected void btn_abrirBuscar_Click(object sender, EventArgs e)
        {
            fn_llenarGrid();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            fn_limpiar();
            txt_alias.Enabled = true;
        }

        protected void btnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = listaGrupos_Izquierda.Items.Count - 1; i >= 0; i--)
            {
                if (listaGrupos_Izquierda.Items[i].Selected == true)
                {
                    int codigo_pag = Convert.ToInt16(listaGrupos_Izquierda.Items[i].Value.ToString());
                    WS_GERENTES.cl_salida ms = ws.int_si_rol_usu(codigo_pag, Int32.Parse(context.Session["CODIGO_USR"].ToString()));
                    if(ms.ai_num_error==0)
                        fn_alerta("VR", ms.as_error);
                    else
                        fn_alerta("RJ", ms.as_error);
                }
            }
            fn_llenarListaRol();
        }

        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            for (int i = listaGrupos_Derech.Items.Count - 1; i >= 0; i--)
            {
                if (listaGrupos_Derech.Items[i].Selected == true)
                {
                    int codigo_pag = Convert.ToInt16(listaGrupos_Derech.Items[i].Value.ToString());
                    WS_GERENTES.cl_salida ms = ws.dlt_si_rol_usu(codigo_pag, Int32.Parse(context.Session["CODIGO_USR"].ToString()));
                    if (ms.ai_num_error == 0)
                        fn_alerta("VR", ms.as_error);
                    else
                        fn_alerta("RJ", ms.as_error);
                }
            }
            fn_llenarListaRol();
        }

        protected void btn_cerrarRol_Click(object sender, EventArgs e)
        {
            if (context.Session["frmUsuarioNEW"].ToString() == "1")
            {
                panGrid.Visible = false;
                panRol.Visible = false;
                panCrear.Visible = true;
            }
            else
            {
                panGrid.Visible = true;
                panRol.Visible = false;
                panCrear.Visible = false;
            }
        }

       

      
        public void fn_cargarCentro()
        {
            DataSet d = new DataSet();
            d = ws.slt_gn_centro("");
            lst_centro.DataSource = d;
            lst_centro.DataTextField = "COMPLETO";
            lst_centro.DataValueField = "CODIGO_CEN";
            lst_centro.DataBind();
            lst_centro.Items.Insert(0, new ListItem(".::NINGUNO::.", ""));
        }
        

        protected void btn_sigRol_Click(object sender, EventArgs e)
        {
            panGrid.Visible = true;
            panCrear.Visible = false;
            panRol.Visible = false;
        }

        protected void txt_alias_TextChanged(object sender, EventArgs e)
        {
            if (ws.slt_validaUsuario(txt_alias.Text.Trim()) >0)
            {
                fn_alerta("RJ", "Este nombre de usuario ya se encuentra registrado.");
                txt_alias.Text = "";
            }
        }
    }
       
}