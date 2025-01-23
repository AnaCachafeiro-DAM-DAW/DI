
using ListaTareas.MVVMListaTareas.Models;

namespace ListaTareas.MVVMListaTareas.Views;

public partial class Completadas : ContentPage
{
    public Completadas()
    {
        InitializeComponent();

        //BindingContext = new CompletadasVM(); // Aquí se asigna el ViewModel
        BindingContext = AppShell.Logica; // Vinculamos directamente la clase lógica
    }


    // Manejador de evento para CheckBox CheckedChanged
    // que sirve para mover de lista a otra lista

    // Método sincrónico
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Tarea tarea)
        {
            // Actualiza el estado de la tarea
            tarea.EstaCompletada = e.Value;

            // Mueve la tarea a la lista correspondiente
            AppShell.Logica.MoverTarea(tarea);
        }
    }
}



