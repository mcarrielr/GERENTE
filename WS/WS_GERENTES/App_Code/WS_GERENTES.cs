using IntegracionRosado.Metodo;
using IntegracionRosado.WS_PO_CENTROS;
using IntegracionRosado.WS_PO_PEDIDO_PEN_SAP;
using IntegracionRosado.WS_PO_PROMOCION_SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Services;

/// <summary>
/// Descripción breve de WS_GERENTES
/// </summary>
[WebService(Namespace = "http://localhost/WS_GERENTES")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WS_GERENTES : System.Web.Services.WebService
{
    #region cadena_de_conexion

    public static string SettingsSERVIDOR_NAME
    {
        get
        {
            return SETTINGS_WEB("con");

        }
    }

    /// <summary>
    /// Extrae los valores de los Settings asociados al proyecto web
    /// </summary>
    /// <param name="strSettingNombre">Nombre del Setting a Extraer</param>
    /// <returns></returns>
    private static string SETTINGS_WEB(string propiedad)
    {

        try
        {
            String[] servicios = System.Web.HttpContext.Current.Request.ServerVariables["PATH_INFO"].ToString().Split('/');

            System.Configuration.Configuration leerWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(String.Concat("/", servicios[1], "/"));

            if (0 < leerWebConfig.AppSettings.Settings.Count)
            {

                System.Configuration.KeyValueConfigurationElement UserSettings = leerWebConfig.AppSettings.Settings[propiedad];

                if (null != UserSettings)

                    propiedad = UserSettings.Value;

                else

                    propiedad = "";

            }
        }
        catch (Exception e)
        {
            Exception error = new Exception(e.Message + "CODE FILE 104");
            throw (error);
        }
        return propiedad;
    }
    #endregion

    public class cl_salida
    {
        public DataSet adt_datos { get; set; }
        public DataSet adt_datos2 { get; set; }
        public DataSet adt_datos3 { get; set; }
        public DataSet adt_datos4 { get; set; }
        public Int32 ai_id { get; set; }
        public string as_error { get; set; }
        public string as_retorno { get; set; }
        public int ai_num_error { get; set; }
        public string as_num_pda { get; set; }
        public string as_fechaExt { get; set; }
        public string as_fechaPro { get; set; }
        public string as_valVentas { get; set; }
        public cl_salida()
        {
        }
    }

    #region basico

    [WebMethod(Description = "")]
    public cl_salida slt_val_Contrasenia(Int32 CODIGO_USR, String CONTRA)
    {
        cl_salida ms = new cl_salida();
        CONTRA = FormsAuthentication.HashPasswordForStoringInConfigFile(CONTRA, "MD5");
        try
        {
            string sql = "select * FROM SI_USUARIO WHERE CODIGO_USR = @CODIGO_USR  ";
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
            da.Fill(dt);
            if (dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows[0]["CLAVE_USR"].ToString() == CONTRA)
                {
                    ms.ai_num_error = 0;
                }
                else
                    throw new Exception("Esta contraseña es incorrecta.");
            }
            else
                throw new Exception("Usuario no existe.");
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "consulta de LOGS")]
    public cl_salida slt_gn_log(String FECHA_INI, String FECHA_FIN)
    {
        cl_salida ms = new cl_salida();
        try
        {
            string sql = "select * FROM GN_LOG WHERE FECHA_CRE_LOG BETWEEN @FECHA_INI AND @FECHA_FIN ";
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.Add("@FECHA_INI", DateTime.ParseExact(FECHA_INI + " 00:00:00", "yyyy-MM-dd HH:mm:ss", null));
            da.SelectCommand.Parameters.Add("@FECHA_FIN", DateTime.ParseExact(FECHA_FIN + " 23:59:59", "yyyy-MM-dd HH:mm:ss", null));
            da.Fill(dt);
            ms.ai_num_error = 0;
            ms.adt_datos = dt;
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "consulta de SOCIEDADES.")]
    public DataSet slt_gn_sociedad(Int32 CODIGO_SOC)
    {
        string sql = "select * FROM GN_SOCIEDAD WHERE (CODIGO_SOC = @CODIGO_SOC OR @CODIGO_SOC = '')";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_SOC", CODIGO_SOC);
        da.Fill(dt);
        return dt;
    }

    public DataSet slt_cen_tienda_x_usuario(Int32 CODIGO_USR)
    {
        string sql = "select * FROM CEN_TIENDA WHERE CODIGO_TND IN (SELECT CODIGO_TND FROM USR_TND WHERE CODIGO_USR = @CODIGO_USR ) ";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
        da.Fill(dt);
        return dt;
    }

    [WebMethod(Description = "consulta de TIENDAS.")]
    public DataSet slt_cen_tienda_X_sociedad(String CODIGO_CEN, Int32 CODIGO_SOC)
    {
        string sql = "select * FROM CEN_TIENDA a, GN_CENTRO b WHERE a.CODIGO_CEN = b.CODIGO_CEN " +
            " AND (a.CODIGO_CEN = @CODIGO_CEN OR @CODIGO_CEN = '') AND (CODIGO_SOC = @CODIGO_SOC OR @CODIGO_SOC = '')";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
        da.SelectCommand.Parameters.Add("@CODIGO_SOC", CODIGO_SOC);
        da.Fill(dt);
        return dt;
    }


    [WebMethod(Description = "consulta de perfiles de usuario.")]
    public DataSet slt_si_correo()
    {
        string sql = "select * FROM SI_CORREO ORDER BY 2 ASC";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.Fill(dt);
        return dt;
    }

    public String slt_gn_parametro_upd(Int32 CODIGO_PUD)
    {
        string sql = "select * FROM GN_PARAMETRO_UPD WHERE CODIGO_PUD = @CODIGO_PUD";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_PUD", CODIGO_PUD);
        da.Fill(dt);
        return DateTime.Parse(dt.Tables[0].Rows[0]["FECHA_CRE_UPD"].ToString()).ToString("yyyy-MM-dd");
    }

    [WebMethod(Description = "consulta de perfiles de usuario.")]
    public DataSet slt_gn_evento()
    {
        string sql = "select * FROM GN_EVENTO ORDER BY 2 ASC";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.Fill(dt);
        return dt;
    }

    [WebMethod(Description = "consulta de perfiles de usuario.")]
    public DataSet slt_gn_centro(String CODIGO_CEN)
    {
        string sql = "select *, CODIGO_CEN +' - '+ DESCRIPCION_CEN AS COMPLETO FROM GN_CENTRO WHERE (CODIGO_CEN = @CODIGO_CEN OR @CODIGO_CEN = '')";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
        da.Fill(dt);
        return dt;
    }

    [WebMethod(Description = "consulta de perfiles de usuario.")]
    public DataSet slt_si_perfil_usuario()
    {
        string sql = "select * FROM SI_PERFIL_USUARIO ";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.Fill(dt);
        return dt;
    }

    [WebMethod(Description = "Validación de Logueo.")]
    public cl_salida slt_si_usuario(String NOMBRE_USR)
    {
        cl_salida ms = new cl_salida();
        try
        {
            string sql = "select * FROM SI_USUARIO a, SI_PERFIL_USUARIO c where " +
                " a.CODIGO_PUS = c.CODIGO_PUS AND (NOMBRE_USR +' '+ APELLIDO_USR like '%'+ @NOMBRE_USR +'%' OR @NOMBRE_USR = '') " +
                " ";
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.Add("@NOMBRE_USR", NOMBRE_USR);
            da.Fill(dt);
            ms.ai_num_error = 0;
            ms.adt_datos = dt;
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Validación de Logueo.")]
    public cl_salida slt_logeo(String ALIAS_USR, String CLAVE_USR)
    {
        cl_salida ms = new cl_salida();
        CLAVE_USR = FormsAuthentication.HashPasswordForStoringInConfigFile(CLAVE_USR, "MD5");
        try
        {
            string sql = "select * FROM SI_USUARIO a left outer join GN_CENTRO b on a.CODIGO_CEN = b.CODIGO_CEN WHERE  " +
                "  ALIAS_USR = @ALIAS_USR  ";
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.Add("@ALIAS_USR", ALIAS_USR);
            da.Fill(dt);
            if (dt.Tables[0].Rows.Count > 0)
            {
                if (dt.Tables[0].Rows[0]["CLAVE_USR"].ToString() == CLAVE_USR)
                {
                    ms.ai_num_error = 0;
                    ms.adt_datos = dt;
                }
                else
                    throw new Exception("Contraseña inconrrecta.");
            }
            else
                throw new Exception("Este usuario no existe.");
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Consulta de Páginas por perfil de usuario.")]
    public DataSet slt_pagina_x_grupo(Int32 CODIGO_USR)
    {
        string sql = " select grupo_pag, max(orden_pag), ICONO_GRU_PAG AS GRAFICO_PAG " +
       " FROM SI_PAGINA a, SI_MENU_ROL b, SI_ROL_USU c " +
        " WHERE a.CODIGO_PAG = b.CODIGO_PAG AND c.CODIGO_ROL = b.CODIGO_ROL " +
       " AND c.CODIGO_USR = @CODIGO_USR GROUP BY GRUPO_PAG, ICONO_GRU_PAG order by 2 ";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
        da.Fill(dt);
        return dt;
    }
    [WebMethod(Description = "consulta de perfiles de usuario.")]
    public DataSet slt_si_rol()
    {
        string sql = "select * FROM SI_ROL ";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.Fill(dt);
        return dt;
    }
    [WebMethod(Description = "Consulta de Páginas por perfil de usuario.")]
    public DataSet slt_pagina_x_usuario(Int32 CODIGO_USR, String GRUPO_PAG)
    {
        string sql = "select distinct a.* " +
       " FROM SI_PAGINA a, SI_MENU_ROL b, SI_ROL_USU c " +
       " WHERE a.CODIGO_PAG = b.CODIGO_PAG AND c.CODIGO_ROL = b.CODIGO_ROL " +
       " AND c.CODIGO_USR = @CODIGO_USR and GRUPO_PAG = @GRUPO_PAG " +
       " order by ORDEN_PAG asc ";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
        da.SelectCommand.Parameters.Add("@GRUPO_PAG", GRUPO_PAG);
        da.Fill(dt);
        return dt;
    }

    [WebMethod(Description = "")]
    public cl_salida int_usr_tnd(Int32 CODIGO_TND, Int32 CODIGO_USR)
    {
        cl_salida ms = new cl_salida();
        try
        {
            Int32 respSQL = 0;
            String sql = "INSERT INTO USR_TND (CODIGO_TND, CODIGO_USR) VALUES (@CODIGO_TND, @CODIGO_USR)";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_TND", CODIGO_TND);
                cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("NO se logró insertar el rol. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "")]
    public cl_salida upd_si_correo(Int32 CODIGO_COR, String CORREO_COR)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "UPDATE SI_CORREO SET CORREO_COR = @CORREO_COR WHERE CODIGO_COR = @CODIGO_COR ";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CORREO_COR", CORREO_COR);
                cmd.Parameters.Add("@CODIGO_COR", CODIGO_COR);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("NO se logró insertar el rol. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "")]
    public cl_salida int_si_correo(String CORREO_COR)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "INSERT INTO SI_CORREO (CORREO_COR) VALUES (@CORREO_COR)";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CORREO_COR", CORREO_COR);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("NO se logró insertar el rol. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    public cl_salida int_gn_centro(String CODIGO_CEN, String DESCRIPCION_CEN)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            if (slt_gn_centro(CODIGO_CEN).Tables[0].Rows.Count == 0)
            {
                String sql = "INSERT INTO GN_CENTRO (CODIGO_CEN, DESCRIPCION_CEN) VALUES (@CODIGO_CEN, @DESCRIPCION_CEN)";
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@DESCRIPCION_CEN", DESCRIPCION_CEN);
                    cmd.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);

                    con.Open();
                    respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                }
                if (respSQL > 0)
                {
                    ms.as_error = "Registro ingresado con éxito";
                    ms.ai_id = respSQL;
                    ms.ai_num_error = 0;
                }
                else
                    throw new Exception("NO se logró insertar el rol. Notifique al administrador.");
            }

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "")]
    public cl_salida int_gn_evento(String DESCRIPCION_EVE)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "INSERT INTO GN_EVENTO (DESCRIPCION_EVE) VALUES (@DESCRIPCION_EVE)";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DESCRIPCION_EVE", DESCRIPCION_EVE);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("NO se logró insertar el rol. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "")]
    public cl_salida upd_gn_evento(Int32 CODIGO_EVE, String DESCRIPCION_EVE)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "UPDATE GN_EVENTO SET DESCRIPCION_EVE = @DESCRIPCION_EVE WHERE CODIGO_EVE = @CODIGO_EVE";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DESCRIPCION_EVE", DESCRIPCION_EVE);
                cmd.Parameters.Add("@CODIGO_EVE", CODIGO_EVE);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("NO se logró insertar el rol. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "")]
    public cl_salida int_si_rol_usu(Int32 CODIGO_ROL, Int32 CODIGO_USR)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "INSERT INTO SI_ROL_USU (CODIGO_ROL, CODIGO_USR) VALUES (@CODIGO_ROL, @CODIGO_USR)";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("NO se logró insertar el rol. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Perchas")]
    public cl_salida int_si_menu_rol(Int32 CODIGO_ROL, Int32 CODIGO_PAG)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "INSERT INTO SI_MENU_ROL (CODIGO_ROL, CODIGO_PAG) VALUES (@CODIGO_ROL, @CODIGO_PAG)";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                cmd.Parameters.Add("@CODIGO_PAG", CODIGO_PAG);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro ingresado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Insert de Usuarios")]
    public cl_salida int_si_usuario(String NOMBRE_USR, String APELLIDO_USR, String ALIAS_USR, String CLAVE_USR, Int32 CODIGO_PUS,
        String CODIGO_CEN)
    {
        CLAVE_USR = FormsAuthentication.HashPasswordForStoringInConfigFile(CLAVE_USR, "MD5");
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "";
            if (CODIGO_CEN != "")
                sql = "INSERT INTO SI_USUARIO (CODIGO_CEN, NOMBRE_USR, APELLIDO_USR, ALIAS_USR, CLAVE_USR, CODIGO_PUS, RENOV_CLAVE_USR, CAMB_CLAVE_USR, ESTADO_USR) VALUES " +
                 " (@CODIGO_CEN, @NOMBRE_USR, @APELLIDO_USR, @ALIAS_USR, @CLAVE_USR, @CODIGO_PUS, 2, getdate(), 1) Select SCOPE_IDENTITY() as ID;";
            else
                sql = "INSERT INTO SI_USUARIO (NOMBRE_USR, APELLIDO_USR, ALIAS_USR, CLAVE_USR, CODIGO_PUS, RENOV_CLAVE_USR, CAMB_CLAVE_USR, ESTADO_USR) VALUES " +
            " (@NOMBRE_USR, @APELLIDO_USR, @ALIAS_USR, @CLAVE_USR, @CODIGO_PUS, 2, getdate(), 1) Select SCOPE_IDENTITY() as ID;";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
                cmd.Parameters.Add("@NOMBRE_USR", NOMBRE_USR);
                cmd.Parameters.Add("@APELLIDO_USR", APELLIDO_USR);
                cmd.Parameters.Add("@ALIAS_USR", ALIAS_USR);
                cmd.Parameters.Add("@CLAVE_USR", CLAVE_USR);
                cmd.Parameters.Add("@CODIGO_PUS", CODIGO_PUS);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod]
    public Int16 upd_contra(Int32 CODIGO_USR, String CLAVE_USR)
    {
        CLAVE_USR = FormsAuthentication.HashPasswordForStoringInConfigFile(CLAVE_USR, "MD5");
        Int16 respSQL = 0;
        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            String sql = "UPDATE SI_USUARIO SET CLAVE_USR = @CLAVE_USR WHERE CODIGO_USR = @CODIGO_USR";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);
            cmd.Parameters.Add("@CLAVE_USR", CLAVE_USR);
            con.Open();
            respSQL = Convert.ToInt16(cmd.ExecuteNonQuery());
            con.Close();
        }
        return respSQL;
    }

    [WebMethod(Description = "Insert de Usuarios")]
    public cl_salida upd_si_usuario(Int32 CODIGO_USR, String NOMBRE_USR, String APELLIDO_USR, String ALIAS_USR, String CLAVE_USR,
       Int32 CODIGO_PUS, Int16 RENOV_CLAVE_USR, String CODIGO_CEN)
    {
        String filtro = "";
        Int32 respSQL = 0;
        if (RENOV_CLAVE_USR == 1)
        {
            CLAVE_USR = FormsAuthentication.HashPasswordForStoringInConfigFile("1234", "MD5");
            filtro = " , CLAVE_USR = @CLAVE_USR, CAMB_CLAVE_USR = GETDATE() ";
        }
        else
        {
            if (CLAVE_USR != "")
            {
                CLAVE_USR = FormsAuthentication.HashPasswordForStoringInConfigFile(CLAVE_USR, "MD5");
                filtro = " , CLAVE_USR = @CLAVE_USR, CAMB_CLAVE_USR = GETDATE() ";
            }
        }
        cl_salida ms = new cl_salida();
        try
        {
            string sql = "";
            if (CODIGO_CEN != "")
                sql = "UPDATE SI_USUARIO SET CODIGO_CEN = @CODIGO_CEN, NOMBRE_USR = @NOMBRE_USR, APELLIDO_USR = @APELLIDO_USR, CODIGO_PUS = @CODIGO_PUS, " +
                    "RENOV_CLAVE_USR = @RENOV_CLAVE_USR " + filtro + " WHERE CODIGO_USR = @CODIGO_USR ";
            else
                sql = "UPDATE SI_USUARIO SET CODIGO_CEN = null, NOMBRE_USR = @NOMBRE_USR, APELLIDO_USR = @APELLIDO_USR, CODIGO_PUS = @CODIGO_PUS, " +
                "RENOV_CLAVE_USR = @RENOV_CLAVE_USR " + filtro + " WHERE CODIGO_USR = @CODIGO_USR ";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                if (CLAVE_USR != "")
                {
                    cmd.Parameters.Add("@CLAVE_USR", CLAVE_USR);

                }
                cmd.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
                cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);
                cmd.Parameters.Add("@RENOV_CLAVE_USR", RENOV_CLAVE_USR);
                cmd.Parameters.Add("@NOMBRE_USR", NOMBRE_USR);
                cmd.Parameters.Add("@APELLIDO_USR", APELLIDO_USR);
                cmd.Parameters.Add("@ALIAS_USR", ALIAS_USR);
                cmd.Parameters.Add("@CODIGO_PUS", CODIGO_PUS);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod]
    public int dlt_gn_log()
    {
        String sql = "DELETE FROM GN_LOG";

        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        return 1;
    }

    [WebMethod(Description = "")]
    public cl_salida dlt_usr_tnd(Int32 CODIGO_TND, Int32 CODIGO_USR)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "DELETE FROM USR_TND WHERE CODIGO_TND = @CODIGO_TND AND  CODIGO_USR = @CODIGO_USR ";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_TND", CODIGO_TND);
                cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro eliminado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("No se pudo eliminar el registro. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }
    public void dlt_gn_pronostico(SqlConnection conn, SqlTransaction tran)
    {
        String sql = "DELETE FROM GN_PRONOSTICO";
        using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
        {
            cmd.CommandType = CommandType.Text;
            Convert.ToInt32(cmd.ExecuteNonQuery());
        }
    }
    public void dlt_gn_proyeccion()
    {
        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            String sql = "TRUNCATE TABLE GN_PROYECCION";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            Convert.ToInt32(cmd.ExecuteNonQuery());
            con.Close();
        }
    }

    [WebMethod(Description = "")]
    public cl_salida dlt_si_rol_usu(Int32 CODIGO_ROL, Int32 CODIGO_USR)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "DELETE FROM SI_ROL_USU WHERE CODIGO_ROL = @CODIGO_ROL AND  CODIGO_USR = @CODIGO_USR ";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro eliminado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
            else
                throw new Exception("No se pudo eliminar el registro. Notifique al administrador.");

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Perchas")]
    public cl_salida dlt_si_menu_rol(Int32 CODIGO_ROL, Int32 CODIGO_PAG)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "DELETE FROM SI_MENU_ROL WHERE CODIGO_ROL = @CODIGO_ROL AND  CODIGO_PAG = @CODIGO_PAG ";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                cmd.Parameters.Add("@CODIGO_PAG", CODIGO_PAG);

                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.as_error = "Registro eliminado con éxito";
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }

        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "consulta los roles de un usuario")]
    public cl_salida rolUsuario(Int32 CODIGO_USR)
    {
        cl_salida ms = new cl_salida();
        try
        {
            string sql = "SELECT * FROM SI_ROL  where CODIGO_ROL in (select CODIGO_ROL from SI_ROL_USU WHERE CODIGO_USR = @CODIGO_USR) ";
            DataSet dt = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con")))
            {
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
                da.Fill(dt);
                ms.adt_datos = dt;
            }
            sql = "SELECT * FROM SI_ROL  where CODIGO_ROL NOT in (select CODIGO_ROL from SI_ROL_USU WHERE CODIGO_USR = @CODIGO_USR) ";
            dt = new DataSet();
            using (SqlDataAdapter da1 = new SqlDataAdapter(sql, SETTINGS_WEB("con")))
            {
                da1.SelectCommand.CommandType = CommandType.Text;
                da1.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
                da1.Fill(dt);
                ms.adt_datos2 = dt;
            }
            ms.ai_num_error = 0;
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "consulta los CENTROS de un usuario")]
    public cl_salida tiendaUsuario(Int32 CODIGO_USR, String CODIGO_CEN)
    {
        cl_salida ms = new cl_salida();
        try
        {
            string sql = "SELECT * FROM CEN_TIENDA  where CODIGO_CEN = @CODIGO_CEN AND CODIGO_TND in (select CODIGO_TND from USR_TND WHERE CODIGO_USR = @CODIGO_USR) ";
            DataSet dt = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con")))
            {
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
                da.SelectCommand.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
                da.Fill(dt);
                ms.adt_datos = dt;
            }

            sql = "SELECT * FROM CEN_TIENDA  where CODIGO_CEN = @CODIGO_CEN and CODIGO_TND not in (select CODIGO_TND from USR_TND WHERE CODIGO_USR = @CODIGO_USR) ";
            dt = new DataSet();
            using (SqlDataAdapter da1 = new SqlDataAdapter(sql, SETTINGS_WEB("con")))
            {
                da1.SelectCommand.CommandType = CommandType.Text;
                da1.SelectCommand.Parameters.Add("@CODIGO_USR", CODIGO_USR);
                da1.SelectCommand.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
                da1.Fill(dt);
                ms.adt_datos2 = dt;
            }
            ms.ai_num_error = 0;
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "consulta de Nivel por percha.")]
    public cl_salida rolPagina(Int32 CODIGO_ROL)
    {
        cl_salida ms = new cl_salida();
        try
        {
            string sql = "SELECT * FROM SI_PAGINA  where CODIGO_PAG in (select CODIGO_PAG from SI_MENU_ROL WHERE CODIGO_ROL = @CODIGO_ROL) ";
            DataSet dt = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con")))
            {
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                da.Fill(dt);
                ms.adt_datos = dt;
            }

            sql = "SELECT * FROM SI_PAGINA  where CODIGO_PAG NOT IN (select CODIGO_PAG from SI_MENU_ROL WHERE CODIGO_ROL = @CODIGO_ROL) ";
            dt = new DataSet();
            using (SqlDataAdapter da1 = new SqlDataAdapter(sql, SETTINGS_WEB("con")))
            {
                da1.SelectCommand.CommandType = CommandType.Text;
                da1.SelectCommand.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                da1.Fill(dt);
                ms.adt_datos2 = dt;
            }
            ms.ai_num_error = 0;
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Update de Roles")]
    public cl_salida upd_si_rol(Int32 CODIGO_ROL, String DESCRIPCION_ROL)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "UPDATE SI_ROL SET DESCRIPCION_ROL = @DESCRIPCION_ROL WHERE CODIGO_ROL = @CODIGO_ROL";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_ROL", CODIGO_ROL);
                cmd.Parameters.Add("@DESCRIPCION_ROL", DESCRIPCION_ROL);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }
    [WebMethod(Description = "Insert de Roles")]
    public cl_salida int_si_rol(String DESCRIPCION_ROL)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "INSERT INTO SI_ROL (DESCRIPCION_ROL) VALUES (@DESCRIPCION_ROL) Select SCOPE_IDENTITY() as ID;";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DESCRIPCION_ROL", DESCRIPCION_ROL);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }
    [WebMethod(Description = "Insert de Perfiles de Usuario")]
    public cl_salida int_si_perfil_usuario(String DESCRIPCION_PUS)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "INSERT INTO SI_PERFIL_USUARIO (DESCRIPCION_PUS) VALUES (@DESCRIPCION_PUS)";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@DESCRIPCION_PUS", DESCRIPCION_PUS);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Mantenimiento de Centros.")]
    public cl_salida gn_sociedad(Int32 CODIGO_SOC, String DESCRIPCION_SOC, int INSERT)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            if (INSERT == 1)
            {
                String sql = "INSERT INTO GN_SOCIEDAD (CODIGO_SOC, DESCRIPCION_SOC) VALUES (@CODIGO_SOC, @DESCRIPCION_SOC)";
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@CODIGO_SOC", CODIGO_SOC);
                    cmd.Parameters.Add("@DESCRIPCION_SOC", DESCRIPCION_SOC);
                    con.Open();
                    respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                }
                if (respSQL > 0)
                {
                    ms.ai_id = respSQL;
                    ms.ai_num_error = 0;
                    ms.as_error = "Registro creado con éxito.";
                }
                else
                    throw new Exception("No se pudo insertar este registro. Notifique al administrador.");
            }
            else
            {
                String sql = "UPDATE GN_SOCIEDAD SET DESCRIPCION_SOC = @DESCRIPCION_SOC WHERE CODIGO_SOC = @CODIGO_SOC";
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@CODIGO_SOC", CODIGO_SOC);
                    cmd.Parameters.Add("@DESCRIPCION_SOC", DESCRIPCION_SOC);
                    con.Open();
                    respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                }
                if (respSQL > 0)
                {
                    ms.ai_id = respSQL;
                    ms.ai_num_error = 0;
                    ms.as_error = "Registro actualizado con éxito.";
                }
                else
                    throw new Exception("No se pudo actualizar este registro. Notifique al administrador.");
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Mantenimiento de Centros.")]
    public cl_salida gn_centro(Int32 CODIGO_SOC, String CODIGO_CEN, String DESCRIPCION_CEN, int INSERT)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            if (INSERT == 1)
            {
                String sql = "INSERT INTO GN_CENTRO (CODIGO_SOC, CODIGO_CEN, DESCRIPCION_CEN) VALUES (@CODIGO_SOC, @CODIGO_CEN, @DESCRIPCION_CEN)";
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@CODIGO_SOC", CODIGO_SOC);
                    cmd.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
                    cmd.Parameters.Add("@DESCRIPCION_CEN", DESCRIPCION_CEN);
                    con.Open();
                    respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                }
                if (respSQL > 0)
                {
                    ms.ai_id = respSQL;
                    ms.ai_num_error = 0;
                    ms.as_error = "Registro creado con éxito.";
                }
                else
                    throw new Exception("No se pudo insertar este registro. Notifique al administrador.");
            }
            else
            {
                String sql = "UPDATE GN_CENTRO SET DESCRIPCION_CEN = @DESCRIPCION_CEN WHERE CODIGO_CEN = @CODIGO_CEN";
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
                    cmd.Parameters.Add("@DESCRIPCION_CEN", DESCRIPCION_CEN);
                    con.Open();
                    respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                }
                if (respSQL > 0)
                {
                    ms.ai_id = respSQL;
                    ms.ai_num_error = 0;
                    ms.as_error = "Registro actualizado con éxito.";
                }
                else
                    throw new Exception("No se pudo actualizar este registro. Notifique al administrador.");
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    [WebMethod(Description = "Insert de Perfiles de Usuario")]
    public cl_salida upd_si_perfil_usuario(Int32 CODIGO_PUS, String DESCRIPCION_PUS)
    {
        cl_salida ms = new cl_salida();
        Int32 respSQL = 0;
        try
        {
            String sql = "UPDATE SI_PERFIL_USUARIO SET DESCRIPCION_PUS = @DESCRIPCION_PUS WHERE CODIGO_PUS = @CODIGO_PUS";
            using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@CODIGO_PUS", CODIGO_PUS);
                cmd.Parameters.Add("@DESCRIPCION_PUS", DESCRIPCION_PUS);
                con.Open();
                respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());
                con.Close();
            }
            if (respSQL > 0)
            {
                ms.ai_id = respSQL;
                ms.ai_num_error = 0;
            }
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }
    #endregion

    #region sap 

    [WebMethod]
    public DataSet sap_consultaPedPendiente(String CENTRO, String MATERIAL)
    {
        Int32 log = int_gn_log("sap_consultaPedPendiente", "");
        DataSet dts_resp = new DataSet();
        try
        {
            DataTable dt = new DataTable("Carrito");
            dt.Columns.Add("PEDIDO");
            dt.Columns.Add("POSICION");
            dt.Columns.Add("MATERIAL");
            dt.Columns.Add("FECHA");
            dt.Columns.Add("CANTIDAD");

            /*WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_Request request = new WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_Request();

            List<WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem> lista1 = new List<WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem>();
            List<WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem1> lista2 = new List<WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem1>();
            List<WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem2> lista3 = new List<WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem2>();
            WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem item1 = new WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem();*/

            DT_OrdenesPendientes_Request request = new DT_OrdenesPendientes_Request();

            List<DT_OrdenesPendientes_RequestItem> lista1 = new List<DT_OrdenesPendientes_RequestItem>();
            List<DT_OrdenesPendientes_RequestItem1> lista2 = new List<DT_OrdenesPendientes_RequestItem1>();
            List<DT_OrdenesPendientes_RequestItem2> lista3 = new List<DT_OrdenesPendientes_RequestItem2>();
            DT_OrdenesPendientes_RequestItem item1 = new DT_OrdenesPendientes_RequestItem();

            //item1.BEDAT = DateTime.Now.ToString("yyyyMMdd");
            item1.BEDAT = null;
            lista1.Add(item1);

            /////WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem1 item2 = new WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem1();
            DT_OrdenesPendientes_RequestItem1 item2 = new DT_OrdenesPendientes_RequestItem1();
            item2.MATNR = MATERIAL.Trim();
            lista2.Add(item2);

            /////WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem2 item3 = new WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_RequestItem2();
            DT_OrdenesPendientes_RequestItem2 item3 = new DT_OrdenesPendientes_RequestItem2();
            item3.WERKS = CENTRO;
            lista3.Add(item3);

            request.IT_BEDAT = lista1.ToArray();
            request.IT_MATNR = lista2.ToArray();
            request.IT_WERKS = lista3.ToArray();


            /////WS_PEDIDO_PEN_SAP.SI_OrdenesPendientes_OUT_SYNCService ws = new WS_PEDIDO_PEN_SAP.SI_OrdenesPendientes_OUT_SYNCService();

            NetworkCredential credencial = new NetworkCredential(
                   SETTINGS_WEB("usuarioWSSap"),
                   SETTINGS_WEB("claveWSSap"));
            /////WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_Response respuesta = ws.SI_OrdenesPendientes_OUT_SYNC(request);
            DT_OrdenesPendientes_Response respuesta = Referencia.Pedido_Pendiente_SAP_PO(credencial, request);

            /////foreach (WS_PEDIDO_PEN_SAP.DT_OrdenesPendientes_ResponseItem item in respuesta.T_OC_PENDIENTE)
            foreach (DT_OrdenesPendientes_ResponseItem item in respuesta.T_OC_PENDIENTE)
            {
                DataRow dr1 = dt.NewRow();
                dr1["PEDIDO"] = item.EBELN;
                dr1["POSICION"] = item.EBELP.TrimStart('0');
                dr1["MATERIAL"] = item.MATNR.TrimStart('0');
                dr1["FECHA"] = item.EINDT;
                dr1["CANTIDAD"] = item.MENGE;
                dt.Rows.Add(dr1);
            }
            dts_resp.Tables.Add(dt);
            upd_gn_log(log, "");
        }
        catch (Exception ex)
        {
            upd_gn_log(log, ex.Message);
        }
        return dts_resp;
    }

    [WebMethod(Description = "Consulta de Promociones")]
    public DataSet slt_promocion_sap(String CODIGO_PRD)
    {
        string cod_sap = String.Empty;
        DataSet dts_resp = new DataSet();
        DataTable dt = new DataTable("Carrito");
        dt.Columns.Add("PROMOCION");
        dt.Columns.Add("FECHA_INI");
        dt.Columns.Add("FECHA_FIN");
        Int32 log = 0;
        try
        {
            log = int_gn_log("slt_promocion_sap", "Producto: " + CODIGO_PRD);

            #region "Consumo WS"
            /*WS_PROMOCION_SAP.DT_PromocionxArticulo_Request request = new WS_PROMOCION_SAP.DT_PromocionxArticulo_Request();
            List<WS_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM> lista = new List<WS_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM>();
            WS_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM itemH = new WS_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM();
            itemH.item = CODIGO_PRD;
            lista.Add(itemH);
            request.IT_PXITEM = itemH;

            WS_PROMOCION_SAP.SI_PromocionxArticulo_OUT_SYNCService ws = new WS_PROMOCION_SAP.SI_PromocionxArticulo_OUT_SYNCService();
            ws.Credentials = new System.Net.NetworkCredential(
                   SETTINGS_WEB("usuarioWSSapPI"),
                   SETTINGS_WEB("claveWSSapPI"));
            WS_PROMOCION_SAP.DT_PromocionxArticulo_Response respuesta = ws.SI_PromocionxArticulo_OUT_SYNC(request);*/

            /*WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_Request request = new WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_Request();
            List<WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM> lista = new List<WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM>();
            WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM itemH = new WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_RequestIT_PXITEM();
            itemH.item = CODIGO_PRD;
            lista.Add(itemH);
            request.IT_PXITEM = itemH;

            WS_PO_PROMOCION_SAP.SI_PromocionxArticulo_OUT_SYNCService ws = new WS_PO_PROMOCION_SAP.SI_PromocionxArticulo_OUT_SYNCService();
            ws.Credentials = new System.Net.NetworkCredential(
                   SETTINGS_WEB("usuarioWSSap"),
                   SETTINGS_WEB("claveWSSap"));
            WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_Response respuesta = ws.SI_PromocionxArticulo_OUT_SYNC(request);*/
            #endregion

            DT_PromocionxArticulo_Request request = new DT_PromocionxArticulo_Request();
            List<DT_PromocionxArticulo_RequestIT_PXITEM> lista = new List<DT_PromocionxArticulo_RequestIT_PXITEM>();
            DT_PromocionxArticulo_RequestIT_PXITEM itemH = new DT_PromocionxArticulo_RequestIT_PXITEM();
            itemH.item = CODIGO_PRD;
            lista.Add(itemH);
            request.IT_PXITEM = itemH;

            NetworkCredential credencial = new System.Net.NetworkCredential(
                   SETTINGS_WEB("usuarioWSSap"),
                   SETTINGS_WEB("claveWSSap"));
            DT_PromocionxArticulo_Response respuesta = Referencia.Promocion_PO(credencial, request);

            if (respuesta.T_OUTPUT.Length > 0)
            {
                foreach (DT_PromocionxArticulo_ResponseItem resp_item in respuesta.T_OUTPUT)
                /////foreach (WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_ResponseItem resp_item in respuesta.T_OUTPUT)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["PROMOCION"] = resp_item.AKTKT;
                    dr1["FECHA_INI"] = resp_item.PXBDAT.Substring(0, 4) + "-" + resp_item.PXBDAT.Substring(4, 2) + "-" + resp_item.PXBDAT.Substring(6, 2);
                    dr1["FECHA_FIN"] = resp_item.PXEDAT.Substring(0, 4) + "-" + resp_item.PXEDAT.Substring(4, 2) + "-" + resp_item.PXEDAT.Substring(6, 2);
                    dt.Rows.Add(dr1);
                }
            }
            upd_gn_log(log, "");
        }
        catch (Exception ex)
        {
            upd_gn_log(log, ex.Message);
        }
        dts_resp.Tables.Add(dt);
        return dts_resp;
    }

    #endregion

    #region select
    [WebMethod(Description = "Consulta de Centros")]
    public DataTable slt_centros_sap()
    {
        string cod_sap = String.Empty;
        DataTable dt = new DataTable("Carrito");
        dt.Columns.Add("COD_CENTRO");
        dt.Columns.Add("DESCRIPCION");
        /*WS_CENTROS.DT_Centro_Request request = new WS_CENTROS.DT_Centro_Request();
        request.item = "";

        WS_CENTROS.SI_Centro_OUT_SYNCService ws = new WS_CENTROS.SI_Centro_OUT_SYNCService();
        ws.Credentials = new System.Net.NetworkCredential(
               SETTINGS_WEB("usuarioWSSap"),
               SETTINGS_WEB("claveWSSap"));
        WS_CENTROS.DT_Centro_Response respuesta = ws.SI_Centro_OUT_SYNC(request);*/

        /*WS_PO_CENTROS.DT_Centro_Request request = new WS_PO_CENTROS.DT_Centro_Request();
        request.item = "";

        WS_PO_CENTROS.SI_Centro_OUT_SYNCService ws = new WS_PO_CENTROS.SI_Centro_OUT_SYNCService();
        ws.Credentials = new System.Net.NetworkCredential(
               SETTINGS_WEB("usuarioWSSap"),
               SETTINGS_WEB("claveWSSap"));
        WS_PO_CENTROS.DT_Centro_Response respuesta = ws.SI_Centro_OUT_SYNC(request);*/

        NetworkCredential credencial = new NetworkCredential(
             SETTINGS_WEB("usuarioWSSap"),
               SETTINGS_WEB("claveWSSap"));
        DT_Centro_Request request = new DT_Centro_Request();
        request.item = "";

        DT_Centro_Response respuesta = Referencia.Centro_PO(credencial, request);

        if (respuesta.CODERROR == "")
        {
            /////foreach (WS_CENTROS.DT_Centro_ResponseItem item in respuesta.item)
            foreach (DT_Centro_ResponseItem item in respuesta.item)
            {
                int_gn_centro(item.WERKS, item.NAME);
                DataRow dr1 = dt.NewRow();
                dr1["COD_CENTRO"] = item.WERKS;
                dr1["DESCRIPCION"] = item.NAME;
                dt.Rows.Add(dr1);
            }
        }
        return dt;
    }

    [WebMethod]
    public int slt_validaUsuario(String ALIAS_USR)
    {
        DataSet dt_ = new DataSet();
        string sql = "select * FROM SI_USUARIO WHERE ALIAS_USR = @ALIAS_USR";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@ALIAS_USR", ALIAS_USR);
        da.Fill(dt);
        return dt.Tables[0].Rows.Count;
    }
    public string slt_gn_ventas(String MATERIAL, String CENTRO)
    {
        DataSet dt_ = new DataSet();
        string sql = "select * FROM GN_VENTAS WHERE MATERIAL = @MATERIAL AND CENTRO= @CENTRO";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@MATERIAL", MATERIAL);
        da.SelectCommand.Parameters.Add("@CENTRO", CENTRO);
        da.Fill(dt);
        if (dt.Tables[0].Rows.Count > 0)
            return dt.Tables[0].Rows[0]["VALOR"].ToString();
        else
            return "";
    }

    public DataSet slt_gn_proyeccion(String CAMPO15, String CAMPO16, String CAMPO18)
    {
        DataSet dt_ = new DataSet();
        string sql = "select * FROM GN_PROYECCION WHERE [column 15] = @CAMPO15 AND [column 16] = @CAMPO16 AND [column 18] = @CAMPO18";
        DataSet dt = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
        da.SelectCommand.CommandType = CommandType.Text;
        da.SelectCommand.Parameters.Add("@CAMPO15", CAMPO15);
        da.SelectCommand.Parameters.Add("@CAMPO16", CAMPO16);
        da.SelectCommand.Parameters.Add("@CAMPO18", CAMPO18);
        da.Fill(dt);
        return dt;
    }

    [WebMethod(Description = "consulta de Pronósticos")]
    public cl_salida slt_gn_pronostico(String CODIGO_CEN, String CAMPO89, String CAMPO1)
    {
        cl_salida ms = new cl_salida();
        try
        {
            if (CAMPO89 != "")
            {
                CAMPO89 = CAMPO89 + DigitoVerificadorEAN13(CAMPO89).ToString();
            }
            string sql = "select * FROM GN_PRONOSTICO a, GN_CENTRO b " +
                " WHERE a.[column 0] = b.CODIGO_CEN AND (a.[column 0] = @CODIGO_CEN OR @CODIGO_CEN = '') " +
                " AND (a.[column 89] = @CAMPO89 OR @CAMPO89 = '') AND (a.[column 1] = @CAMPO1 OR @CAMPO1 = '')";
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, SETTINGS_WEB("con"));
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.Add("@CODIGO_CEN", CODIGO_CEN);
            da.SelectCommand.Parameters.Add("@CAMPO89", CAMPO89);
            da.SelectCommand.Parameters.Add("@CAMPO1", CAMPO1);
            da.Fill(dt);
            if (dt.Tables[0].Rows.Count > 0)
            {
                ms.adt_datos2 = slt_gn_proyeccion(dt.Tables[0].Rows[0]["column 1"].ToString().Trim(), CODIGO_CEN, "SLS");
                ms.adt_datos3 = slt_promocion_sap(dt.Tables[0].Rows[0]["column 1"].ToString());
                ms.adt_datos4 = sap_consultaPedPendiente(CODIGO_CEN, dt.Tables[0].Rows[0]["column 1"].ToString());
                ms.as_valVentas = slt_gn_ventas(dt.Tables[0].Rows[0]["column 1"].ToString(), CODIGO_CEN);
                ms.as_fechaExt = slt_gn_parametro_upd(1);
                ms.as_fechaPro = slt_gn_parametro_upd(2);
            }
            ms.ai_num_error = 0;
            ms.adt_datos = dt;
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message.Replace("'", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        return ms;
    }

    #endregion

    #region insert

    [WebMethod]
    public Int32 int_gn_log(String METODO_LOG, String PARAMETROS_LOG)
    {
        String sql = "INSERT INTO GN_LOG (METODO_LOG, FECHA_CRE_LOG, PARAMETROS_LOG)" +
            " values (@METODO_LOG, getdate(), @PARAMETROS_LOG) Select SCOPE_IDENTITY() as ID;";
        Int32 respSQL = 0;
        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@METODO_LOG", METODO_LOG);
            cmd.Parameters.Add("@PARAMETROS_LOG", PARAMETROS_LOG);
            con.Open();
            respSQL = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
        }
        return respSQL;
    }

    [WebMethod]
    public Int32 upd_gn_log(Int32 CODIGO_LOG, String ERROR_LOG)
    {
        String sql = "UPDATE GN_LOG SET ERROR_LOG = @ERROR_LOG, FECHA_FIN_LOG = GETDATE() WHERE CODIGO_LOG = @CODIGO_LOG";
        Int32 respSQL = 0;
        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@CODIGO_LOG", CODIGO_LOG);
            cmd.Parameters.Add("@ERROR_LOG", ERROR_LOG);
            con.Open();
            respSQL = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
        }
        return respSQL;
    }

    #endregion

    #region delete 
    [WebMethod]
    public Int32 dlt_si_usuario(Int32 CODIGO_USR)
    {
        Int32 respSQL = 0;
        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            con.Open();

            String sql = "DELETE FROM SI_ROL_USU WHERE CODIGO_USR = @CODIGO_USR";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@CODIGO_USR", CODIGO_USR);
            respSQL = Convert.ToInt32(cmd.ExecuteNonQuery());

            sql = "DELETE FROM SI_USUARIO WHERE CODIGO_USR = @CODIGO_USR";
            SqlCommand cmd1 = new SqlCommand(sql, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.Add("@CODIGO_USR", CODIGO_USR);

            respSQL = Convert.ToInt32(cmd1.ExecuteNonQuery());
            con.Close();
        }
        return respSQL;
    }
    #endregion 

    #region update

    [WebMethod]
    public Int32 upd_gn_parametro_upd(Int32 CODIGO_PUD)
    {
        String sql = "UPDATE GN_PARAMETRO_UPD SET FECHA_CRE_UPD = GETDATE() WHERE CODIGO_PUD = @CODIGO_PUD";
        Int32 respSQL = 0;
        using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@CODIGO_PUD", CODIGO_PUD);
            con.Open();
            respSQL = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
        }
        return respSQL;
    }


    #endregion 

    #region FTP
    [WebMethod]
    public cl_salida fn_cargaArchivo()
    {
        cl_salida ms = new cl_salida();
        try
        {
            DescargarRepositorio descargar = new DescargarRepositorio();
            List<string> lista = new List<string>();
            descargar.Clave_par = SETTINGS_WEB("FTPclave");
            descargar.Usuario_par = SETTINGS_WEB("FTPusuario");
            descargar.Direccion_par = SETTINGS_WEB("FTPpath");
            descargar.Direccion_path_local = SETTINGS_WEB("FTPpathLocal");
            Console.WriteLine("Leyendo archivos");
            lista = descargar.ListDirectory();
            Console.WriteLine("descargando");
            if (lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    descargar.descargarFic(lista[i]);

                }
                ms.ai_num_error = 0;
                ms.as_error = "Proceso realizado con éxito";
            }
            else
                throw new Exception("No se encontaron registros en el ftp");
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message;
        }
        return ms;
    }
    [WebMethod]
    public cl_salida fn_cargaAcrchivo_extractor(String FECHA)
    {
        cl_salida ms = new cl_salida();
        Int32 log = 0;
        string user = SETTINGS_WEB("FTPusuario");
        string pass = SETTINGS_WEB("FTPclave");
        try
        {
            log = int_gn_log("fn_cargaAcrchivo_extractor", FECHA);
            DirectoryInfo directory = new DirectoryInfo(@"" + SETTINGS_WEB("FTPpathLocal"));
            FileInfo[] files = directory.GetFiles("" + SETTINGS_WEB("prefijoExtractor") + FECHA + ".csv*").OrderByDescending(p => p.CreationTime).ToArray();
            DirectoryInfo[] directories = directory.GetDirectories();

            if (files.Length > 0)
            {
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("pcd_int_extractor", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = Int32.Parse(SETTINGS_WEB("timeout"));
                    cmd.Parameters.Add("@ARG_RUTA_ARCHIVO", @"" + SETTINGS_WEB("FTPdisk") + files[0].Name);
                    cmd.Parameters.Add("@ARG_FORMART_BULK", SETTINGS_WEB("PathServerSQL") + "formatoCSV.fmt");
                    cmd.ExecuteNonQuery();
                }
                ms.ai_num_error = 0;
                ms.as_error = "Archivo generado con éxito.";
                upd_gn_parametro_upd(1);
                upd_gn_log(log, "");
            }
            else
            {
                ms.ai_num_error = 0;
                ms.as_error = "No se encontró ningún archivo para procesar.";
                upd_gn_log(log, "No se encontró ningún archivo para procesar.");
            }
        }
        catch (Exception ex)
        {
            upd_gn_log(log, ex.Message);
            ms.ai_num_error = 1;
            ms.as_error = ex.Message;
        }
        return ms;
    }

    [WebMethod]
    public cl_salida fn_cargaAcrchivo_proyeccion(String FECHA)
    {
        cl_salida ms = new cl_salida();
        Int32 log = 0;
        string user = SETTINGS_WEB("FTPusuario");
        string pass = SETTINGS_WEB("FTPclave");
        try
        {
            log = int_gn_log("fn_cargaAcrchivo_proyeccion", FECHA);
            DirectoryInfo directory = new DirectoryInfo(@"" + SETTINGS_WEB("FTPpathLocal"));
            FileInfo[] files = directory.GetFiles("" + SETTINGS_WEB("prefijoProyeccion") + FECHA + "*.csv*").OrderByDescending(p => p.CreationTime).ToArray();
            DirectoryInfo[] directories = directory.GetDirectories();
            if (files.Length > 0)
            {
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    dlt_gn_proyeccion();
                    con.Open();
                    for (int i = 0; i < files.Length; i++)
                    {
                        SqlCommand cmd = new SqlCommand("pcd_int_proyeccion", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = Int32.Parse(SETTINGS_WEB("timeout"));
                        cmd.Parameters.Add("@ARG_RUTA_ARCHIVO", @"" + SETTINGS_WEB("FTPdisk") + files[0].Name);
                        cmd.Parameters.Add("@ARG_FORMART_BULK", SETTINGS_WEB("PathServerSQL") + "formatoCSV2.fmt");
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                upd_gn_parametro_upd(2);
                upd_gn_log(log, "");
                ms.ai_num_error = 0;
                ms.as_error = "Archivo procesado con éxito.";
            }
            else
            {
                upd_gn_log(log, "No se encontró ningún archivo para procesar.");
                ms.ai_num_error = 0;
                ms.as_error = "No se encontró ningún archivo para procesar.";
            }
        }
        catch (Exception ex)
        {
            upd_gn_log(log, ex.Message);
            ms.ai_num_error = 1;
            ms.as_error = ex.Message;
        }
        return ms;
    }

    [WebMethod]
    public cl_salida fn_cargaAcrchivo_ventas(String FECHA)
    {
        cl_salida ms = new cl_salida();
        Int32 log = 0;
        string user = SETTINGS_WEB("FTPusuario");
        string pass = SETTINGS_WEB("FTPclave");
        try
        {
            log = int_gn_log("fn_cargaAcrchivo_ventas", FECHA);
            DirectoryInfo directory = new DirectoryInfo(@"" + SETTINGS_WEB("FTPpathLocal"));
            FileInfo[] files = directory.GetFiles("" + SETTINGS_WEB("prefijoVentas") + FECHA + ".csv*").OrderByDescending(p => p.CreationTime).ToArray();
            DirectoryInfo[] directories = directory.GetDirectories();

            if (files.Length > 0)
            {
                using (SqlConnection con = new SqlConnection(SETTINGS_WEB("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("pcd_int_ventas", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = Int32.Parse(SETTINGS_WEB("timeout"));
                    //cmd.Parameters.Add("@ARG_RUTA_ARCHIVO", files[0].FullName);
                    cmd.Parameters.Add("@ARG_RUTA_ARCHIVO", @"" + SETTINGS_WEB("FTPdisk") + files[0].Name);
                    cmd.Parameters.Add("@ARG_FORMART_BULK", SETTINGS_WEB("PathServerSQL") + "formatoCSV_Ventas.fmt");
                    cmd.ExecuteNonQuery();
                }

                ms.ai_num_error = 0;
                ms.as_error = "Archivo generado con éxito.";
                upd_gn_parametro_upd(3);
                upd_gn_log(log, "");
            }
            else
            {
                ms.ai_num_error = 0;
                ms.as_error = "No se encontró ningún archivo para procesar.";
                upd_gn_log(log, "No se encontró ningún archivo para procesar.");
            }
        }
        catch (Exception ex)
        {
            upd_gn_log(log, ex.Message);
            ms.ai_num_error = 1;
            ms.as_error = ex.Message;
        }
        return ms;
    }

    [WebMethod]
    public cl_salida fn_ejectaDemonio(string USUARIO)
    {
        cl_salida ms = new cl_salida();
        Int32 log = 0;
        try
        {
            log = int_gn_log("Ejecución Manual Demonio", "Usuario: " + USUARIO);
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = SETTINGS_WEB("dir_bat1");
            proc.StartInfo.WorkingDirectory = SETTINGS_WEB("dir_bat2");
            proc.Start();
            upd_gn_log(log, "");
            ms.ai_num_error = 0;
            ms.as_error = "Ejecutado con éxito";
        }
        catch (Exception ex)
        {
            ms.ai_num_error = 1;
            ms.as_error = ex.Message;
            upd_gn_log(log, ex.Message);
        }
        return ms;
    }

    [WebMethod]
    public int DigitoVerificadorEAN13(string codigoEAN)
    {
        char[] digitos = codigoEAN.ToArray();
        int sum = 0;
        int digitoVerificador = 0;
        for (int i = 1; i <= digitos.Length; i++)
        {
            if (i % 2 != 0)
            {
                sum += Convert.ToInt32(digitos[i - 1].ToString());
            }
            else
            {
                sum += (Convert.ToInt32(digitos[i - 1].ToString()) * 3);
            }

        }
        string value = Convert.ToString(sum);
        char ultimoDigito = value.ToArray()[value.Length - 1];
        char antePenultimoDigito = value.ToArray()[value.Length - 2];
        if (!ultimoDigito.ToString().Equals("0"))
        {
            int siguienteValor = Convert.ToInt32(antePenultimoDigito.ToString()) + 1;
            value = value.Substring(0, value.Length - 2) + siguienteValor + "0";
            digitoVerificador = Convert.ToInt32(value) - sum;
        }
        return digitoVerificador;
    }
    #endregion
}
