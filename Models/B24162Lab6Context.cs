using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPILab.Models
{
    public partial class B24162Lab6Context : DbContext
    {
        public B24162Lab6Context()
        {
        }

        public B24162Lab6Context(DbContextOptions<B24162Lab6Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=163.178.107.10;Initial Catalog=B24162Lab6;User ID=laboratorios;Password=Saucr.118");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Major>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MajorNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.Major)
                    .HasConstraintName("Major_fk");

                entity.HasOne(d => d.NationalityNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.Nationality)
                    .HasConstraintName("Nationality_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
