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
    
    public partial class Login
    {
        public int ID { get; set; }
        public int FK_Student { get; set; }
        public int FK_Campus { get; set; }
        public int FK_AreaOfAssistance { get; set; }
        public Nullable<int> FK_Course { get; set; }
        public System.DateTime LogInTime { get; set; }
        public Nullable<System.DateTime> LogOutTime { get; set; }
        public Nullable<int> SurveyRating { get; set; }
        public Nullable<int> FK_SurveyReferrer { get; set; }
    
        public virtual AreaOfAssistance AreaOfAssistance { get; set; }
        public virtual LKP_Campus LKP_Campus { get; set; }
        public virtual LKP_Course LKP_Course { get; set; }
        public virtual LKP_SurveyReferrer LKP_SurveyReferrer { get; set; }
        public virtual Student Student { get; set; }
    }
}