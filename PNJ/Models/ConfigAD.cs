//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PNJ.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConfigAD
    {
        public int id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string directoryDomain { get; set; }
        public string directoryPath { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> sort { get; set; }
    }
}
