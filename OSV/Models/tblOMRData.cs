//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OSV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOMRData
    {
        public int id { get; set; }
        public string PaperCode { get; set; }
        public string CenterCode { get; set; }
        public string RollNo { get; set; }
        public string VersionNo { get; set; }
        public string FirstPageFile { get; set; }
        public string SecondPageFile { get; set; }
        public Nullable<decimal> Marks { get; set; }
        public string MarksDetail { get; set; }
        public string AnswersDetail { get; set; }
    }
}
