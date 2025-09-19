using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TiendaPuntoVenta.Models;

public partial class StoreDbContext : DbContext
{
    public StoreDbContext()
    {
    }

    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<TblPedido> TblPedidos { get; set; }

    public virtual DbSet<TblProducto> TblProductos { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:StoreConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasIndex(e => e.BrandId, "IX_Beers_BrandId");

            entity.Property(e => e.Alcohol).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Beers).HasForeignKey(d => d.BrandId);
        });

        modelBuilder.Entity<TblPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblPedid__3214EC075B677010");

            entity.ToTable("tblPedidos");
        });

        modelBuilder.Entity<TblProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblProdu__3214EC07BE6CBD7E");

            entity.ToTable("tblProductos");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.FechaCaptura)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Imagen)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUsuar__3214EC07F3E04179");

            entity.ToTable("tblUsuarios");

            entity.Property(e => e.Administrador)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.FechaCaptura)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Usuario)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
