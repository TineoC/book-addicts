//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteca
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public System.Guid id { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string publisher { get; set; }
        public System.DateTime createdDate { get; set; }
        public string description { get; set; }
        public string cover { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public Nullable<double> valoration { get; set; }
    }
}
