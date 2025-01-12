
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
namespace ListaTareas.MVVMListaTareas.Views;

public partial class Completadas : ContentPage
{
    public Completadas()
    {
        InitializeComponent();

        BindingContext = new CompletadasVM(); // Aquí se asigna el ViewModel
    }

    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // Validar que el sender es un CheckBox y su BindingContext es una tarea
        if (sender is CheckBox checkBox && checkBox.BindingContext is Tarea tarea)
        {
            // Invocar el método MoverTarea del ViewModel
            if (BindingContext is Logica viewModel)
            {
                viewModel.MoverTarea(tarea);
            }
        }

    }
}