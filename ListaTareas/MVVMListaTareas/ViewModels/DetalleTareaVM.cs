
using ListaTareas.MVVMListaTareas.ViewModels;
using System.Windows.Input;

[QueryProperty(nameof(NombreTarea), "NombreTarea")]
[QueryProperty(nameof(EstaCompletada), "EstaCompletada")]
public class DetalleTareaVM : LogicaCambios
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

    public ICommand GuardarCommand { get; }

    public DetalleTareaVM()
    {
        GuardarCommand = new Command(GuardarCambios);
    }

    private async void GuardarCambios()
    {
        // Guarda los cambios y vuelve a la pantalla anterior.
        await Shell.Current.GoToAsync("..");
    }
}
