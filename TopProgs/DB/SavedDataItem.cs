//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TopProgs.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class SavedDataItem
    {
        public int Id { get; set; }
        public Nullable<int> SavedDataID { get; set; }
        public string Key { get; set; }
        public byte[] Data { get; set; }
    
        public virtual SavedDataUser SavedDataUser { get; set; }
    }
}
