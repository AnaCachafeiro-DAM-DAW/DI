using ListaTareas.MVVMListaTareas.ViewModels;
using ListaTareas.MVVMListaTareas.Models;
using System.Collections.ObjectModel;
using ListaTareas;

[QueryProperty(nameof(NombreTarea), "NombreTarea")]
[QueryProperty(nameof(EstaCompletada), "EstaCompletada")]
[QueryProperty(nameof(Importancia), "Importancia")]

public class CompletadasVM : LogicaCambios
{

    private string nombreTarea;
    public string NombreTarea
    {
        get => nombreTarea;
        set => SetProperty(ref nombreTarea, value);
    }

    private bool estaCompletada;
    public bool EstaCompletada
    {
        get => estaCompletada;
        set => SetProperty(ref estaCompletada, value);
    }

    private bool importancia;
    public bool Importancia
    {
        get => importancia;
        set => SetProperty(ref importancia, value);
    }

    // Lista de tareas completadas que será mostrada en la vista
    public ObservableCollection<Tarea> TareasCompletadas { get; set; }

    public CompletadasVM()
    {
        // llamo a las tareas completadas desde el método de Logica
        TareasCompletadas = new ObservableCollection<Tarea>(AppShell.Logica.TareasCompletadas);
    }
}
