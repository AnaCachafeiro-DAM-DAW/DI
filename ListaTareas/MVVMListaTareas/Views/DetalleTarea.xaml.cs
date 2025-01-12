using ListaTareas.MVVMListaTareas.ViewModels;

namespace ListaTareas.MVVMListaTareas.Views;

public partial class DetalleTarea : ContentPage
{

    public string Tarea { get; set; }
    public bool Estado { get; set; }

    public DetalleTarea()
    {
        InitializeComponent();

        BindingContext = new DetalleTareaVM(); // Aqu� se asigna el ViewModel
    }
}
