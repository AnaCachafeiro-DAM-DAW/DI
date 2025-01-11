
using ListaTareas.MVVMListaTareas.Models;
namespace ListaTareas.MVVMListaTareas.Views;

// mapea el par�metro tarea que se pasa en la navegaci�n y lo asigna a la propiedad Tarea de DetalleTarea
 [QueryProperty("Tarea", "tarea")]
 [QueryProperty("Estado", "estado")]

public partial class DetalleTarea : ContentPage
{

    public string Tarea { get; set; }
    public bool Estado { get; set; }

    public DetalleTarea()
	{
		InitializeComponent();
    }
}