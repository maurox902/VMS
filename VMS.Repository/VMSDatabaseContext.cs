using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VMS.Entities;

namespace VMS.Repository
{
    public partial class VMSDatabaseContext : DbContext
    {
        public VMSDatabaseContext()
        {
        }

        public VMSDatabaseContext(DbContextOptions<VMSDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<Preference> Preferences { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<ReservationType> ReservationTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<SeatType> SeatTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=VMS_DB;Integrated Security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("module");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.ToTable("preferences");

                entity.Property(e => e.PreferenceId).HasColumnName("preference_id");

                entity.Property(e => e.Detail)
                    .HasMaxLength(100)
                    .HasColumnName("detail");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Preferences)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_preferences_users");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReservactionId);

                entity.ToTable("reservation");

                entity.HasIndex(e => e.ReservationDate, "UK_reservation_date")
                    .IsUnique();

                entity.Property(e => e.ReservactionId).HasColumnName("reservaction_id");

                entity.Property(e => e.Detail)
                    .HasMaxLength(200)
                    .HasColumnName("detail");

                entity.Property(e => e.ReservationDate)
                    .HasColumnType("date")
                    .HasColumnName("reservation_date");

                entity.Property(e => e.ReservationTypeId).HasColumnName("reservation_type_id");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ReservationType)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ReservationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reservation_reservation_types");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reservation_schedule");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reservation_seat");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_reservation_users");
            });

            modelBuilder.Entity<ReservationType>(entity =>
            {
                entity.ToTable("reservation_types");

                entity.Property(e => e.ReservationTypeId).HasColumnName("reservation_type_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolId);

                entity.ToTable("role");

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("seat");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.ModuleId).HasColumnName("module_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_seat_module");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_seat_types");
            });

            modelBuilder.Entity<SeatType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("seat_types");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "UK_username")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
