//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QGS.Entities.DataEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMileage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMileage()
        {
            this.tblQuotes = new HashSet<tblQuote>();
        }
    
        public int MileageId { get; set; }
        public Nullable<int> Mileage_K_ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuote> tblQuotes { get; set; }
    }
}
