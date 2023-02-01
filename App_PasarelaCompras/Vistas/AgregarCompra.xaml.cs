using App_PasarelaCompras.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//vm
using App_PasarelaCompras.VistaModelo;

namespace App_PasarelaCompras.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarCompra : ContentPage
    {

        VMagregarCompra vm;
        public AgregarCompra(Mproductos parametrosTrae)
        {
            InitializeComponent();
            vm = new VistaModelo.VMagregarCompra(Navigation, parametrosTrae);
            BindingContext = vm;

            //ejecutar sin preguntar
            this.Appearing += AgregarCompra_Appearing;
        }

        private async void AgregarCompra_Appearing(object sender, EventArgs e) {
          //  await vm.PreviaStock();
        }
        
    }
}