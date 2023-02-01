using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App_PasarelaCompras.Droid
{
   // [Activity(Label = "splashScreen")]
    [Activity(Label = "App_PasarelaCompras", Icon = "@mipmap/icon", Theme = "@style/temaApp", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize)]

    public class splashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //enviar suscripcion de anterior configuracion de inicio
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Create your application here


        }
    }
}