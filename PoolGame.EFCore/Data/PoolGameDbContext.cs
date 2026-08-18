using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PoolGame.EFCore.Models;

namespace PoolGame.EFCore.Data;

public partial class PoolGameDbContext : DbContext
{
    public PoolGameDbContext()
    {
    }

    public PoolGameDbContext(DbContextOptions<PoolGameDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<PlayerStat> PlayerStats { get; set; }

    public virtual DbSet<StatsPerTurn> StatsPerTurns { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__2AB897FD94753596");

            entity.Property(e => e.GameDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PlayerStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__PlayerSt__3A162D3E5E4AB4AA");

            entity.HasIndex(e => new { e.GameId, e.UserId }, "UQ_PlayerStats_GameUser").IsUnique();

            entity.Property(e => e.ShotsAttempted).HasDefaultValue(0);
            entity.Property(e => e.ShotsMade).HasDefaultValue(0);

            entity.HasOne(d => d.Game).WithMany(p => p.PlayerStats)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerSta__GameI__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.PlayerStats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerSta__UserI__440B1D61");
        });

        modelBuilder.Entity<StatsPerTurn>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__StatsPer__3A162D3E6F19A770");

            entity.ToTable("StatsPerTurn");

            entity.HasOne(d => d.Game).WithMany(p => p.StatsPerTurns)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__StatsPerT__GameI__44FF419A");

            entity.HasOne(d => d.Player).WithMany(p => p.StatsPerTurns)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__StatsPerT__Playe__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C308E701E");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4112CF22B").IsUnique();

            entity.Property(e => e.ProfileName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
