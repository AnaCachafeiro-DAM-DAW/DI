using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListaTareas.MVVMListaTareas.ViewModels
{
    // esta es la clase que se encarga de notificar los cambios de la interfaz 
    // la interfaz INotifyPropertyChanged indica que una propiedad en el modelo o modelsView cambió
    // fundamental para que tenga sincronización el MVVM
    // todo lo que cambia en tarea se actualiza
    public class LogicaCambios : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value)) return false;
            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

