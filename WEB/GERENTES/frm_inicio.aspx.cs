using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GERENTES
{
    public partial class frm_inicio : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_usuario.Text = Session["SES_NOMBRE_USR"].ToString();


        }
        

       //public void fn_alerta(Int16 num_error, String mensaje)
       // {
       //     Button btnCerr = new Button();
       //     btnCerr.ID = "btn";
       //     btnCerr.Text = "x";
       //     btnCerr.CssClass = "close";            
            
       //     btnCerr.Click += new EventHandler(btn_ingresar_Click);
       //     //num_error = 1 INFO; num_error = 2 ERROR; num_error = 3 EXITOSO
       //     if (num_error == 2)
       //     {
       //         pn_alerta.Controls.Add(new LiteralControl("<div class='alert alert-info alert-danger'>"));
       //         pn_alerta.Controls.Add(new LiteralControl("<i class='fa fa-info-circle'></i> <strong>" + mensaje + "</strong>"));
       //         pn_alerta.Controls.Add(btnCerr);
       //         pn_alerta.Controls.Add(new LiteralControl("</div>"));
       //     }

       //     else
       //         if (num_error == 3)
       //     {
       //         pn_alerta.Controls.Add(new LiteralControl("<div class='alert alert-success'>"));
       //         pn_alerta.Controls.Add(new LiteralControl("<i class='fa fa-info-circle'></i> <strong>" + mensaje + "</strong>"));
       //         pn_alerta.Controls.Add(btnCerr);
       //         pn_alerta.Controls.Add(new LiteralControl("</div>"));
       //     }
       //     else
       //     {
       //         pn_alerta.Controls.Add(new LiteralControl("<div class='alert alert-info alert-active'>"));
       //         pn_alerta.Controls.Add(new LiteralControl("<i class='fa fa-info-circle'></i> <strong>" + mensaje + "</strong>"));
       //         pn_alerta.Controls.Add(btnCerr);
       //         pn_alerta.Controls.Add(new LiteralControl("</div>"));
       //     }
       // }
    }
}