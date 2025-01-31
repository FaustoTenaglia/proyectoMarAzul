using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace NeoRemiseria.Models;

public partial class DbremiseriaContext : DbContext
{
    public DbremiseriaContext()
    {
    }

    public DbremiseriaContext(DbContextOptions<DbremiseriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abono> Abonos { get; set; }

    public virtual DbSet<Caja> Cajas { get; set; }

    public virtual DbSet<Cartel> Carteles { get; set; }

    public virtual DbSet<Chofer> Choferes { get; set; }

    public virtual DbSet<Cobro> Cobros { get; set; }
    public virtual DbSet<Deuda> Deudas { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Localidad> Localidades { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Movil> Moviles { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Telefono> Telefonos {get; set;}

    public virtual DbSet<VModelo> VModelos { get; set; }

    public virtual DbSet<VMovil> VMoviles { get; set; }

    public virtual DbSet<VPersona> VPersonas { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseMySql("server=localhost;port=3306;database=dbremiseria;uid=root;password=tuchaje__", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.Tipo).HasName("PRIMARY");

            entity.ToTable("abono");

            entity.Property(e => e.Tipo)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("tipo");
            entity.Property(e => e.Cuotas)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("cuotas");
            entity.Property(e => e.Importe)
                .HasPrecision(10, 2)
                .HasColumnName("importe");
        });

        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("caja");

            entity.Property(e => e.Jornada)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("jornada");
            entity.Property(e => e.Apertura).HasColumnName("apertura");
            entity.Property(e => e.Cierre).HasColumnName("cierre");
            entity.Property(e => e.Entrada)
                .HasPrecision(11,2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("entrada");
            entity.Property(e => e.Salida)
                .HasPrecision(11,2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("salida");
            entity.Property(e => e.Saldo)
                .HasPrecision(11, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("saldo");
            /*entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .HasDefaultValueSql("'A")
                .IsFixedLength()
                .HasColumnName("estado");*/
        });

        modelBuilder.Entity<Cartel>(entity =>
        {
            entity.HasKey(e => new { e.NumeroMovil, e.FechaDesde })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("cartel");

            entity.Property(e => e.NumeroMovil)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("numero_movil");
            entity.Property(e => e.FechaDesde).HasColumnName("fecha_desde");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .HasDefaultValueSql("'A'")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaHasta).HasColumnName("fecha_hasta");
            entity.Property(e => e.Observacion)
                .HasMaxLength(255)
                .HasColumnName("observacion");

            entity.HasOne(d => d.NumeroMovilNavigation).WithMany(p => p.Carteles)
                .HasForeignKey(d => d.NumeroMovil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cartel_fk_Movil");
        });

        modelBuilder.Entity<Chofer>(entity =>
        {
            // entity.HasKey(e => new { e.IdPersona, e.NumeroMovil, e.FechaDesde })
            //     .HasName("PRIMARY")
            //     .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chofer");

            entity.HasIndex(e => e.NumeroMovil, "Chofer_fk_Movil");

            entity.Property(e => e.IdPersona)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_persona");
            entity.Property(e => e.NumeroMovil)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("numero_movil");
            entity.Property(e => e.FechaDesde).HasColumnName("fecha_desde");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .HasDefaultValueSql("'A'")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaHasta).HasColumnName("fecha_hasta");
            entity.Property(e => e.Observacion)
                .HasMaxLength(255)
                .HasColumnName("observacion");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Choferes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chofer_fk_Persona");

            entity.HasOne(d => d.NumeroMovilNavigation).WithMany(p => p.Choferes)
                .HasForeignKey(d => d.NumeroMovil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Chofer_fk_Movil");
        });

        modelBuilder.Entity<Cobro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cobro");

            entity.HasIndex(e => e.NumeroMovil, "Cobro_fk_Movil");

            entity.HasIndex(e => e.IdServicio, "Cobro_fk_Servicio");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Cuotas)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("cuotas");
            entity.Property(e => e.IdServicio)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_servicio");
            entity.Property(e => e.Importe)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("importe");
            entity.Property(e => e.NumeroMovil)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("numero_movil");
            entity.Property(e => e.Periodo)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("periodo");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Cobros)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cobro_fk_Servicio");

            entity.HasOne(d => d.NumeroMovilNavigation).WithMany(p => p.Cobros)
                .HasForeignKey(d => d.NumeroMovil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cobro_fk_Movil");
        });

        modelBuilder.Entity<Deuda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("deuda");

            entity.Property(e => e.IdMovil)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_movil");

            entity.HasOne(d => d.IdMovilNavigation).WithMany(p => p.Deudas)
                .HasForeignKey(d => d.IdMovil);
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("gasto");

            entity.HasIndex(e => e.IdServicio, "Gasto_fk_Servicio");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fecha");
            entity.Property(e => e.IdServicio)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_servicio");
            entity.Property(e => e.Importe)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("importe");
            entity.Property(e => e.Numero)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("numero");

            entity.HasOne(d => d.IdServicioNavigation).WithMany()
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gasto_fk_Servicio");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("localidad");

            entity.HasIndex(e => e.IdProvincia, "Localidad_fk_Provincia");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.IdProvincia)
                .HasDefaultValueSql("'22'")
                .HasColumnType("int(10) unsigned") // Modificado a entero sin signo
                .HasColumnName("id_provincia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("Localidad_fk_Provincia");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("marca");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("modelo");

            entity.HasIndex(e => e.IdMarca, "Modelo_fk_Marca");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.IdMarca)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(19)
                .IsFixedLength()
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("Modelo_fk_Marca");
        });

        modelBuilder.Entity<Movil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("movil");

            entity.HasIndex(e => e.IdModelo, "Movil_fk_Modelo");

            entity.HasIndex(e => e.IdTitular, "Movil_fk_Persona");

            entity.HasIndex(e => e.Patente, "patente").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Año)
                .HasDefaultValueSql("(year(curdate()) - 15)")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("año");
            entity.Property(e => e.IdCartel)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_cartel");
            entity.Property(e => e.IdModelo)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_modelo");
            entity.Property(e => e.IdTitular)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_titular");
            entity.Property(e => e.Patente)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("patente");
            entity.Property(e => e.Habilitacion)
                .HasColumnType("int(10)")
                .HasColumnName("habilitacion");
            entity.Property(e => e.Estado)
                .HasColumnType("char")
                .HasColumnName("estado");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Moviles)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("Movil_fk_Modelo");

            entity.HasOne(d => d.IdTitularNavigation).WithMany(p => p.Moviles)
                .HasForeignKey(d => d.IdTitular)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Movil_fk_Persona");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("movimiento");

            entity.HasIndex(e => e.IdServicio, "Movimiento_fk_Servicio");
            entity.HasIndex(e => e.IdServicio, "Movimiento_fk_Caja");

            entity.Property(e => e.Id)
                .HasColumnType("int(10)")
                .HasColumnName("id");
            entity.Property(e => e.Tiempo)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("tiempo");
            entity.Property(e => e.IdServicio)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_servicio");
            entity.Property(e => e.Importe)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("importe");
            entity.Property(e => e.IdCaja)
                .HasColumnType("int(10)")
                .HasColumnName("id_caja");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Movimiento_fk_Servicio");

            entity.HasOne(d => d.IdCajaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdCaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Movimiento_fk_Caja");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            // entity.HasKey(e => new { e.IdCobro, e.Cuota, e.Fecha })
            //     .HasName("PRIMARY")
            //     .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pago");

            // entity.Property(e => e.IdCobro)
            //     .HasColumnType("int(10) unsigned")
            //     .HasColumnName("id_cobro");
            entity.Property(e => e.IdDeuda)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_deuda");

            // entity.Property(e => e.Cuota)
            //     .HasColumnType("int(10) unsigned")
            //     .HasColumnName("cuota");
            
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fecha");
            entity.Property(e => e.Importe)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("importe");

            // entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.Pagos)
            //     .HasForeignKey(d => d.IdCobro)
            //     .OnDelete(DeleteBehavior.ClientSetNull)
            //     .HasConstraintName("Detalle_fk_Cobro");
            entity.HasOne(d => d.IdDeudaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdDeuda);
                // .OnDelete(DeleteBehavior.ClientSetNull)
                // .HasConstraintName("Detalle_fk_Cobro");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.IdLocalidad, "Persona_fk_Localidad");

            entity.HasIndex(e => e.Dni, "dni").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .HasColumnName("apellido");
            entity.Property(e => e.Calle)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("calle");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("dni");
            entity.Property(e => e.Estado)
                .HasColumnType("char")
                .HasColumnName("estado");
            entity.Property(e => e.IdLocalidad)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id_localidad");
            entity.Property(e => e.Nacimiento).HasColumnName("nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(42)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(10)")
                .HasColumnName("numero");
            entity.Property(e => e.Sexo)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("sexo");

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("Persona_fk_Localidad");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("provincia");

            entity.Property(e => e.Id)
                //.ValueGeneratedOnAdd()
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(66)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("servicio");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("'0'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Telefono>(entity =>{
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("telefono");
            
            entity.Property(e => e.IdPersona)
                .HasColumnName("id_persona");
                
            entity.HasOne(d => d.IdPersonaNavigation).WithMany(t => t.Telefonos)
                .HasForeignKey(d => d.IdPersona);
        });

        modelBuilder.Entity<VModelo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_modelo");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Marca)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(19)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<VMovil>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_movil");

            entity.Property(e => e.Año)
                .HasDefaultValueSql("(year(curdate()) - 15)")
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("año");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("dni");
            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Marca)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(19)
                .IsFixedLength()
                .HasColumnName("nombre");
            entity.Property(e => e.Patente)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("patente");
            entity.Property(e => e.Titular)
                .HasMaxLength(30)
                .HasColumnName("titular");
        });

        modelBuilder.Entity<VPersona>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_persona");

            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .HasColumnName("apellido");
            entity.Property(e => e.Calle)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("calle");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("dni");
            entity.Property(e => e.Edad)
                .HasColumnType("int(5)")
                .HasColumnName("edad");
            entity.Property(e => e.Localidad)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("localidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(42)
                .HasDefaultValueSql("''")
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("numero");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
