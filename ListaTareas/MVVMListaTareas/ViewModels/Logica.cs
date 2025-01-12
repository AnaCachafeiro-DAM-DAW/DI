using System.Collections.ObjectModel; // Cambiado a ObservableCollection
using System.Diagnostics;
using System.Windows.Input;
using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.ViewModels;
using ListaTareas.MVVMListaTareas.Views;

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

        // Método para editar una tarea en Logica.cs
        private async void EditarTarea(Tarea tarea)
        {
            if (tarea == null) return;

            // Navegación a DetalleTarea y pasar la tarea como parámetro
            try
            {
                var viewModel = new DetalleTareaViewModel(tarea, tareaActualizada =>
                {
                    // Actualizar las listas después de editar
                    MoverTarea(tareaActualizada);
                });
                var detalleTareaPage = new DetalleTarea { BindingContext = viewModel };
                await Shell.Current.Navigation.PushAsync(detalleTareaPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al navegar a DetalleTarea: {ex.Message}");
            }
        }


        // Método para manejar el cambio de estado de una tarea
        public void MoverTarea(Tarea tarea)
        {
            // Si la tarea está completada, debe ir a la lista de tareas completadas
            if (tarea.EstaCompletada)
            {
                // Elimina la tarea de la lista activa (si está en ella) y la agrega a la lista completada
                if (TareasActivas.Contains(tarea))
                {
                    TareasActivas.Remove(tarea);
                }
                if (!TareasCompletadas.Contains(tarea))
                {
                    TareasCompletadas.Add(tarea);
                }
            }
            // Si la tarea no está completada, debe ir a la lista de tareas activas
            else
            {
                // Elimina la tarea de la lista completada (si está en ella) y la agrega a la lista activa
                if (TareasCompletadas.Contains(tarea))
                {
                    TareasCompletadas.Remove(tarea);
                }
                if (!TareasActivas.Contains(tarea))
                {
                    TareasActivas.Add(tarea);
                }
            }
        }
    }

    }

