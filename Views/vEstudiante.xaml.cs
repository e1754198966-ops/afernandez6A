using afernandez6A.models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace afernandez6A.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://192.168.18.11/ws_estudiante/estudiante_ws.php";
    private readonly HttpClient _cliente = new HttpClient();

    private ObservableCollection<Estudiante> _estudiante;

    public vEstudiante()
    {
        InitializeComponent();
        MostrarEstudiante();
    }

    public async void MostrarEstudiante()
    {
        var content = await _cliente.GetStringAsync(Url);
        List<Estudiante> _mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        _estudiante = new ObservableCollection<Estudiante>(_mostrar);
        listaEstudiante.ItemsSource = _estudiante;
    }

    private async void btnIrAgregar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAgregarEstudiante());
    }

    // Evento que se ejecuta al hacer clic en cualquier estudiante de la lista
    private async void listaEstudiante_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var estudianteSeleccionado = e.CurrentSelection.FirstOrDefault() as Estudiante;

        if (estudianteSeleccionado != null)
        {
            // Abre la vista de actualizar/eliminar enviándole los datos del estudiante
            await Navigation.PushAsync(new vActualizarEliminar(estudianteSeleccionado));

            // Limpia la selección para permitir volver a hacer clic en el mismo registro si se desea
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}