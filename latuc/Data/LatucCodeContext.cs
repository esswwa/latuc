using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace latuc;

public partial class LatucCodeContext : DbContext
{
    public LatucCodeContext()
    {
    }

    public LatucCodeContext(DbContextOptions<LatucCodeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<LevelsStatistic> LevelsStatistics { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Top> Tops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=MarLHF1191);database=latuc_code", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Idachievements).HasName("PRIMARY");

            entity.ToTable("achievements");

            entity.Property(e => e.Idachievements).HasColumnName("idachievements");
            entity.Property(e => e.AchievementCondition)
                .HasMaxLength(120)
                .HasColumnName("achievementCondition");
            entity.Property(e => e.AchievementImg)
                .HasMaxLength(45)
                .HasColumnName("achievementImg");
            entity.Property(e => e.AchievementName)
                .HasMaxLength(45)
                .HasColumnName("achievementName");
            entity.Property(e => e.Taken).HasColumnName("taken");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Idlevels).HasName("PRIMARY");

            entity.ToTable("levels");

            entity.Property(e => e.Idlevels)
                .ValueGeneratedNever()
                .HasColumnName("idlevels");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.AnswerPractic)
                .HasMaxLength(45)
                .HasColumnName("answer_practic");
            entity.Property(e => e.Question)
                .HasMaxLength(120)
                .HasColumnName("question");
        });

        modelBuilder.Entity<LevelsStatistic>(entity =>
        {
            entity.HasKey(e => e.Idlevels).HasName("PRIMARY");

            entity.ToTable("levels_statistic");

            entity.HasIndex(e => e.Iduser, "FK_user_From_Levels_idx");

            entity.Property(e => e.Idlevels).HasColumnName("idlevels");
            entity.Property(e => e.CountTry).HasColumnName("count_try");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.LevelsStatistics)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_From_Levels");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => e.Idstatistic).HasName("PRIMARY");

            entity.ToTable("statistic");

            entity.Property(e => e.Idstatistic).HasColumnName("idstatistic");
            entity.Property(e => e.CountOfPassedLevel).HasColumnName("count_of_passed_level");
            entity.Property(e => e.CountTry).HasColumnName("count_try");
            entity.Property(e => e.LanguageLvl).HasColumnName("language_lvl");
            entity.Property(e => e.ResultTest).HasColumnName("result_test");
            entity.Property(e => e.Score).HasColumnName("score");
        });

        modelBuilder.Entity<Top>(entity =>
        {
            entity.HasKey(e => e.Idtop).HasName("PRIMARY");

            entity.ToTable("top");

            entity.HasIndex(e => e.IdUser, "FK_user_from_top_idx");

            entity.Property(e => e.Idtop)
                .ValueGeneratedNever()
                .HasColumnName("idtop");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Reiting).HasColumnName("reiting");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Tops)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_from_top");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.IdAchievemnts, "FK_idAchiev_From_User_idx");

            entity.HasIndex(e => e.IdStatistics, "FK_statistic_Fqrom_User_idx");

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.IdAchievemnts).HasColumnName("idAchievemnts");
            entity.Property(e => e.IdStatistics).HasColumnName("idStatistics");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");

            entity.HasOne(d => d.IdAchievemntsNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAchievemnts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idAchiev_From_User");

            entity.HasOne(d => d.IdStatisticsNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdStatistics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_statistic_Fqrom_User");
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => e.IduserAchievements).HasName("PRIMARY");

            entity.ToTable("user_achievements");

            entity.HasIndex(e => e.IdAchievements, "FK_userAchievement_user_idx");

            entity.Property(e => e.IduserAchievements)
                .ValueGeneratedNever()
                .HasColumnName("iduser_achievements");
            entity.Property(e => e.IdAchievements).HasColumnName("idAchievements");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Taken).HasColumnName("taken");

            entity.HasOne(d => d.IdAchievementsNavigation).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.IdAchievements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userAchievement_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
