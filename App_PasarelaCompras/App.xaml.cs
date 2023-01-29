using App_PasarelaCompras.Services;
using App_PasarelaCompras.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App_PasarelaCompras.Vistas;
using Plugin.SharedTransitions;

namespace App_PasarelaCompras
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new SharedTransitionNavigationPage(new Compras());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
