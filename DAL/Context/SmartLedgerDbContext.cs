using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartLedger.DAL.Entities;

namespace SmartLedger.DAL.Context;

public partial class SmartLedgerDbContext : DbContext
{
    public SmartLedgerDbContext()
    {
    }

    public SmartLedgerDbContext(DbContextOptions<SmartLedgerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CashflowPrediction> CashflowPredictions { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CashflowPrediction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cashflow_predictions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.GeneratedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Org).WithMany(p => p.CashflowPredictions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("cashflow_predictions_org_id_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoices_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.AnomalyScore).HasDefaultValueSql("0");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Currency).HasDefaultValueSql("'INR'::character varying");
            entity.Property(e => e.Status).HasDefaultValueSql("'pending'::character varying");

            entity.HasOne(d => d.Org).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invoices_org_id_fkey");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Invoices).HasConstraintName("invoices_vendor_id_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organizations_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Plan).HasDefaultValueSql("'Free'::character varying");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("payments_invoice_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Role).HasDefaultValueSql("'user'::character varying");

            entity.HasOne(d => d.Org).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_org_id_fkey");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vendors_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Org).WithMany(p => p.Vendors)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vendors_org_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
