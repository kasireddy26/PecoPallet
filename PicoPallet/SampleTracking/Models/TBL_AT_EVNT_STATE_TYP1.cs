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
    
    public partial class TBL_AT_EVNT_STATE_TYP1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_AT_EVNT_STATE_TYP1()
        {
            this.TBL_AT_EVNT_STATE1 = new HashSet<TBL_AT_EVNT_STATE1>();
        }
    
        public int IID { get; set; }
        public string EVNT_STATE_TYP { get; set; }
        public string ADDED_BY { get; set; }
        public Nullable<System.DateTime> CREATION_DATE { get; set; }
        public string LAST_MODIFY_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFY_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_AT_EVNT_STATE1> TBL_AT_EVNT_STATE1 { get; set; }
    }
}
