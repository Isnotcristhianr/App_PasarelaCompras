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
    public partial class compraEfectuada : ContentPage
    {
        public compraEfectuada()
        {
            InitializeComponent();
        }

        private void BtnComprar(object sender, EventArgs e)
        {
            //enviar login
            Navigation.PushAsync(new Compras());
        }
    }
}