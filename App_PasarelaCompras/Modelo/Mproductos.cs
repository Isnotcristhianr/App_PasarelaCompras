using System;
using System.Collections.Generic;
using System.Text;

namespace App_PasarelaCompras.Modelo
{
    public class Mproductos
    {
        public string Contenido { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Precio { get; set; }

        //idAdicional
        public string IdProducto { get; set; }
        //stock
        public string Stock { get; set; }
    }
}
