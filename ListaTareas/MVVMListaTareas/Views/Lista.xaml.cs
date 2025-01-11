
using ListaTareas.MVVMListaTareas.ViewModels;
namespace ListaTareas.MVVMListaTareas.Views;

public partial class Lista : ContentPage
{
    public Lista()
    {
		InitializeComponent(); // Esto conecta la parte XAML con el código detrás

        BindingContext = new Logica(); // Aquí se asigna el ViewModel
    }
}
