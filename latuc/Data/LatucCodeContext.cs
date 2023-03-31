using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace latuc.Data;

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

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseMySql("server=localhost;user=root;password=Qwerty123;database=latuc_code", ServerVersion.Parse("8.0.25-mysql"));

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

            entity.HasIndex(e => e.Options, "FK_option_idx");

            entity.Property(e => e.Idlevels)
                .ValueGeneratedNever()
                .HasColumnName("idlevels");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.AnswerPractic)
                .HasMaxLength(45)
                .HasColumnName("answer_practic");
            entity.Property(e => e.Options).HasColumnName("options");
            entity.Property(e => e.Question)
                .HasMaxLength(120)
                .HasColumnName("question");
            entity.Property(e => e.ScorePractic).HasColumnName("score_practic");
            entity.Property(e => e.ScoreTest).HasColumnName("score_test");

            entity.HasOne(d => d.OptionsNavigation).WithMany(p => p.Levels)
                .HasForeignKey(d => d.Options)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_option");
        });

        modelBuilder.Entity<LevelsStatistic>(entity =>
        {
            entity.HasKey(e => e.Idlevels).HasName("PRIMARY");

            entity.ToTable("levels_statistic");

            entity.HasIndex(e => e.Iduser, "FK_user_From_Levels_idx");

            entity.Property(e => e.Idlevels)
                .ValueGeneratedNever()
                .HasColumnName("idlevels");
            entity.Property(e => e.CountTry).HasColumnName("count_try");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.LevelComplete).HasColumnName("level_complete");
            entity.Property(e => e.ScorePractic).HasColumnName("score_practic");
            entity.Property(e => e.ScoreTest).HasColumnName("score_test");

            entity.HasOne(d => d.IdlevelsNavigation).WithOne(p => p.LevelsStatistic)
                .HasForeignKey<LevelsStatistic>(d => d.Idlevels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idLevel");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.LevelsStatistics)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_From_Levels");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Idoption).HasName("PRIMARY");

            entity.ToTable("option");

            entity.Property(e => e.Idoption)
                .ValueGeneratedNever()
                .HasColumnName("idoption");
            entity.Property(e => e.Number1)
                .HasMaxLength(45)
                .HasColumnName("number1");
            entity.Property(e => e.Number2)
                .HasMaxLength(45)
                .HasColumnName("number2");
            entity.Property(e => e.Number3)
                .HasMaxLength(45)
                .HasColumnName("number3");
            entity.Property(e => e.Number4)
                .HasMaxLength(45)
                .HasColumnName("number4");
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
