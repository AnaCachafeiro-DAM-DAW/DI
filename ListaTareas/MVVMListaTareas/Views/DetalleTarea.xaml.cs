using ListaTareas.MVVMListaTareas.Models;

namespace ListaTareas.MVVMListaTareas.Views;

public partial class DetalleTarea : ContentPage
{

    public string Tarea { get; set; }
    public bool Estado { get; set; }

    public DetalleTarea()
    {
        InitializeComponent();

        // BindingContext = new DetalleTareaVM(); // Aqu� se asigna el ViewModel
        BindingContext = AppShell.Logica;


    }

    private async void OnGuardarCambiosButtonClicked(object sender, EventArgs e)
    {
        // Accede a la tarea que est� vinculada al BindingContext de la p�gina
        var tarea = BindingContext as Tarea;

        // Aseg�rate de que la tarea se ha actualizado correctamente antes de navegar
        if (tarea != null)
        {
            // Mover la tarea a completadas si es necesario
            AppShell.Logica.MoverTarea(tarea);
        }

        // Navegar a la pantalla de completadas
        await Shell.Current.GoToAsync("Completadas");
    }

}
