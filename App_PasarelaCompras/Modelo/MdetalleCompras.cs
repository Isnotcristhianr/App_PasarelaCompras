using System;
using System.Collections.Generic;
using System.Text;

namespace App_Compras.Modelo
{
    public class MdetalleCompras
    {
        public string Cantidad { get; set; }
        public string IdProducto { get; set; }
        public string PrecioCompra { get; set; }
        public string Total { get; set; }
        
        //idVenta
        public string IdDetalleCompra { get; set; }

        //img almacenar
        public string Imagen { get; set; }
        //detalles
        public string Descripcion { get; set; }
        //pagar
       //public string PagarCompra { get; set; }

    }
}
