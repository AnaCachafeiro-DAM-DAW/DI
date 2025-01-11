
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
namespace ListaTareas.MVVMListaTareas.Views;

public partial class Lista : ContentPage
{
    public Lista()
    {
		InitializeComponent(); // Esto conecta la parte XAML con el c�digo detr�s

        BindingContext = new Logica(); // Aqu� se asigna el ViewModel
    }

    // M�todo para manejar el cambio de estado del checkBox y mover las listas de tareas
    // Llmamos a este m�todo cada vez que el checkBox cambia de estado
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
