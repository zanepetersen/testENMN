﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class zpeterseEntities : DbContext
    {
        public zpeterseEntities()
            : base("name=zpeterseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ForumPost> ForumPosts { get; set; }
        public virtual DbSet<ForumThread> ForumThreads { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageThread> MessageThreads { get; set; }
        public virtual DbSet<Mother> Mothers { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<TextBlast> TextBlasts { get; set; }
        public virtual DbSet<TextField> TextFields { get; set; }
        public virtual DbSet<TextFieldResponse> TextFieldResponses { get; set; }
        public virtual DbSet<TextView> TextViews { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
    }
}
