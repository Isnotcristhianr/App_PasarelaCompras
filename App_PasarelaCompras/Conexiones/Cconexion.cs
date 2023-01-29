using System;
using System.Collections.Generic;
using System.Text;
//firebase
using Firebase.Database;

namespace App_PasarelaCompras.Conexiones
{
    public class Cconexion
    {
        //conexion a la bd firebase
        public static FirebaseClient firebase = new FirebaseClient("https://appcompras-f3230-default-rtdb.firebaseio.com/");

    }
}
