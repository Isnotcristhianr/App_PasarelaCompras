using App_PasarelaCompras.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App_PasarelaCompras.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}