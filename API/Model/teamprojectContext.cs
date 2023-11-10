using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Model
{
    public partial class teamprojectContext : DbContext
    {
        public teamprojectContext()
        {
        }

        public teamprojectContext(DbContextOptions<teamprojectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Content> Contents { get; set; } = null!;
        public virtual DbSet<Developer> Developers { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Library> Libraries { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductGenre> ProductGenres { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Support> Supports { get; set; } = null!;
        public virtual DbSet<SupportType> SupportTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=1941;database=teamproject", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(e => e.ContentsId)
                    .HasName("PRIMARY");

                entity.ToTable("contents");

                entity.HasIndex(e => e.UserId, "User_id_FK_idx");

                entity.HasIndex(e => e.SupportRequestId, "support_request_id_idx");

                entity.Property(e => e.ContentsId)
                    .ValueGeneratedNever()
                    .HasColumnName("contents_id");

                entity.Property(e => e.ContentMessege)
                    .HasMaxLength(200)
                    .HasColumnName("content_messege");

                entity.Property(e => e.SupportRequestId).HasColumnName("support_request_id");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.SupportRequest)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.SupportRequestId)
                    .HasConstraintName("support_request_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("User_id_FK");
            });

            modelBuilder.Entity<Developer>(entity =>
            {
                entity.ToTable("developer");

                entity.Property(e => e.DeveloperId)
                    .ValueGeneratedNever()
                    .HasColumnName("developer_id");

                entity.Property(e => e.DeveloperName)
                    .HasMaxLength(45)
                    .HasColumnName("developer_name");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("genre_id");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(45)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<Library>(entity =>
            {
                entity.ToTable("library");

                entity.HasIndex(e => e.LibraryStatus, "library_status_id_FK_idx");

                entity.HasIndex(e => e.UserId, "library_user_id_FK_idx");

                entity.HasIndex(e => e.ProductId, "libraty_product_id_FK_idx");

                entity.Property(e => e.LibraryId)
                    .ValueGeneratedNever()
                    .HasColumnName("library_id");

                entity.Property(e => e.LibraryStatus).HasColumnName("library_status");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.LibraryStatusNavigation)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.LibraryStatus)
                    .HasConstraintName("library_status_id_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("library_product_id_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("library_user_id_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.ProductDeveloper, "developer_id_FK_idx");

                entity.HasIndex(e => e.ProductStatus, "product_status_id_FK_idx");

                entity.HasIndex(e => e.ProductPublisher, "publisher_id_FK_idx");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(200)
                    .HasColumnName("product_description");

                entity.Property(e => e.ProductDeveloper).HasColumnName("product_developer");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(70)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("product_price");

                entity.Property(e => e.ProductPublisher).HasColumnName("product_publisher");

                entity.Property(e => e.ProductStatus).HasColumnName("product_status");

                entity.HasOne(d => d.ProductDeveloperNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductDeveloper)
                    .HasConstraintName("developer_id_FK");

                entity.HasOne(d => d.ProductPublisherNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductPublisher)
                    .HasConstraintName("publisher_id_FK");

                entity.HasOne(d => d.ProductStatusNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductStatus)
                    .HasConstraintName("product_status_id_FK");
            });

            modelBuilder.Entity<ProductGenre>(entity =>
            {
                entity.ToTable("product_genre");

                entity.HasIndex(e => e.GenreId, "genre_id_FK_idx");

                entity.HasIndex(e => e.ProductId, "product_id_FK_idx");

                entity.Property(e => e.ProductGenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_genre_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.ProductGenres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("genre_id_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductGenres)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_id_FK");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_image");

                entity.HasIndex(e => e.ProductId, "product_image_id_FK_idx");

                entity.Property(e => e.ProductImageId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_image_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductImagePath)
                    .HasMaxLength(45)
                    .HasColumnName("product_image_path");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("product_image_id_FK");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("publisher");

                entity.Property(e => e.PublisherId)
                    .ValueGeneratedNever()
                    .HasColumnName("publisher_id");

                entity.Property(e => e.PublisherName)
                    .HasMaxLength(45)
                    .HasColumnName("publisher_name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("Role_Id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(45)
                    .HasColumnName("Role_Name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(45)
                    .HasColumnName("status_name");
            });

            modelBuilder.Entity<Support>(entity =>
            {
                entity.ToTable("supports");

                entity.HasIndex(e => e.SupportStatus, "support_status_id_FK_idx");

                entity.HasIndex(e => e.SupportType, "support_type_id_FK_idx");

                entity.HasIndex(e => e.UserId, "support_user_id_FK_idx");

                entity.Property(e => e.SupportId)
                    .ValueGeneratedNever()
                    .HasColumnName("support_id");

                entity.Property(e => e.SupportStatus).HasColumnName("support_status");

                entity.Property(e => e.SupportType).HasColumnName("support_type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.SupportStatusNavigation)
                    .WithMany(p => p.Supports)
                    .HasForeignKey(d => d.SupportStatus)
                    .HasConstraintName("support_status_id_FK");

                entity.HasOne(d => d.SupportTypeNavigation)
                    .WithMany(p => p.Supports)
                    .HasForeignKey(d => d.SupportType)
                    .HasConstraintName("support_type_id_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Supports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("support_user_id_FK");
            });

            modelBuilder.Entity<SupportType>(entity =>
            {
                entity.ToTable("support_type");

                entity.Property(e => e.SupportTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("support_type_id");

                entity.Property(e => e.SupportTypeName)
                    .HasMaxLength(70)
                    .HasColumnName("support_type_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.UserRole, "User_Role_Id_FK_idx");

                entity.HasIndex(e => e.UserStatus, "User_Status_id_FK_idx");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_User");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .HasColumnName("User_Email");

                entity.Property(e => e.UserImage)
                    .HasMaxLength(50)
                    .HasColumnName("User_Image");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(20)
                    .HasColumnName("User_Login");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("User_Name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(20)
                    .HasColumnName("User_Password");

                entity.Property(e => e.UserRole).HasColumnName("User_Role");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .HasConstraintName("User_Role_Id_FK");

                entity.HasOne(d => d.UserStatusNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatus)
                    .HasConstraintName("User_Status_id_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
