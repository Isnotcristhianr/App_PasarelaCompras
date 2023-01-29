using App_PasarelaCompras.Modelo;
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
    public partial class AgregarCompra : ContentPage
    {
        public AgregarCompra(Mproductos parametrosTrae)
        {
            InitializeComponent();
            BindingContext = new VistaModelo.VMagregarCompra(Navigation, parametrosTrae);

        }
    }
}