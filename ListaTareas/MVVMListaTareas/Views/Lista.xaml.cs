
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
namespace ListaTareas.MVVMListaTareas.Views;

public partial class Lista : ContentPage
{
    public Lista()
    {
		InitializeComponent(); // Esto conecta la parte XAML con el código detrás

        BindingContext = new Logica(); // Aquí se asigna el ViewModel
    }

    // Método para manejar el cambio de estado del checkBox y mover las listas de tareas
    // Llmamos a este método cada vez que el checkBox cambia de estado
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Tarea tarea)
        {
            if (BindingContext is Logica viewModel)
            {
                viewModel.MoverTarea(tarea);
            }
        }
    }
}
