using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEMONIO_GERENTE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Int32 LOG = 0;
            WS_GERENTES.WS_GERENTES ws = new WS_GERENTES.WS_GERENTES();
            try
            {
                LOG = ws.int_gn_log("fn_cargaAcrchivo_demonio", DateTime.Now.ToString("yyyyMMdd"));
                try
                {
                    ws = new WS_GERENTES.WS_GERENTES();
                    ws.Timeout = int.MaxValue;
                    ws.fn_cargaAcrchivo_extractor(DateTime.Now.ToString("yyyyMMdd"));
                    ws = new WS_GERENTES.WS_GERENTES();
                    ws.Timeout = int.MaxValue;
                    ws.fn_cargaAcrchivo_proyeccion(DateTime.Now.ToString("yyyyMMdd"));
                    ws.Timeout = int.MaxValue;
                    ws.fn_cargaAcrchivo_ventas(DateTime.Now.ToString("yyyyMMdd"));

                    ws.upd_gn_log(LOG, "");
                }
                catch (Exception ex) { throw new Exception(ex.Message); }

            }
            catch (Exception ex)
            {
                ws.upd_gn_log(LOG, ex.Message);
                string g = "";
            }
            Close();
        }
    }
}
