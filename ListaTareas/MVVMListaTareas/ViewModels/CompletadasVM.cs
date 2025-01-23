using ListaTareas;
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

//[QueryProperty(nameof(NombreTarea), "NombreTarea")]
//[QueryProperty(nameof(EstaCompletada), "EstaCompletada")]
//[QueryProperty(nameof(Importancia), "Importancia")]

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

        // Inicialización del comando Cancelar
        // shell.current indica que debe regresar a la pantalla anterior
        CancelarCommand = new Command(async () =>
        {
            Console.WriteLine("Ejecutando CancelarCommand");
            await Shell.Current.GoToAsync("//Lista");
            // Navega hacia la página anterior en el stack de Shell
            //..: Navega hacia atrás en el stack de navegación. Esto funciona si llegaste a Completadas desde Lista.
            //Lista: Navega directamente a la página Lista, reiniciando el stack de navegación si es necesario.
        });

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

