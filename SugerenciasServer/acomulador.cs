//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SugerenciasServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class acomulador
    {
        public int id { get; set; }
        public int id_palabras { get; set; }
        public int id_base_conocimiento { get; set; }
    
        public virtual baseconocimiento baseconocimiento { get; set; }
        public virtual palabra palabra { get; set; }
    }
}
