using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppS6Prl9B
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewInsertarB : ContentPage
    {
        public viewInsertarB()
        {
            InitializeComponent();
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                //parametros.Add("codigo", txtCodigo.Text); --Se utiliza un campo PK autoincrementable para codigo de tabla estudiante
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);
                cliente.UploadValues("http://192.168.200.3/moviles/post.php", "POST", parametros);
                DisplayAlert("Alerta", "Ingreso correcto", "OK");
                limpiarCampos();
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        private void limpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEdad.Text = string.Empty;
        }
    }
}