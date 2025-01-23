
using System.ComponentModel;

namespace ListaTareas.MVVMListaTareas.Models
{
    public class Tarea

    {

        // En esta clase vamos a tener los datos (variables) *entidades
        // get / set encapsulan el acceso a los campos de de la clase. funcionan como en java
        // lo hace de manera autoimplmentada. reducen código

        private string nombreTarea;
        private bool estaCompletada;
        private bool importancia;

        // Propiedad para el nombre de la tarea
        public string NombreTarea
        {
            get => nombreTarea;
            set
            {
                if (nombreTarea != value)
                {
                    nombreTarea = value;
                    OnPropertyChanged(nameof(NombreTarea));
                }
            }
        }

        // Propiedad para la importancia de la tarea
        public bool Importancia
        {
            get => importancia;
            set
            {
                if (importancia != value)
                {
                    importancia = value;
                    OnPropertyChanged(nameof(Importancia));  // Notificar el cambio a la vista
                }
            }
        }


        // Propiedad para indicar si la tarea está completada
        public bool EstaCompletada
        {
            get => estaCompletada;
            set
            {
                if (estaCompletada != value)
                {
                    estaCompletada = value;
                    OnPropertyChanged(nameof(EstaCompletada));
                }
            }
        }

        // Implementación de la interfaz INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para notificar que una propiedad ha cambiado
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
