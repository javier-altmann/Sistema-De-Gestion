//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            this.Pagos = new HashSet<Pago>();
        }
    
        public int Id_Venta { get; set; }
        public int Id_Cliente { get; set; }
        public Nullable<decimal> Precio_Real_Del_Producto { get; set; }
        public Nullable<decimal> Precio_De_Venta_Del_Producto { get; set; }
        public string Talle { get; set; }
        public string Codigo_Articulo { get; set; }
        public string Producto_Vendido { get; set; }
        public string Fecha_De_Venta { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}