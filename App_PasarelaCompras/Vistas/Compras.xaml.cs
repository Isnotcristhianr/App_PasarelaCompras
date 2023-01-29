using App_PasarelaCompras.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_PasarelaCompras.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Compras : ContentPage
    {

        VMcompras vm;

        public Compras()
        {
            InitializeComponent();
            vm = new App_PasarelaCompras.VistaModelo.VMcompras(Navigation, ladoDerecha, ladoIzquierda);
            BindingContext = vm;

            // detalle previa compra, siempre ejecuta sin pedir
            this.Appearing += Compras_Appearing;
        }

        private async void Compras_Appearing(object sender, EventArgs e)
        {
            await vm.PreviaDetalleCompra();
            await vm.PreviaDetalleCompra();
            await vm.SumarCantidades();
        }

        private async void DeslizarPnlContador(object sender, SwipedEventArgs e)
        {
            await vm.MostrarPnlDetalleCompras(gridProductos, panelDetalleCompra, PanelContador);
        }

        private async void DeslizarPnlDetalleCompra (object sender, SwipedEventArgs e)
        {
            await vm.MostrarGridProductos(gridProductos, panelDetalleCompra, PanelContador);

        }
    }
}