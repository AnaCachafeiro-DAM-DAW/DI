using System.Collections.ObjectModel; // Cambiado a ObservableCollection
using System.Diagnostics;
using System.Windows.Input;
using ListaTareas.MVVMListaTareas.Models;

namespace ListaTareas.MVVMListaTareas.ViewModels
{
    class Logica : LogicaCambios
    {
        // Lista de tareas: ObservableCollection<Tarea> cambios automáticos
        // Esto permite que los cambios (agregar/eliminar) se reflejen automáticamente en la vista
        // Estas listas son las que manejaré en cada pantalla
        public ObservableCollection<Tarea> TareasActivas { get; set; } // TareasActivas para el collectionView 
        public ObservableCollection<Tarea> TareasCompletadas { get; set; } // TareasCompletadas para el collectionView 


        // Propiedad para nueva tarea
        private string nuevaTarea;
        public string NuevaTarea
        {
            get => nuevaTarea;
            set => SetProperty(ref nuevaTarea, value); // Notifica cambios al Entry
        }

        // Comandos
        public ICommand AgregarTareaCommand { get; }
        public ICommand EliminarTareaCommand { get; }
        public ICommand EditarTareaCommand { get; }

        public Logica()
        {
            // Las dos listas
            TareasActivas = new ObservableCollection<Tarea>();
            TareasCompletadas = new ObservableCollection<Tarea>();

            // Inicialización de tareas con clasificación automática
            var tareasIniciales = new List<Tarea>
    {
                // añado dos a mano para tener info ya en la pantalla inicial
            new Tarea { NombreTarea = "Cumpleaños Javi", EstaCompletada = false },
            new Tarea { NombreTarea = "Dentista", EstaCompletada = true }
    };

            // Que me añada la tarea 
            foreach (var tarea in tareasIniciales)
            {
                if (tarea.EstaCompletada)
                {
                    TareasCompletadas.Add(tarea);
                }
                else
                {
                    TareasActivas.Add(tarea);
                }
            }

            // Inicialización de los comandos
            AgregarTareaCommand = new Command(AgregarTarea);
            EliminarTareaCommand = new Command<Tarea>(EliminarTarea);
            EditarTareaCommand = new Command<Tarea>(EditarTarea);
        }


        // Método para agregar una nueva tarea
        private void AgregarTarea()
        {
            // Verifica que el campo no esté vacío
            if (!string.IsNullOrWhiteSpace(NuevaTarea))
            {
                // Agrega la nueva tarea a la colección
                TareasActivas.Add(new Tarea { NombreTarea = NuevaTarea, EstaCompletada = false });

                // Limpia el campo de texto
                NuevaTarea = string.Empty;
            }

        }

        // Método para eliminar una tarea
        private void EliminarTarea(Tarea tarea)
        {
            if (tarea != null)
            {
                // Elimina la tarea de la colección
                TareasActivas.Remove(tarea);
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

        // Método para manejar el cambio de estado de una tarea
        public void MoverTarea(Tarea tarea)
        {
            if (tarea.EstaCompletada)
            {
                // Mover a tareas completadas
                TareasActivas.Remove(tarea);
                if (!TareasCompletadas.Contains(tarea))
                {
                    TareasCompletadas.Add(tarea);
                }
            }
            else
            {
                // Mover a tareas activas
                TareasCompletadas.Remove(tarea);
                if (!TareasActivas.Contains(tarea))
                {
                    TareasActivas.Add(tarea);
                }
            }
        }

    }

}

