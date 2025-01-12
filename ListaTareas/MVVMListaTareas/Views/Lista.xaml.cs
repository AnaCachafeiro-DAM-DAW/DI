
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
        var checkbox = sender as CheckBox;

        // Asegurándonos de que el contexto de enlace es correcto y que tenemos la tarea
        if (checkbox != null && checkbox.BindingContext is Tarea tarea)
        {
            // Accedemos a la clase Logica desde el BindingContext
            var viewModel = BindingContext as Logica;

            // Llamamos al método MoverTarea en el ViewModel (Logica)
            if (viewModel != null)
            {
                viewModel.MoverTarea(tarea);
            }
        }
    }

}

