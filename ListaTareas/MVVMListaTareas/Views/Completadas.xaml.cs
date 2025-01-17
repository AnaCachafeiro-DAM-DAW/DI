
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
using System.Diagnostics;

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

    // Método a sincrónico
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is Tarea tarea)
        {
            // Actualiza el estado de la tarea
            tarea.EstaCompletada = e.Value;

            // Llamar al método MoverTarea del ViewModel
            AppShell.Logica.MoverTarea(tarea);
        }

        // Navegar a la pantalla de completadas
        Shell.Current.GoToAsync("Completadas");
    }



}
