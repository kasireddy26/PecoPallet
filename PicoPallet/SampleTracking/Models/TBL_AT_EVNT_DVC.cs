//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SampleTracking.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_AT_EVNT_DVC
    {
        public int IID { get; set; }
        public Nullable<System.DateTime> EVNT_DVC_STRT_TM { get; set; }
        public Nullable<System.DateTime> EVNT_DVC_END_TM { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
        public int DVC_ID { get; set; }
        public Nullable<int> EVNT_ID { get; set; }
        public Nullable<System.DateTime> CREATION_DATE { get; set; }
        public string ADDED_BY { get; set; }
        public string LAST_MODIFY_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFY_DATE { get; set; }
    
        public virtual TBL_AT_DVC TBL_AT_DVC { get; set; }
        public virtual TBL_AT_EVNT TBL_AT_EVNT { get; set; }
    }
}
