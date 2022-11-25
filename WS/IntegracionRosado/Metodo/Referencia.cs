using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace IntegracionRosado.Metodo
{
    public class Referencia
    {
        public static WS_PO_CENTROS.DT_Centro_Response Centro_PO(NetworkCredential credencial, WS_PO_CENTROS.DT_Centro_Request request)
        {
            WS_PO_CENTROS.DT_Centro_Response respuesta;

            WS_PO_CENTROS.SI_Centro_OUT_SYNCService ws = new WS_PO_CENTROS.SI_Centro_OUT_SYNCService();
            ws.Credentials = credencial;
            respuesta = ws.SI_Centro_OUT_SYNC(request);

            return respuesta;
        }

        public static WS_PO_PEDIDO_PEN_SAP.DT_OrdenesPendientes_Response Pedido_Pendiente_SAP_PO(NetworkCredential credencial, WS_PO_PEDIDO_PEN_SAP.DT_OrdenesPendientes_Request request)
        {
            WS_PO_PEDIDO_PEN_SAP.DT_OrdenesPendientes_Response respuesta;

            WS_PO_PEDIDO_PEN_SAP.SI_OrdenesPendientes_OUT_SYNCService ws = new WS_PO_PEDIDO_PEN_SAP.SI_OrdenesPendientes_OUT_SYNCService();
            ws.Credentials = credencial;
            respuesta = ws.SI_OrdenesPendientes_OUT_SYNC(request);

            return respuesta;
        }

        public static WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_Response Promocion_PO(NetworkCredential credencial, WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_Request request)
        {
            WS_PO_PROMOCION_SAP.DT_PromocionxArticulo_Response respuesta;

            WS_PO_PROMOCION_SAP.SI_PromocionxArticulo_OUT_SYNCService ws = new WS_PO_PROMOCION_SAP.SI_PromocionxArticulo_OUT_SYNCService();
            ws.Credentials = credencial;
            respuesta = ws.SI_PromocionxArticulo_OUT_SYNC(request);

            return respuesta;
        }






    }
}