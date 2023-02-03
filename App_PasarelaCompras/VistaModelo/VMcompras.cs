using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//ext
using App_PasarelaCompras.Modelo;
using App_PasarelaCompras.Datos;
using System.Windows.Input;
using App_PasarelaCompras.Vistas;
using App_Compras.Modelo;
//efecto
using Plugin.SharedTransitions;
using GalaSoft.MvvmLight.Command;

namespace App_PasarelaCompras.VistaModelo
{
    public class VMcompras:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _index;

        List<Mproductos> _listaProd;
        List<MdetalleCompras> _listaPreviaCompras;
        bool _IsVisiblePnlDetalleCompras;
        string _cantidadTotal;
        #endregion
        #region CONSTRUCTOR
        public VMcompras(INavigation navigation, StackLayout ladoDerecha, StackLayout ladoIzquierda)
        {
            Navigation = navigation;
            _ = MostrarProductosBDD(ladoDerecha, ladoIzquierda);
            IsVisiblePnlDetalleCompras = false;
        }
        #endregion
        #region OBJETOS
        public string CantidadTotal
        {
            get { return _cantidadTotal; }
            set { SetValue(ref _cantidadTotal, value); }
        }
        public bool IsVisiblePnlDetalleCompras
        {
            get { return _IsVisiblePnlDetalleCompras; }
            set { SetValue(ref _IsVisiblePnlDetalleCompras, value); }
        }

        public List<MdetalleCompras> ListaPreviaCompras
        {
            get { return _listaPreviaCompras; }
            set { SetValue(ref _listaPreviaCompras, value); }
        }
        public List<Mproductos> ListaProductos
        {
            get { return _listaProd; }
            set { SetValue(ref _listaProd, value); }
        }
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS
        public void CrearContenedorProductos(Mproductos item, int index, StackLayout ladoDerecha, StackLayout ladoIzquierda)
        {

            //obtener lado
            var _ubicacion = Convert.ToBoolean(index % 2);

            //asignar lado
            var lado = _ubicacion ? ladoDerecha : ladoIzquierda;

            //contenedor
            var frame = new Frame
            {
                HeightRequest = 350,
                CornerRadius = 10,
                Margin = 4,
                BackgroundColor = Color.White,
                Padding = 20
            };
            var stack = new StackLayout
            {
                //vacio
            };
            var img = new Image
            {
                Source = item.Icono,
                HeightRequest = 200,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };
            var lblPrecio = new Label
            {
                Text = "$" + item.Precio,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                Margin = new Thickness(0, 10),
                TextColor = Color.FromHex("#333333")
            };
            var lblDescrip = new Label
            {
                Text = item.Descripcion,
                FontSize = 18,
                TextColor = Color.Black,
                CharacterSpacing = 1
            };
            var lblCantidad = new Label
            {
                Text = item.Contenido + "ml",
                FontSize = 13,
                TextColor = Color.Gray,
                CharacterSpacing = 1
            };
            stack.Children.Add(img);
            stack.Children.Add(lblPrecio);
            stack.Children.Add(lblDescrip);
            stack.Children.Add(lblCantidad);
            frame.Content = stack;
            //gesto tap
            var tap = new TapGestureRecognizer();
            tap.Tapped += async (object sender, EventArgs e) => {
                //transicion efecto
                var page = (App.Current.MainPage as SharedTransitionNavigationPage).CurrentPage; //pag origen
                SharedTransitionNavigationPage.SetBackgroundAnimation(page, BackgroundAnimation.SlideFromTop);
                SharedTransitionNavigationPage.SetTransitionDuration(page, 1000); //duracion animacion
                SharedTransitionNavigationPage.SetTransitionSelectedGroup(page, item.IdProducto);
                await Navigation.PushAsync(new AgregarCompra(item));
            };
            //ubicar 
            lado.Children.Add(frame);
            //asignar tap
            stack.GestureRecognizers.Add(tap);
        }

        //mostrar prod de la bd de firebase
        public async Task MostrarProductosBDD(StackLayout ladoDerecha, StackLayout ladoIzquierda)
        {
            var funcion = new Dproductos();
            ListaProductos = await funcion.MostrarProductos();
            //limpiar
            ladoDerecha.Children.Clear();
            ladoIzquierda.Children.Clear();
            //recorrer y agregar
            foreach (var item in ListaProductos)
            {
                CrearContenedorProductos(item, _index, ladoDerecha, ladoIzquierda);
                _index++;
            }
        }
        public async Task PreviaDetalleCompra()
        {
            var funcion = new DdetalleCompra();
            ListaPreviaCompras = await funcion.PreviaDetalleCompra();
        }
        public async Task Pagar()
        {
            var funcion = new DdetalleCompra();
            ListaPreviaCompras = await funcion.PreviaPagar();
        }
        public async Task ProcesoAsyncrono()
        {
            await Task.Delay(1000);
        }
        public async Task MostrarPnlDetalleCompras(Grid gridProdcutos, StackLayout pnlDetalleCompras, StackLayout pnlContador)
        {
            uint duration = 500;
            await Task.WhenAll(
                    pnlContador.FadeTo(0, 500),
                    gridProdcutos.TranslateTo(0, -200, duration, Easing.CubicIn),
                    pnlDetalleCompras.TranslateTo(0, -300, duration, Easing.CubicIn)
                ) ;
            IsVisiblePnlDetalleCompras = true;
        }
        public async Task MostrarGridProductos(Grid gridProdcutos, StackLayout pnlDetalleCompras, StackLayout pnlContador)
        {
            uint duration = 1500;
            await Task.WhenAll(
                    pnlContador.FadeTo(1, 500),
                    gridProdcutos.TranslateTo(0, 0, duration + 200, Easing.CubicIn),
                    pnlDetalleCompras.TranslateTo(0, 1000, duration, Easing.CubicIn)
                );
            IsVisiblePnlDetalleCompras = false;
        }

        public async Task SumarCantidades(){
            var funcion = new DdetalleCompra();
            CantidadTotal = await funcion.SumarCantitad();
        }

        /// <summary>
        /// eliminar
        /// </summary>
        public void Elimnar()
        {
            _ = DisplayAlert("Eliminar", "Se va a eliminar", "OK");
        }



        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
        public ICommand Eliminarcommand => new Command(Elimnar);

    }
}
