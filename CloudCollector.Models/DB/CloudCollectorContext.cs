using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CloudCollector.Models
{
    public partial class CloudCollectorContext : DbContext
    {
        public CloudCollectorContext()
        {
        }
        public CloudCollectorContext(DbContextOptions<CloudCollectorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cloud> Clouds { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Config> Configs { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        optionsBuilder.UseSqlServer(@"Server=LJJ\SQLSERVER;Database=CloudCollector;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatorName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Img)
                   .HasMaxLength(255)
                   .IsUnicode(false);
                entity.Property(e => e.Memo)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<Cloud>(entity =>
            {
                entity.ToTable("Cloud");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatorName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Memo)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Pic).HasMaxLength(250).IsUnicode(false);
                entity.Property(e => e.OrderNo).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Context)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatorName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HeadImg)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.ToTable("Config");

                entity.Property(e => e.Keys)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Value)
                   .HasMaxLength(4000)
                   .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
