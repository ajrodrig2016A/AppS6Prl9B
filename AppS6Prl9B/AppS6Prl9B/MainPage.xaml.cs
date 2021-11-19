using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AppS6Prl9B
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://192.168.200.3/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;
        private Datos datosSelected;

        public MainPage()
        {
            InitializeComponent();
            GetDatos();
        }

        private async void GetDatos()
        {
            var content = await client.GetStringAsync(Url);
            List<AppS6Prl9B.Datos> posts = JsonConvert.DeserializeObject<List<Datos>>(content);
            _post = new ObservableCollection<Datos>(posts);

            MyListView.ItemsSource = _post;
        }

        private async void btnPost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewInsertarB());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        private async void btnPut_Clicked(object sender, EventArgs e)
        {
            Datos datos = datosSelected;

            if (datos == null)
            {
                await DisplayAlert("Mensaje informativo", "Favor seleccionar un registro del ListView", "OK");
                return;
            }
            await Navigation.PushAsync(new viewActualizarB(datos));
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Datos datos = datosSelected;

                if (datos == null)
                {
                    await DisplayAlert("Mensaje informativo", "Favor seleccionar un registro del ListView", "OK");
                    return;
                }

                var action = await DisplayAlert("Mensaje de confirmación", "Usted está seguro de eliminar el registro", "Aceptar", "Cancelar");

                if (action.Equals(true))
                {
                    var client = new RestClient("http://192.168.200.3/moviles/post.php?codigo=" + Convert.ToString(datos.codigo));
                    client.Timeout = -1;
                    var request = new RestRequest(Method.DELETE);
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        await DisplayAlert("Alerta", "Eliminacion correcta", "OK");
                        GetDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objDatos = ((Xamarin.Forms.ListView)sender).SelectedItem as Datos;

            if (objDatos == null)
            {
                return;
            }
            datosSelected = objDatos;
        }
    }
}
