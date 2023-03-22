using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TUPApp.Models;

public partial class TupContext : DbContext
{
    public TupContext()
    {
    }

    public TupContext(DbContextOptions<TupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EducationBg> EducationBgs { get; set; }

    public virtual DbSet<Emergency> Emergencies { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Training> Trainings { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-QNIGV0L\\SQLEXPRESS01;Database=tup;ConnectRetryCount=0;user=sa;password=l123;Persist Security Info=true;trustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EducationBg>(entity =>
        {
            entity.ToTable("EducationBG");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Course)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.School)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.EducationBgs)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_EducationBG_Student1");
        });

        modelBuilder.Entity<Emergency>(entity =>
        {
            entity.ToTable("Emergency");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Emergencies)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Emergency_Student");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experience");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Companyname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Experience_Student");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.Property(e => e.Skills)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Skills)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Skills_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Summary).IsUnicode(false);
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Trainingname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Training)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Trainings_Student1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
