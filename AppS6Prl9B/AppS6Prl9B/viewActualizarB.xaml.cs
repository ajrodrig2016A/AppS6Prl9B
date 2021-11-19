using RestSharp;
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
    public partial class viewActualizarB : ContentPage
    {
        public viewActualizarB(Datos datos)
        {
            InitializeComponent();
            //llenar datos en controles
            txtCodigo.Text = Convert.ToString(datos.codigo);
            txtNombre.Text = datos.nombre;
            txtApellido.Text = datos.apellido;
            txtEdad.Text = Convert.ToString(datos.edad);
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient("http://192.168.200.3/moviles/post.php?codigo=" + txtCodigo.Text + "&nombre=" + txtNombre.Text + "&apellido=" + txtApellido.Text + "&edad=" + txtEdad.Text);
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    DisplayAlert("Alerta", "Actualizacion correcta", "OK");
                    limpiarCampos();

                }
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        private void limpiarCampos()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEdad.Text = string.Empty;
        }
    }
}