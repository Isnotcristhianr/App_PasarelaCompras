using System;
using System.Collections.Generic;
using System.Text;
//firebase
using Firebase.Database;
using Firebase.Database.Query;
//extra
using System.Linq;
using System.Threading.Tasks;
//directorios
using App_PasarelaCompras.Modelo;
using App_PasarelaCompras.Conexiones;
using App_Compras.Modelo;

namespace App_PasarelaCompras.Datos
{
    class DdetalleCompra
    {
        public async Task InsertarDetCompra(MdetalleCompras parametros)
        {
            await Cconexion.firebase.Child("DetalleCompra").PostAsync(new MdetalleCompras()
            {
                Cantidad = parametros.Cantidad,
                IdProducto = parametros.IdProducto,
                PrecioCompra = parametros.PrecioCompra,
                Total = parametros.Total,
            });
        }

        public async Task<List<MdetalleCompras>> PreviaDetalleCompra()
        {
          
            //almacenar ids
            var ListaDetalleCompras = new List<MdetalleCompras>();
            var funcionProductos = new Dproductos();
            var paramProductos = new Mproductos();

            //lista de datos
            var data = (await Cconexion.firebase.Child("DetalleCompra").OnceAsync<MdetalleCompras>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new MdetalleCompras()
                {
                    IdProducto = item.Object.IdProducto,
                    IdDetalleCompra = item.Key,
                    Cantidad = item.Object.Cantidad,
                    Total = item.Object.Total
                });

            //recorrer ids de img
            foreach (var tmp in data)
            {

                var parametros = new MdetalleCompras();
                parametros.IdProducto = tmp.IdProducto;
                paramProductos.IdProducto = tmp.IdProducto;
                var listaProductos = await funcionProductos.MostrarProductosId(paramProductos);

                parametros.Imagen = listaProductos[0].Icono;
                parametros.Cantidad = tmp.Cantidad;
                parametros.Total = tmp.Total;
                parametros.Descripcion = listaProductos[0].Descripcion;

                //parametros.PagarCompra = ((Int32.Parse(tmp.Cantidad))*(Int32.Parse(tmp.Total))).ToString();

                ListaDetalleCompras.Add(parametros);


            }

            return ListaDetalleCompras;

        }

        public async Task<List<MdetalleCompras>> PreviaPagar()
        {

            //almacenar ids
            var ListaDetalleCompras = new List<MdetalleCompras>();
            var funcionProductos = new Dproductos();
            var paramProductos = new Mproductos();


            //lista de datos
            var data = (await Cconexion.firebase.Child("DetalleCompra").OnceAsync<MdetalleCompras>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new MdetalleCompras()
                {
                    IdProducto = item.Object.IdProducto,
                    Cantidad = item.Object.Cantidad,
                    Total = item.Object.Total
                });

            //recorrer ids de img
            foreach (var tmp in data)
            {

                var parametros = new MdetalleCompras();
                parametros.IdProducto = tmp.IdProducto;
                paramProductos.IdProducto = tmp.IdProducto;
                var listaProductos = await funcionProductos.MostrarProductosId(paramProductos);

                parametros.Cantidad = tmp.Cantidad;
                parametros.Total = tmp.Total;
                parametros.Descripcion = listaProductos[0].Descripcion;

                ListaDetalleCompras.Add(parametros);

            }

            return ListaDetalleCompras;

        }

        //validador por id
        public async Task<List<MdetalleCompras>> MostrarCantidades() {
            return (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a => a.Key != "Modelo").Select(item => new MdetalleCompras
                {
                    Cantidad = item.Object.Cantidad
                }).ToList();
        }
        public async Task<List<MdetalleCompras>> MostrarDetallexIdProd(String idProd)
        {
            return (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a => a.Object.IdProducto == idProd).Select(item => new MdetalleCompras
                {
                    Cantidad = item.Object.Cantidad
                }).ToList();
        }
        //editar 
        public async Task EditarDetalle(MdetalleCompras parametros)
        {
            var data = (await Cconexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<MdetalleCompras>())
                .Where(a => a.Object.IdProducto == parametros.IdProducto)
                .FirstOrDefault();

            double cantInicial = Convert.ToDouble(data.Object.Cantidad);
            data.Object.Cantidad = (cantInicial + Convert.ToDouble(parametros.Cantidad)).ToString();
            
            double cantidad = Convert.ToDouble(data.Object.Cantidad);
            double precioCompra = Convert.ToDouble(parametros.PrecioCompra);
            double total = 0;

            total = cantidad * precioCompra;

            data.Object.Total = total.ToString();

            await Cconexion.firebase
                .Child("DetalleCompra")
                .Child(data.Key)
                .PutAsync(data.Object);
        }

        //suma total
        public async Task<String> SumarCantitad() {
            var funcion = new DdetalleCompra();
            var lista = await funcion.MostrarCantidades();

            double cantidad = 0;

            foreach (var tmp in lista) {
                cantidad += Convert.ToDouble(tmp.Cantidad);
            }

            return cantidad.ToString();
        }
    }
}
