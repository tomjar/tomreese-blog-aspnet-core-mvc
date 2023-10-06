using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TRBlog.Controllers;

namespace TRBlog.Database;

public partial class TomreeseblogContext : DbContext
{
    public TomreeseblogContext()
    {
    }

    public TomreeseblogContext(DbContextOptions<TomreeseblogContext> options)
        : base(options)
    {
    }




    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Sspw> Sspws { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("FileName=tomreeseblog.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Createtimestamp)
                .HasColumnType("NUMERIC")
                .HasColumnName("createtimestamp");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Header).HasColumnName("header");
            entity.Property(e => e.Ispublished)
                .HasColumnType("NUMERIC")
                .HasColumnName("ispublished");
            entity.Property(e => e.Modifytimestamp)
                .HasColumnType("NUMERIC")
                .HasColumnName("modifytimestamp");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Createtimestamp)
                .HasColumnType("NUMERIC")
                .HasColumnName("createtimestamp");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Createtimestamp)
                .HasColumnType("NUMERIC")
                .HasColumnName("createtimestamp");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.ToTable("Setting");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AboutSection).HasColumnName("about_section");
            entity.Property(e => e.ArchiveView).HasColumnName("archive_view");
        });

        modelBuilder.Entity<Sspw>(entity =>
        {
            entity.ToTable("Sspw");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Createtimestamp)
                .HasColumnType("NUMERIC")
                .HasColumnName("createtimestamp");
            entity.Property(e => e.Modifytimestamp)
                .HasColumnType("NUMERIC")
                .HasColumnName("modifytimestamp");
            entity.Property(e => e.Pw).HasColumnName("pw");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
