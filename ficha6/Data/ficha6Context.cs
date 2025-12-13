using System;
using System.Collections.Generic;
using ficha6.Models;
using Microsoft.EntityFrameworkCore;

namespace ficha6.Data;

public partial class ficha6Context : DbContext
{
    public ficha6Context()
    {
    }

    public ficha6Context(DbContextOptions<ficha6Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ficha6Context");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3213E83F76ACC900");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PK__student__FD291E40467E3FFA");

            entity.HasOne(d => d.Class).WithMany(p => p.Students).HasConstraintName("FK__student__class_i__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
