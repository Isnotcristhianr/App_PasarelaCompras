using System;
using System.Collections.Generic;
using System.Text;
//usar where
using System.Linq;
using System.Threading.Tasks;
//firebase
using App_PasarelaCompras.Conexiones;
//asincronas
using App_PasarelaCompras.Modelo;

namespace App_PasarelaCompras.Datos
{
    public class Dproductos
    {

        public async Task<List<Mproductos>> MostrarProductos()
        {

            return (await Cconexion.firebase
                .Child("Productos")
                .OnceAsync<Mproductos>()).Select(item => new Mproductos
                {
                    Contenido = item.Object.Contenido,
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Precio = item.Object.Precio,
                    Stock = item.Object.Stock,
                    IdProducto = item.Key

                }).ToList();
        }

        public async Task<List<Mproductos>> MostrarProductosId(Mproductos parametros)
        {

            return (await Cconexion.firebase
                .Child("Productos")
                .OnceAsync<Mproductos>()).Where(a => a.Key == parametros.IdProducto).Select(item => new Mproductos
                {
                    Contenido = item.Object.Contenido,
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Precio = item.Object.Precio,
                    Stock = item.Object.Stock,
                    IdProducto = item.Key

                }).ToList();
        }
    }
}
