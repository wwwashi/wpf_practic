using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PracticTaxi.Model;

public partial class TaxiContext : DbContext
{
    public TaxiContext()
    {
    }

    public TaxiContext(DbContextOptions<TaxiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderservice> Orderservices { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Statusorder> Statusorders { get; set; }

    public virtual DbSet<Trunktype> Trunktypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Username=postgres;Password=123;Database=taxi");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Idcar).HasName("car_pkey");

            entity.ToTable("car");

            entity.Property(e => e.Idcar).HasColumnName("idcar");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Model)
                .HasColumnType("character varying")
                .HasColumnName("model");
            entity.Property(e => e.Number)
                .HasMaxLength(5)
                .HasColumnName("number");
            entity.Property(e => e.Trunkid).HasColumnName("trunkid");

            entity.HasOne(d => d.Trunk).WithMany(p => p.Cars)
                .HasForeignKey(d => d.Trunkid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("car_trunkid_fkey");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Iddriver).HasName("driver_pkey");

            entity.ToTable("driver");

            entity.Property(e => e.Iddriver).HasColumnName("iddriver");
            entity.Property(e => e.Carid).HasColumnName("carid");
            entity.Property(e => e.Usersid).HasColumnName("usersid");

            entity.HasOne(d => d.Car).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.Carid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("driver_carid_fkey");

            entity.HasOne(d => d.Users).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.Usersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("driver_usersid_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Idorders).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Idorders).HasColumnName("idorders");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Driverid).HasColumnName("driverid");
            entity.Property(e => e.Endaddress)
                .HasColumnType("character varying")
                .HasColumnName("endaddress");
            entity.Property(e => e.Payid).HasColumnName("payid");
            entity.Property(e => e.Startaddress)
                .HasColumnType("character varying")
                .HasColumnName("startaddress");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Usersid).HasColumnName("usersid");

            entity.HasOne(d => d.Driver).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Driverid)
                .HasConstraintName("orders_driverid_fkey");

            entity.HasOne(d => d.Pay).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Payid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_payid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Statusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_statusid_fkey");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Usersid)
                .HasConstraintName("orders_usersid_fkey");
        });

        modelBuilder.Entity<Orderservice>(entity =>
        {
            entity.HasKey(e => e.Idordser).HasName("orderservices_pkey");

            entity.ToTable("orderservices");

            entity.Property(e => e.Idordser).HasColumnName("idordser");
            entity.Property(e => e.Ordersid).HasColumnName("ordersid");
            entity.Property(e => e.Servicesid).HasColumnName("servicesid");

            entity.HasOne(d => d.Orders).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Ordersid)
                .HasConstraintName("orderservices_ordersid_fkey");

            entity.HasOne(d => d.Services).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Servicesid)
                .HasConstraintName("orderservices_servicesod_fkey");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.Idpay).HasName("pay_pkey");

            entity.ToTable("pay");

            entity.Property(e => e.Idpay).HasColumnName("idpay");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Idrating).HasName("rating_pkey");

            entity.ToTable("rating");

            entity.Property(e => e.Idrating).HasColumnName("idrating");
            entity.Property(e => e.Comment)
                .HasColumnType("character varying")
                .HasColumnName("comment");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Ordersid).HasColumnName("ordersid");

            entity.HasOne(d => d.Orders).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.Ordersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rating_ordersid_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrole).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Idrole).HasColumnName("idrole");
            entity.Property(e => e.Name)
                .HasMaxLength(13)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Idschedule).HasName("schedule_pkey");

            entity.ToTable("schedule");

            entity.Property(e => e.Idschedule).HasColumnName("idschedule");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Driverid).HasColumnName("driverid");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Starttime).HasColumnName("starttime");

            entity.HasOne(d => d.Driver).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Driverid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("schedule_driverid_fkey");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Idservices).HasName("services_pkey");

            entity.ToTable("services");

            entity.Property(e => e.Idservices).HasColumnName("idservices");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Statusorder>(entity =>
        {
            entity.HasKey(e => e.Idstatus).HasName("status_pkey");

            entity.ToTable("statusorders");

            entity.Property(e => e.Idstatus)
                .HasDefaultValueSql("nextval('status_idstatus_seq'::regclass)")
                .HasColumnName("idstatus");
            entity.Property(e => e.NameStatus)
                .HasMaxLength(8)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Trunktype>(entity =>
        {
            entity.HasKey(e => e.Idtrunk).HasName("trunktype_pkey");

            entity.ToTable("trunktype");

            entity.Property(e => e.Idtrunk).HasColumnName("idtrunk");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Idusers).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Idusers).HasColumnName("idusers");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Mail)
                .HasColumnType("character varying")
                .HasColumnName("mail");
            entity.Property(e => e.Midname)
                .HasMaxLength(20)
                .HasColumnName("midname");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(22)
                .HasColumnName("phone");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
