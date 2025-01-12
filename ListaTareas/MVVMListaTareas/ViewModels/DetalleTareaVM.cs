
using System.Windows.Input;
using ListaTareas.MVVMListaTareas.Models;

namespace ListaTareas.MVVMListaTareas.ViewModels
{
    public class DetalleTareaViewModel : LogicaCambios
    {

        // variables de clase Tarea
        // varible nombre tarea
        private string nombreTarea;
        public string NombreTarea
        {
            get => nombreTarea;
            set => SetProperty(ref nombreTarea, value);
        }

        // variable si está completada o no
        private bool estaCompletada;
        public bool EstaCompletada
        {
            get => estaCompletada;
            set => SetProperty(ref estaCompletada, value);
        }

        // opciones de los controles
        public ICommand GuardarCambiosCommand { get; }
        public ICommand CancelarCommand { get; }

        private Tarea tareaOriginal;

        public DetalleTareaViewModel(Tarea tarea, Action<Tarea> onGuardarCambios)
        {
            tareaOriginal = tarea;
            NombreTarea = tarea.NombreTarea;
            EstaCompletada = tarea.EstaCompletada;

            // Comando para guardar cambios
            GuardarCambiosCommand = new Command(() =>
            {

                // aquí actualiza las propiedades de la tarea original
                tareaOriginal.NombreTarea = NombreTarea;
                tareaOriginal.EstaCompletada = EstaCompletada;

                // llmamos al callback sobre la tarea actual
                onGuardarCambios?.Invoke(tareaOriginal);

                // vuelve a la panatalla anterior
                Shell.Current.GoToAsync("..");
            });

            // comando cancelar cambios
            CancelarCommand = new Command(() =>
            {

                // si no hay cambios, vuelve
                Shell.Current.GoToAsync("..");
            });
        }
    }
}
