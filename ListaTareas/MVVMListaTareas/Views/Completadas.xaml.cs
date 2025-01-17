
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
using System.Diagnostics;

namespace ListaTareas.MVVMListaTareas.Views;

public partial class Completadas : ContentPage
{
    public Completadas()
    {
        InitializeComponent();

        BindingContext = new CompletadasVM(); // Aqu� se asigna el ViewModel
        BindingContext = AppShell.Logica; // Vinculamos directamente la clase l�gica
    }


    // Manejador de evento para CheckBox CheckedChanged
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // Verifica que el sender sea un CheckBox y que el BindingContext sea una tarea
        if (sender is CheckBox checkBox && checkBox.BindingContext is Tarea tarea)
        {
            // Actualizar la propiedad EstaCompletada en la tarea
            tarea.EstaCompletada = e.Value;

            // Invocar el m�todo MoverTarea desde el ViewModel o Logica
            if (BindingContext is CompletadasVM viewModel)
            {
                // Aqu� se mueve la tarea entre listas (activa/completada)
                AppShell.Logica.MoverTarea(tarea);
            }

        }
        }
        }