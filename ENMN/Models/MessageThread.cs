//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENMN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MessageThread
    {
        public MessageThread()
        {
            this.Messages = new HashSet<Message>();
        }
    
        public int MessageThreadID { get; set; }
        public int NurseID { get; set; }
        public int MotherID { get; set; }
    
        public virtual ICollection<Message> Messages { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person Person1 { get; set; }
    }
}
