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
    
    public partial class mecanicos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mecanicos()
        {
            this.mantenimiento = new HashSet<mantenimiento>();
        }
    
        public int id_mecanico { get; set; }
        public string Nombre { get; set; }
        public string cedula { get; set; }
        public Nullable<int> telefono { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mantenimiento> mantenimiento { get; set; }
    }
}
