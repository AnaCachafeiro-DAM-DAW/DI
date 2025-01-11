using System.Collections.ObjectModel; // Cambiado a ObservableCollection
using System.Diagnostics;
using System.Windows.Input;
using ListaTareas.MVVMListaTareas.Models;

namespace ListaTareas.MVVMListaTareas.ViewModels
{
    class Logica : LogicaCambios
    {
        // Lista de tareas: ObservableCollection<Tarea> cambios automáticos
        // Esto permite que los cambios (agregar/eliminar) se reflejen automáticamente en la vista.
        public ObservableCollection<Tarea> Tareas { get; set; } // Tareas para el collectionView 

        // Propiedad para nueva tarea
        private string nuevaTarea;
        public string NuevaTarea
        {
            get => nuevaTarea;
            set => SetProperty(ref nuevaTarea, value); // Notifica cambios al Entry.
        }

        // Comandos
        public ICommand AgregarTareaCommand { get; }
        public ICommand EliminarTareaCommand { get; }
        public ICommand EditarTareaCommand { get; }

        public Logica()
        {
            // Inicialización de la lista de tareas como ObservableCollection.
            Tareas = new ObservableCollection<Tarea>
            {
                new Tarea { NombreTarea = "Cumpleaños Javi", EstaCompletada = false },
                new Tarea { NombreTarea = "Dentista", EstaCompletada = true }
            };

            // Inicialización de los comandos.
            AgregarTareaCommand = new Command(AgregarTarea);
            EliminarTareaCommand = new Command<Tarea>(EliminarTarea);
            EditarTareaCommand = new Command<Tarea>(EditarTarea);
        }

        // Método para agregar una nueva tarea.
        private void AgregarTarea()
        {
            // Verifica que el campo no esté vacío.
            if (!string.IsNullOrWhiteSpace(NuevaTarea))
            {
                // Agrega la nueva tarea a la colección.
                Tareas.Add(new Tarea { NombreTarea = NuevaTarea, EstaCompletada = false });

                // Limpia el campo de texto.
                NuevaTarea = string.Empty;
            }

        }

        // Método para eliminar una tarea.
        private void EliminarTarea(Tarea tarea)
        {
            if (tarea != null)
            {
                // Elimina la tarea de la colección.
                Tareas.Remove(tarea);
            }

        }

        // Método para editar una tarea
        private async void EditarTarea(Tarea tarea)
        {
            if (tarea == null) return;

            try
            {
                // Usa las propiedades del objeto 'tarea' recibido como argumento
                var route = $"DetalleTarea?NombreTarea={Uri.EscapeDataString(tarea.NombreTarea)}&EstaCompletada={tarea.EstaCompletada}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al navegar a DetalleTarea: {ex.Message}");
            }
        }

    }
}
