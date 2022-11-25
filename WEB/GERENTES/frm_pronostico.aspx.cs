using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_pronostico : System.Web.UI.Page
    {
        WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
        private DataTable dt = new DataTable();
        HttpContext context = HttpContext.Current;
        private String ordenar = " 1 asc";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["frm_pronostico.aspx"].ToString() == "1")
                {
                    if (!IsPostBack)
                {
                    if (Session["SES_CODIGO_CEN"].ToString() == "0")
                    {
                        txt_busCentro.Text = "";
                        txt_busCentro.Enabled = true;
                    }
                    else
                    {
                        txt_busCentro.Text = Session["SES_CODIGO_CEN"].ToString();
                        txt_busCentro.Enabled = false;
                    }
                    pan2.Visible = false;
                    pan1.Visible = true;
                }
                }
                else
                {
                    fn_alerta("RJ", "Error: Debe registrarse para poder ver esta página");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("frm_login.aspx");
            }
        }

        protected void btn_regresarPan2_Click(object sender, EventArgs e)
        {
            pan2.Visible = false;
            pan1.Visible = true;
        }

        protected String nombreColumna(String indxColumna)
        {
            String sortExpression = "";
            switch (indxColumna)
            {
                case "1":
                    sortExpression = "CODIGO";
                    break;
                case "2":
                    sortExpression = "DESCRIPCION_PRD_PED";
                    break;
                case "3":
                    sortExpression = "CANTIDAD";
                    break;
                default:
                    sortExpression = "";
                    break;
            }
            return sortExpression;
        }

        protected String direccionOrder(SortDirection direccion)
        {
            String direccionOrder = "";

            switch (direccion.ToString())
            {
                case "Ascending":
                    direccionOrder = "asc";
                    break;
                case "Descending":
                    direccionOrder = "desc";
                    break;
                default:
                    direccionOrder = "";
                    break;
            }
            return direccionOrder;
        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            dt = (DataTable)context.Session["data"];
            DataView dataView = new DataView(dt);
            String direccion = (string)(context.Session["direccion"]);
            if (direccion.Equals("asc"))
            {
                ordenar = nombreColumna(e.SortExpression.ToString()) + " " + direccionOrder(SortDirection.Descending);
                context.Session["direccion"] = "desc";
                dataView.Sort = ordenar;
                context.Session["ordenar"] = ordenar;
            }
            else
            {
                ordenar = nombreColumna(e.SortExpression.ToString()) + " " + direccionOrder(SortDirection.Ascending);
                context.Session["direccion"] = "asc";
                dataView.Sort = ordenar;
                context.Session["ordenar"] = ordenar;
            }

            grid.DataSource = dataView;
            grid.DataBind();
            context.Session["data"] = dataView.ToTable();
        }

        protected void btn_siguiente_Click(object sender, EventArgs e)
        {

            int thisWeekNumber = GetIso8601WeekOfYear(DateTime.Today);  
            DateTime firstDayOfWeek = FirstDateOfWeek(DateTime.Now.Year, thisWeekNumber, CultureInfo.CurrentCulture);
            try
            {
               if(txt_busCentro.Text.Trim()=="")
                { 
                    throw new Exception("Debe ingresar información en el campo Centro");
                }
                if (txt_busEan.Text.Trim() == "" && txt_busSku.Text.Trim() == "")
                {
                    throw new Exception("Debe ingresar información en uno de los dos campos EAN o SKU");
                }
                if(txt_busEan.Text.Trim() !="" && txt_busEan.Text.Substring(0,1)== "0")
                    throw new Exception("El código de EAN no puede empezar con 0");
                //System.Threading.Thread.Sleep(5000);
                WS_GERENTES.cl_salida ms = ws.slt_gn_pronostico(txt_busCentro.Text.Trim(), txt_busEan.Text.Trim(), txt_busSku.Text.Trim());
                if (ms.ai_num_error == 0)
                {
                    if (ms.adt_datos.Tables[0].Rows.Count > 0)
                    {
                        lbl_fechaExt.Text = AntiXssEncoder.HtmlEncode(ms.as_fechaExt, false);
                        lbl_fechaPro.Text = AntiXssEncoder.HtmlEncode(ms.as_fechaPro, false); 
                        string[] ls_inv = ms.adt_datos.Tables[0].Rows[0]["column 19"].ToString().Trim().Split('.');
                        string lsi_inventario = "";
                        //decimal ld_inventario = 0;
                        if (ls_inv[0].ToString() == "")
                            lsi_inventario = "0" + ms.adt_datos.Tables[0].Rows[0]["column 7"].ToString().Trim();
                        else
                            lsi_inventario = ms.adt_datos.Tables[0].Rows[0]["column 7"].ToString().Trim();
                        //if(ld_equivale>0)
                        //ld_inventario = Math.Round(decimal.Parse(ms.adt_datos.Tables[0].Rows[0]["column 7"].ToString().Trim()) / ld_equivale,2);
                        pan1.Visible = false;
                        pan2.Visible = true;
                        lbl_ean.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 89"].ToString().Trim(), false);
                        lbl_sku.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 1"].ToString().Trim() + " - " + ms.adt_datos.Tables[0].Rows[0]["column 6"].ToString().Trim(), false);  
                        if (ms.adt_datos.Tables[0].Rows[0]["column 25"].ToString().Trim().Equals("R") || ms.adt_datos.Tables[0].Rows[0]["column 25"].ToString().Trim().Equals("W"))
                            lbl_estadoLogistico.Text = "Activo";
                        else
                        {
                            if (ms.adt_datos.Tables[0].Rows[0]["column 25"].ToString().Trim().Equals("M"))
                            lbl_estadoLogistico.Text = "No Planifica";
                            else
                                lbl_estadoLogistico.Text = "Bloqueado /No Pedir";
                        }
                        lbl_clase.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 59"].ToString().Trim(), false);
                        lbl_uxc.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 16"].ToString().Trim(), false); 
                        lbl_inventario.Text = AntiXssEncoder.HtmlEncode(lsi_inventario + " UN", false); 
                        lbl_transito.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 8"].ToString().Trim() + " UN", false); 
                        decimal ld_equivale = 0;
                        if (ms.adt_datos2.Tables[0].Rows.Count > 0)
                        {
                            lbl_pronostico.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos2.Tables[0].Rows[0]["column 26"].ToString().Trim() + " UN", false);
                            try
                            {
                                decimal val1 = decimal.Parse(ms.adt_datos2.Tables[0].Rows[0]["column 26"].ToString().Trim())/7;
                                ld_equivale = Math.Round((decimal.Parse(lsi_inventario)/val1), 2);
                            }
                            catch(Exception ex)
                            { ld_equivale = 0; }
                            
                            //string[] ls_prono = ms.adt_datos2.Tables[0].Rows[0]["column 26"].ToString().Trim().Split('.');
                            //string lsi_pronos = "";
                            //if (ls_prono[0].ToString() == "")
                            //    lsi_pronos = "0" + ms.adt_datos.Tables[0].Rows[0]["column 26"].ToString().Trim();
                            //else
                            //    lsi_pronos = ms.adt_datos.Tables[0].Rows[0]["column 26"].ToString().Trim();

                            //lbl_pronostico.Text = lsi_pronos.Trim() + " UN";
                        }
                        else
                            lbl_pronostico.Text = "";
                        if (ms.adt_datos3.Tables[0].Rows.Count > 0)
                        {
                            grid.Visible = true;
                            DataView dataView = new DataView(ms.adt_datos3.Tables[0]);
                            grid.ShowHeader = true;
                            grid.DataSource = dataView;
                            grid.DataBind();
                            Session["data"] = dataView.ToTable();
                        }
                        else
                            grid.Visible = false;
                        if (ms.adt_datos4.Tables[0].Rows.Count > 0)
                        {
                            grid_pedPen.Visible = true;
                            DataView dataView = new DataView(ms.adt_datos4.Tables[0]);
                            grid_pedPen.ShowHeader = true;
                            grid_pedPen.DataSource = dataView;
                            grid_pedPen.DataBind();
                            Session["dataGrid2"] = dataView.ToTable();
                            lbl_pedidoPen.Visible = true;
                        }
                        else
                        {
                            grid_pedPen.Visible = false;
                            lbl_pedidoPen.Visible = false;
                        }
                        lbl_ventas.Text = AntiXssEncoder.HtmlEncode(ms.as_valVentas, false); 
                        lbl_cabecera.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 13"].ToString().Trim(), false); 
                        lbl_codMayorista.Text = AntiXssEncoder.HtmlEncode(ms.adt_datos.Tables[0].Rows[0]["column 12"].ToString().Trim() + " UN", false); 
                        lbl_equivale.Text = ld_equivale.ToString() +" Días";
                        lbl_tienda.Text = AntiXssEncoder.HtmlEncode("Tienda: " + ms.adt_datos.Tables[0].Rows[0]["CODIGO_CEN"].ToString() + " - " + ms.adt_datos.Tables[0].Rows[0]["DESCRIPCION_CEN"].ToString(), false); 
                    }
                    else
                        fn_alerta("AZ","No se encontraron registros para su búsqueda");
                }
                else
                {
                    throw new Exception(ms.as_error);
                }
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

        protected void grid_pedPen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt_grid = (DataTable)Session["dataGrid2"];
            grid_pedPen.PageIndex = e.NewPageIndex;
            grid_pedPen.DataSource = dt_grid;
            grid_pedPen.DataBind();
        }

        protected void grid_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
        }

        public void fn_alerta(String tipo_error, String mensaje)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SomestartupScript", "myFunction('" + mensaje + "', '" + tipo_error + "');", true);
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        protected void txt_busEan_TextChanged(object sender, EventArgs e)
        {
            if (txt_busEan.Text.Trim() != "")
            {
                txt_busSku.Text = "";
                txt_busSku.Enabled = false;
            }
            else
                txt_busSku.Enabled = true;
        }

        protected void txt_busSku_TextChanged(object sender, EventArgs e)
        {
            if (txt_busSku.Text.Trim() != "")
            {
                txt_busEan.Text = "";
                txt_busEan.Enabled = false;
            }
            else
                txt_busEan.Enabled = true;
        }
    }
}