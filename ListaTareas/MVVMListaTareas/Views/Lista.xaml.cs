
using ListaTareas.MVVMListaTareas.ViewModels;
namespace ListaTareas.MVVMListaTareas.Views;

public partial class Lista : ContentPage
{
    public Lista()
    {
		InitializeComponent(); // Esto conecta la parte XAML con el c�digo detr�s

        BindingContext = new Logica(); // Aqu� se asigna el ViewModel
    }
}
