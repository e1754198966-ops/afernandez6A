using System.Net;

namespace afernandez6A.Views;

public partial class vAgregarEstudiante : ContentPage
{
    private const string Url = "http://192.168.18.11/ws_estudiante/estudiante_ws.php";

    public vAgregarEstudiante()
    {
        InitializeComponent();
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (WebClient cliente = new WebClient())
            {
                System.Collections.Specialized.NameValueCollection parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);

                cliente.UploadValues(Url, "POST", parametros);
                await Navigation.PushAsync(new vEstudiante());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", "Error " + ex.Message, "OK");
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vEstudiante());
    }
}