using ListaTareas.MVVMListaTareas.ViewModels;
using ListaTareas.MVVMListaTareas.Models;
using System.Collections.ObjectModel;
using ListaTareas;
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

    // Propiedad para el comando Volver
    public ICommand VolverCommand { get; }


    public CompletadasVM()
    {
        // llamo a las tareas completadas desde el método de Logica
        TareasCompletadas = new ObservableCollection<Tarea>(AppShell.Logica.TareasCompletadas);

        // Vincular directamente a la lista de tareas global
        TareasCompletadas = AppShell.Logica.TareasCompletadas;

        // reconoce los cambios en la colección global
        AppShell.Logica.TareasCompletadas.CollectionChanged += OnTareasCompletadasChanged;

        // Inicializa el comando Volver
        VolverCommand = new Command(async () => await Shell.Current.GoToAsync("//Lista"));

    }

    // hacemos que los cambios en el método tareasCompletas se reflejen en vista completadas
    private void OnTareasCompletadasChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(TareasCompletadas)); // actualiza la vista cuando cambia la lista 
    }

    


}

