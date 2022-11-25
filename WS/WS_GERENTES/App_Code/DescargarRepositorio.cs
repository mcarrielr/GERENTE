using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de DescargarRepositorio
/// </summary>
public class DescargarRepositorio
{
    #region Atributos
    private String direccion_path_local;
    private String direccion_par;
    private String usuario_par;
    private String clave_par;
    private String puerto_par;
    private String descripcion_par;
    public String Direccion_path_local
    {
        get { return direccion_path_local; }
        set { direccion_path_local = value; }
    }

    public String Direccion_par
    {
        get { return direccion_par; }
        set { direccion_par = value; }
    }

    public String Usuario_par
    {
        get { return usuario_par; }
        set { usuario_par = value; }
    }

    public String Clave_par
    {
        get { return clave_par; }
        set { clave_par = value; }
    }

    public String Puerto_par
    {
        get { return puerto_par; }
        set { puerto_par = value; }
    }

    public String Descripcion_par
    {
        get { return descripcion_par; }
        set { descripcion_par = value; }
    }
    #endregion

    public DescargarRepositorio()
    {

    }

    public Boolean descargarFic(String nombre)
    {
        try
        {
            FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(Direccion_par + nombre));
            NetworkCredential cr = new NetworkCredential(Usuario_par, Clave_par);
            dirFtp.Credentials = cr;

            // El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
            dirFtp.Method = WebRequestMethods.Ftp.DownloadFile;

            // Obtener el resultado del comando
            using (StreamReader reader =
                new StreamReader(dirFtp.GetResponse().GetResponseStream()))
            {

                // Leer el stream
                string res = reader.ReadToEnd();

                // Mostrarlo.
                //Console.WriteLine(res);

                // Guardarlo localmente
                string ficLocal = Path.Combine(Direccion_path_local, Path.GetFileName(Direccion_par + nombre));
                using (StreamWriter sw = new StreamWriter(ficLocal, false, Encoding.UTF8))
                {
                    sw.Write(res);
                    sw.Close();
                }
                // Cerrar el stream abierto.
                reader.Close();
            }
            fn_borrar_archivoDeSitioFtp(Clave_par, direccion_par, nombre, Usuario_par);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("error descargarFic {0}, reader abierto", e.Message);
            return false;
        }
    }

    // -============================================== MÉTODO QUE SE ENCARGA DE ELIMINAR UN ARCHIVO .XML DEL REPOSITORIO

    public Boolean fn_borrar_archivoDeSitioFtp(string vstr_clave, string vstr_direccionDeArchivo, string vstr_nombreDeArchivo, string vstr_usuario)
    {
        try
        {
            FtpWebRequest vftp_request = (FtpWebRequest)WebRequest.Create(vstr_direccionDeArchivo + vstr_nombreDeArchivo);
            vftp_request.Credentials = new NetworkCredential(vstr_usuario, vstr_clave);
            vftp_request.Method = WebRequestMethods.Ftp.DeleteFile;

            using (FtpWebResponse vftp_response = (FtpWebResponse)vftp_request.GetResponse())
            {

            }
                return true;
        }
        catch (Exception ex)
        {
            string f = ex.Message;
            return false;
        }
    }

    // -- == Método para eliminar documento.
    public void metEliminarDocumento(string ls_doc)
    {
        if (System.IO.File.Exists(ls_doc))
        {
            try
            {
                System.IO.File.Delete(ls_doc);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }

    // -==============================================

    public List<string> ListDirectory()
    {

        List<string> lista = new List<string>();
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Direccion_par);
        NetworkCredential cr = new NetworkCredential(Usuario_par, Clave_par);
        request.Credentials = cr;
        request.Method = WebRequestMethods.Ftp.ListDirectory;

        try
        {
            using (StreamReader reader = new StreamReader(((FtpWebResponse)request.GetResponse()).GetResponseStream()))
            {
                string ln = "";
                do
                {
                    ln = reader.ReadLine();
                    switch (ln)
                    {
                        case ".":
                            break;
                        case "..":
                            break;
                        case null:
                            break;
                        default:
                            // if (ln.Contains(".xml") || ln.Contains(".XML"))
                            if (ln.Contains(".csv"))
                                lista.Add(ln);
                            //if(ln.Contains("/"))
                            //    lista.Add(ln.Split('/')[1]);
                            break;
                    }
                } while (ln != null);

                reader.Close();
            }
            return lista;
        }
        catch (Exception)
        {
            return lista;
        }
    }

}
