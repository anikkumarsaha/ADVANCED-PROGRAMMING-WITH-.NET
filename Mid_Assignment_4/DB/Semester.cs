//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mid_Assignment_4.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Semester
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CsCount { get; set; }
    
        public virtual Student Student { get; set; }
    }
}
