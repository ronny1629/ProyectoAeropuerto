//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaAeropuerto
{
    using System;
    using System.Collections.Generic;
    
    public partial class propietario
    {
        public int id_propietario { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> cedula { get; set; }
        public Nullable<int> id_avion { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
    
        public virtual avion avion { get; set; }
    }
}
