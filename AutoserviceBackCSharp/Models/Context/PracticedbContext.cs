using AutoserviceBackCSharp.Singletone;
using Microsoft.EntityFrameworkCore;

namespace AutoserviceBackCSharp.Models;

public partial class PracticedbContext : DbContext
{
    string _conString;
    public PracticedbContext()
    {
        _conString = string.Empty;
    }
    public PracticedbContext(DbContextOptions<PracticedbContext> options, DbConnection con)
        : base(options)
    {
        _conString = con.ConnectionString;
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<DetailList> DetailLists { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<WorkList> WorkLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(_conString, ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Car");

            entity.HasIndex(e => e.Client, "Car_fk0");

            entity.HasIndex(e => e.CarNumber, "carNumber").IsUnique();

            entity.HasIndex(e => e.Vin, "vin").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarNumber).HasColumnName("carNumber");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.Mark)
                .HasMaxLength(255)
                .HasColumnName("mark");
            entity.Property(e => e.Vin).HasColumnName("vin");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Car_fk0");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.HasIndex(e => e.ParentCategory, "Category_fk0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategory).HasColumnName("parentCategory");

            entity.HasOne(d => d.ParentCategoryNavigation).WithMany(p => p.InverseParentCategoryNavigation)
                .HasForeignKey(d => d.ParentCategory)
                .HasConstraintName("Category_fk0");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsConfirm).HasColumnName("isConfirm");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.TelegramId)
                .HasMaxLength(255)
                .HasColumnName("telegramId");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Detail");

            entity.HasIndex(e => e.Category, "Detail_fk0");

            entity.HasIndex(e => e.VendorCode, "vendorCode").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.CompatibleVehicles)
                .HasMaxLength(255)
                .HasColumnName("compatibleVehicles");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .HasColumnName("model");
            entity.Property(e => e.VendorCode).HasColumnName("vendorCode");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Details)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Detail_fk0");
        });

        modelBuilder.Entity<DetailList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DetailList");

            entity.HasIndex(e => e.Warehouse, "DetailList_fk0");

            entity.HasIndex(e => e.Detail, "DetailList_fk1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.Warehouse).HasColumnName("warehouse");

            entity.HasOne(d => d.DetailNavigation).WithMany(p => p.DetailLists)
                .HasForeignKey(d => d.Detail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DetailList_fk1");

            entity.HasOne(d => d.WarehouseNavigation).WithMany(p => p.DetailLists)
                .HasForeignKey(d => d.Warehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DetailList_fk0");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.Client, "Feedback_fk0");

            entity.HasIndex(e => e.Order, "Feedback_fk1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Feedback_fk0");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Feedback_fk1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.Client, "Order_fk0");

            entity.HasIndex(e => e.Technician, "Order_fk1");

            entity.HasIndex(e => e.Car, "Order_fk2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentTime).HasColumnName("appointmentTime");
            entity.Property(e => e.Car).HasColumnName("car");
            entity.Property(e => e.CarMileage).HasColumnName("carMileage");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.End).HasColumnName("end");
            entity.Property(e => e.FinalPrice).HasColumnName("finalPrice");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.Technician).HasColumnName("technician");

            entity.HasOne(d => d.CarNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Car)
                .HasConstraintName("Order_fk2");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Client)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_fk0");

            entity.HasOne(d => d.TechnicianNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Technician)
                .HasConstraintName("Order_fk1");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Technician");

            entity.HasIndex(e => e.Phone, "phone").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Raiting).HasColumnName("raiting");
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .HasColumnName("specialization");
            entity.Property(e => e.StartWork).HasColumnName("startWork");
            entity.Property(e => e.StartWorkInCompany).HasColumnName("startWorkInCompany");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Work");

            entity.HasIndex(e => e.Detail, "Work_fk0");

            entity.HasIndex(e => e.Order, "Work_fk1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.DetailPrice).HasColumnName("detailPrice");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.WorkPrice).HasColumnName("workPrice");

            entity.HasOne(d => d.DetailNavigation).WithMany(p => p.Works)
                .HasForeignKey(d => d.Detail)
                .HasConstraintName("Work_fk0");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.Works)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Work_fk1");
        });

        modelBuilder.Entity<WorkList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("WorkList");

            entity.Property(e => e.Id)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Duration)
                .HasMaxLength(100)
                .HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasMaxLength(100)
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
