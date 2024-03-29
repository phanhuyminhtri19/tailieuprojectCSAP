//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniHostel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BillCompulsoryServiceDetail
    {
        public int ID { get; set; }
        public string BillID { get; set; }
        public string CompulsoryServiceID { get; set; }
        public Nullable<System.DateTime> Time { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The previous number is not valid")]
        public int PreNum { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The current number is not valid")]
        public int CurNum { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "The amount is not valid")]
        public float Amount { get; set; }

        public string Description { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual CompulsoryService CompulsoryService { get; set; }
    }
}
