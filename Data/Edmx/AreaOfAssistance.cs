//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class AreaOfAssistance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AreaOfAssistance()
        {
            this.Login = new HashSet<Login>();
        }
    
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public Nullable<int> DefaultLogoffMinutes { get; set; }
        public Nullable<bool> SkipsCourseSelection { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login> Login { get; set; }
    }
}
