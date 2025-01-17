using ListaTareas.MVVMListaTareas.Models;
using ListaTareas.MVVMListaTareas.Views;
using System.Collections.ObjectModel; // Cambiado a ObservableCollection
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Input;

namespace ListaTareas.MVVMListaTareas.ViewModels
{
    class Logica : LogicaCambios
    {
        // Lista de tareas: ObservableCollection<Tarea> cambios automáticos
        // Esto permite que los cambios (agregar/eliminar) se reflejen automáticamente en la vista
        // Estas listas son las que manejaré en cada pantalla
        public ObservableCollection<Tarea> TareasActivas { get; set; } // TareasActivas para el collectionView 
        public ObservableCollection<Tarea> TareasCompletadas { get; set; } // TareasCompletadas para el collectionView 

        // Lista de tareas Importantes/ No importantes
        public ObservableCollection<Tarea> TareasImportantes { get; set; }
        public ObservableCollection<Tarea> TareasNoImportantes { get; set; }


        // Propiedad para nueva tarea
        private string nuevaTarea;
        public string NuevaTarea
        {
            get => nuevaTarea;
            set => SetProperty(ref nuevaTarea, value); // Notifica cambios al Entry
        }

        // Comandos
        // son los que se usan en .xaml en los comand
        public ICommand AgregarTareaCommand { get; } // en lista
        public ICommand EliminarTareaCommand { get; } // en lista y completadas
        public ICommand EditarTareaCommand { get; } // en detalle
        public ICommand ToggleImportanciaCommand { get; }  // lista

        public Logica()
        {
            // Inicialización de las listas
            TareasActivas = new ObservableCollection<Tarea>();
            TareasCompletadas = new ObservableCollection<Tarea>();

            TareasImportantes = new ObservableCollection<Tarea>();
            TareasNoImportantes = new ObservableCollection<Tarea>();

            ToggleImportanciaCommand = new Command<Tarea>(AlternarImportancia); // Inicialización del comando


            // Inicialización de tareas con clasificación automática
            var tareasIniciales = new List<Tarea>
    {
                // añado dos a mano para tener info ya en la pantalla inicial y completadas
            new Tarea { NombreTarea = "Cumpleaños Javi", EstaCompletada = false },
            new Tarea { NombreTarea = "Dentista", EstaCompletada = true },
            new Tarea { NombreTarea = "Comprar regalo", EstaCompletada = false, Importancia = true },
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


            // COMANDOS
            // Inicialización de los comandos
            AgregarTareaCommand = new Command(AgregarTarea);
            EliminarTareaCommand = new Command<Tarea>(EliminarTarea);
            EditarTareaCommand = new Command<Tarea>(EditarTarea);
            ToggleImportanciaCommand = new Command<Tarea>(AlternarImportancia); // Nueva lógica para alternar la importancia
        }

        // Métodos para manejar cambios
        private void OnTareasActivasChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TareasActivas));
        }

        private void OnTareasCompletadasChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TareasCompletadas));
        }

        // MÉTODOS
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
                // Elimina las tareas de sus colecciones
                TareasActivas.Remove(tarea);
                TareasCompletadas.Remove(tarea);
            }

        }

        // Método para editar una tarea en Logica.cs
        private async void EditarTarea(Tarea tarea)
        {
            if (tarea == null) return;

            var viewModel = new DetalleTareaVM(tarea, tareaEditada =>
            {
                // Llamar a MoverTarea para actualizar la lista (mover entre tareas activas/completadas)
                MoverTarea(tareaEditada);

                // Ahora reemplazar la tarea editada en la lista original
                var index = TareasActivas.IndexOf(tarea);
                if (index != -1)
                {
                    TareasActivas[index] = tareaEditada;
                }
                else
                {
                    index = TareasCompletadas.IndexOf(tarea);
                    if (index != -1)
                    {
                        TareasCompletadas[index] = tareaEditada;
                    }
                }
            });

            var detalleTareaPage = new DetalleTarea { BindingContext = viewModel };
            await Shell.Current.Navigation.PushAsync(detalleTareaPage);
        }



        // Método para clasificar las tareas
        private void ClasificarTarea(Tarea tarea)
        {
            if (tarea.EstaCompletada)
            {
                // Tareas completadas no necesitan clasificación por importancia
                TareasCompletadas.Add(tarea);
                TareasImportantes.Remove(tarea);
                TareasNoImportantes.Remove(tarea);
            }
            else
            {
                // Clasificación por importancia
                if (tarea.Importancia)
                {
                    if (!TareasImportantes.Contains(tarea))
                    {
                        TareasImportantes.Add(tarea);
                    }
                    TareasNoImportantes.Remove(tarea);
                }
                else
                {
                    if (!TareasNoImportantes.Contains(tarea))
                    {
                        TareasNoImportantes.Add(tarea);
                    }
                    TareasImportantes.Remove(tarea);
                }
            }
        }

        // Método para alternar la importancia de la tarea
        private void AlternarImportancia(Tarea tarea)
        {
            if (tarea == null) return;

            // Cambiar el estado de importancia de la tarea
            tarea.Importancia = !tarea.Importancia;

            // Después de cambiar la importancia, reclasificar la tarea
            ClasificarTarea(tarea);
        }


        // Método para manejar el cambio de estado de una tarea
        public void MoverTarea(Tarea tarea)
        {
            if (tarea == null) return;

            Debug.WriteLine($"MoverTarea: {tarea.NombreTarea}, Completada={tarea.EstaCompletada}");

            if (tarea.EstaCompletada)
            {
                // Mover a completadas
                TareasActivas.Remove(tarea); // Siempre intenta remover de activas
                if (!TareasCompletadas.Contains(tarea))
                {
                    TareasCompletadas.Add(tarea);
                }

                // Eliminar de las listas de importancia si es completada
                TareasImportantes.Remove(tarea);
                TareasNoImportantes.Remove(tarea);
            }
            else
            {
                // Mover a activas
                TareasCompletadas.Remove(tarea); // Siempre intenta remover de completadas
                if (!TareasActivas.Contains(tarea))
                {
                    TareasActivas.Add(tarea);
                }

                // Reclasificar según su importancia
                ClasificarTarea(tarea);
            }

            // Notificar cambios en las colecciones
            OnPropertyChanged(nameof(TareasActivas));
            OnPropertyChanged(nameof(TareasCompletadas));
        }

    }

}




