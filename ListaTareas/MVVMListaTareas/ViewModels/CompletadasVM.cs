using ListaTareas.MVVMListaTareas.ViewModels;
using System.Windows.Input;

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

}
