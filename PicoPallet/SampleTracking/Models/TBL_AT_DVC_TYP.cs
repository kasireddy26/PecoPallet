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
    
    public partial class TBL_AT_DVC_TYP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_AT_DVC_TYP()
        {
            this.TBL_AT_DVC = new HashSet<TBL_AT_DVC>();
            this.TBL_AT_DVC_TYP_CAPBLTY = new HashSet<TBL_AT_DVC_TYP_CAPBLTY>();
        }
    
        public int IID { get; set; }
        public string DVC_TYP_DESC { get; set; }
        public string DVC_TYP_MNFCTR { get; set; }
        public Nullable<decimal> DVC_TYP_COST { get; set; }
        public string ADDED_BY { get; set; }
        public Nullable<System.DateTime> CREATION_DATE { get; set; }
        public string LAST_MODIFY_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFY_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_AT_DVC> TBL_AT_DVC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_AT_DVC_TYP_CAPBLTY> TBL_AT_DVC_TYP_CAPBLTY { get; set; }
    }
}
