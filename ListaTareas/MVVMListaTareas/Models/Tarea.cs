
namespace ListaTareas.MVVMListaTareas.Models
{
     public class Tarea
     
    {       
        // En esta clase vamos a tener los datos (variables) *entidades
        // get / set encapsulan el acceso a los campos de de la clase. funcionan como en java
        // lo hace de manera autoimplmentada. reducen código
        public string NombreTarea { get; set; } = string.Empty; // nombre tarea
        public bool EstaCompletada { get; set; } = false; // Estado (completada o no)
    }
}
