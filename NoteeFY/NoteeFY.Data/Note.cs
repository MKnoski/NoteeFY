//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoteeFY.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Note
    {
        public Note()
        {
            this.TaskItems = new HashSet<TaskItem>();
        }
    
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public string UserID { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}