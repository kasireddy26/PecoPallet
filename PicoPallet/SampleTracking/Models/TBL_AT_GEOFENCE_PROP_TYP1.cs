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
    
    public partial class TBL_AT_GEOFENCE_PROP_TYP1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_AT_GEOFENCE_PROP_TYP1()
        {
            this.TBL_AT_GEOFENCE_PROPERTY1 = new HashSet<TBL_AT_GEOFENCE_PROPERTY1>();
        }
    
        public int IID { get; set; }
        public string GEOFENCE_PROP_TYP_NM { get; set; }
        public string GEOFENCE_PROP_TYP_INPUT { get; set; }
        public string ADDED_BY { get; set; }
        public Nullable<System.DateTime> CREATION_DATE { get; set; }
        public string LAST_MODIFY_BY { get; set; }
        public Nullable<System.DateTime> LAST_MODIFY_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_AT_GEOFENCE_PROPERTY1> TBL_AT_GEOFENCE_PROPERTY1 { get; set; }
    }
}
