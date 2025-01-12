namespace Lista
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Usamos AppShell para que gestione la navegación
            MainPage = new ListaTareas.AppShell();  // AppShell es el punto de entrada
        }
    }
}
