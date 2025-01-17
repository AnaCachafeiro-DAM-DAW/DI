using Lista;
using ListaTareas.MVVMListaTareas.ViewModels;
using ListaTareas.MVVMListaTareas.Views;

namespace ListaTareas
{
    public partial class AppShell : Shell
    {


        public AppShell()
        {
            InitializeComponent();  // Este método inicializa los componentes definidos en XAML

            Logica = new Logica(); // Inicializar Logica

            // ruta para la pantalla detalle
            Routing.RegisterRoute("DetalleTarea", typeof(DetalleTarea));
            // ruta para la pantalla completadas
            Routing.RegisterRoute("Completadas", typeof(Completadas));

        }

        internal static Logica Logica { get; set; }
    }
}
