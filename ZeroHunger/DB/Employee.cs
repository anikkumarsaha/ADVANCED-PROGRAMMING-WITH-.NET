//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZeroHunger.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Employee()
        {
            this.CollectRequests = new HashSet<CollectRequest>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCompletedRequests { get; set; }
        public string Location { get; set; }
        public int NGOId { get; set; }
    
        public virtual ICollection<CollectRequest> CollectRequests { get; set; }
        public virtual NGO NGO { get; set; }
    }
}
