using ListaTareas;
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;


public class CompletadasVM : LogicaCambios
{

    // variables lógica
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

    // Propiedad para el comando retornar
    public ICommand CancelarCommand { get; } // botón que retorna


    public CompletadasVM()
    {
        // Copia inicial de tareas completadas
        TareasCompletadas = new ObservableCollection<Tarea>(AppShell.Logica.TareasCompletadas);

        // Vincular a los cambios en la colección global
        AppShell.Logica.TareasCompletadas.CollectionChanged += (s, e) =>
        {
            SincronizarConGlobal();
        };

    }
    

// este método SincronizarConGlobal mantendrá ambas listas sincronizadas
private void SincronizarConGlobal()
    {
        TareasCompletadas.Clear();
        foreach (var tarea in AppShell.Logica.TareasCompletadas)
        {
            TareasCompletadas.Add(tarea);
        }
    }


}

