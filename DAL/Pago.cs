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
    
    public partial class Pago
    {
        public int Id_Pago { get; set; }
        public int Id_Venta { get; set; }
        public Nullable<decimal> Cantidad_Pagada { get; set; }
        public Nullable<System.DateTime> Fecha_De_Pago { get; set; }
        public Nullable<decimal> Saldo_Actual { get; set; }
    
        public virtual Venta Venta { get; set; }
    }
}
