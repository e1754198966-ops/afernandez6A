using afernandez6A.models;
using System.Net;

namespace afernandez6A.Views;

public partial class vActualizarEliminar : ContentPage
{
    private const string Url = "http://192.168.18.11/ws_estudiante/estudiante_ws.php";

    public vActualizarEliminar(Estudiante estudiante)
    {
        InitializeComponent();

        // Rellenar los campos con los datos del estudiante seleccionado
        txtCodigo.Text = estudiante.codigo.ToString();
        txtNombre.Text = estudiante.nombre;
        txtApellido.Text = estudiante.apellido;
        txtEdad.Text = estudiante.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (WebClient cliente = new WebClient())
            {
            
                string urlActualizar = $"{Url}?codigo={txtCodigo.Text}&nombre={txtNombre.Text}&apellido={txtApellido.Text}&edad={txtEdad.Text}";

 
                cliente.UploadString(urlActualizar, "PUT", "");

                await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
                await Navigation.PushAsync(new vEstudiante());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", "Error " + ex.Message, "OK");
        }
    }
    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlert("Confirmar", "¿Desea eliminar este estudiante?", "Sí", "No");
            if (confirmar)
            {
                using (WebClient cliente = new WebClient())
                {
                    cliente.UploadString(Url + "?codigo=" + txtCodigo.Text, "DELETE", "");

                    await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
                    await Navigation.PushAsync(new vEstudiante());
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", "Error " + ex.Message, "OK");
        }
    }
}