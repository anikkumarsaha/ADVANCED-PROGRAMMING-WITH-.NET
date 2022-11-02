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
    
    public partial class CollectRequest
    {
        public CollectRequest()
        {
            this.EmployeeCollectRequests = new HashSet<EmployeeCollectRequest>();
            this.FoodDetails = new HashSet<FoodDetail>();
        }
    
        public int Id { get; set; }
        public string Status { get; set; }
        public int RestaurantId { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<EmployeeCollectRequest> EmployeeCollectRequests { get; set; }
        public virtual ICollection<FoodDetail> FoodDetails { get; set; }
    }
}