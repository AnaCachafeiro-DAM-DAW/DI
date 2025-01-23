using ListaTareas.MVVMListaTareas.Models;

namespace ListaTareas.MVVMListaTareas.Views;

public partial class DetalleTarea : ContentPage
{

    public string Tarea { get; set; }
    public bool Estado { get; set; }

    public DetalleTarea()
    {
        InitializeComponent();

        // BindingContext = new DetalleTareaVM(); // Aquí se asigna el ViewModel
        BindingContext = AppShell.Logica;


    }

    private async void OnGuardarCambiosButtonClicked(object sender, EventArgs e)
    {
        // Accede a la tarea que está vinculada al BindingContext de la página
        var tarea = BindingContext as Tarea;

        // Asegúrate de que la tarea se ha actualizado correctamente antes de navegar
        if (tarea != null)
        {
            // Mover la tarea a completadas si es necesario
            AppShell.Logica.MoverTarea(tarea);
        }

        // Navegar a la pantalla de completadas
        await Shell.Current.GoToAsync("Completadas");
    }

}
