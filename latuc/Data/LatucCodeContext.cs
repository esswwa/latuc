using MaterialDesignThemes.Wpf;

namespace latuc.Data.Model;
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

    public virtual DbSet<Practic> Practics { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Theory> Theories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=Qwerty123;database=latuc_code", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

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

            entity.HasIndex(e => e.Practic, "fk_practic_idpractic_idx");

            entity.HasIndex(e => e.IdTheory, "fk_theory_idTheory_idx");

            entity.Property(e => e.Idlevels)
                .ValueGeneratedNever()
                .HasColumnName("idlevels");
            entity.Property(e => e.IdTheory).HasColumnName("id_theory");
            entity.Property(e => e.LanguageLvl).HasColumnName("language_lvl");
            entity.Property(e => e.Options).HasColumnName("options");
            entity.Property(e => e.Practic).HasColumnName("practic");

            entity.Property(e => e.Theme)
                .HasMaxLength(100)
                .HasColumnName("theme");

            

            entity.HasOne(d => d.IdTheoryNavigation).WithMany(p => p.Levels)
                .HasForeignKey(d => d.IdTheory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_theory_idTheory");

            entity.HasOne(d => d.OptionsNavigation).WithMany(p => p.Levels)
                .HasForeignKey(d => d.Options)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_option");

            entity.HasOne(d => d.PracticNavigation).WithMany(p => p.Levels)
                .HasForeignKey(d => d.Practic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_practic_idpractic");
        });

        modelBuilder.Entity<LevelsStatistic>(entity =>
        {
            entity.HasKey(e => e.Idlevels).HasName("PRIMARY");

            entity.ToTable("levels_statistic");

            entity.Property(e => e.Idlevels).HasColumnName("idlevels");

            entity.HasIndex(e => e.Iduser, "FK_user_From_Levels_idx");

            entity.HasIndex(e => e.Id_level, "fk_idlvel_from_levels_to_levels");

            entity.Property(e => e.Idlevels)
                .ValueGeneratedNever()
                .HasColumnName("idlevels");
            entity.Property(e => e.CountTryPractic).HasColumnName("count_try_practic");
            entity.Property(e => e.CountTryTest).HasColumnName("count_try_test");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.LevelComplete).HasColumnName("level_complete");
            entity.Property(e => e.ScorePractic).HasColumnName("score_practic");
            entity.Property(e => e.ScoreTest).HasColumnName("score_test");
            entity.Property(e => e.ScoreTheory).HasColumnName("score_theory");
            entity.Property(e => e.Id_level).HasColumnName("id_level");
            
            entity.HasOne(d => d.IdlevelsNavigation).WithOne(p => p.LevelsStatistic)
                .HasForeignKey<LevelsStatistic>(d => d.Id_level)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idlvel_from_levels_to_levels");

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
            entity.Property(e => e.Answer)
                .HasColumnName("answer");
            entity.Property(e => e.Question)
                .HasMaxLength(250)
                .HasColumnName("question");
            entity.Property(e => e.Number1)
                .HasMaxLength(250)
                .HasColumnName("number1");
            entity.Property(e => e.Number2)
                .HasMaxLength(250)
                .HasColumnName("number2");
            entity.Property(e => e.Number3)
                .HasMaxLength(250)
                .HasColumnName("number3");
            entity.Property(e => e.Number4)
                .HasMaxLength(250)
                .HasColumnName("number4");
            entity.Property(e => e.Numberquestion)
            .HasColumnName("numberquestion");

            
        }); 

        modelBuilder.Entity<Practic>(entity =>
        {
            entity.HasKey(e => e.Idpractic).HasName("PRIMARY");

            entity.ToTable("practic");

            entity.Property(e => e.Idpractic)
                .ValueGeneratedNever()
                .HasColumnName("idpractic");
            entity.Property(e => e.Answer)
                .HasMaxLength(300)
                .HasColumnName("answer");
            entity.Property(e => e.Question)
                .HasMaxLength(300)
                .HasColumnName("question");
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

        modelBuilder.Entity<Theory>(entity =>
        {
            entity.HasKey(e => e.IdTheory).HasName("PRIMARY");

            entity.ToTable("theory");

            entity.Property(e => e.IdTheory)
                .ValueGeneratedNever()
                .HasColumnName("id_theory");
            entity.Property(e => e.Text)
                .HasColumnType("mediumtext")
                .HasColumnName("text");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.IdAchievemnts, "FK_idAchiev_From_User_idx");

            entity.HasIndex(e => e.IdStatistics, "FK_statistic_Fqrom_User_idx");

            entity.HasIndex(e => e.Role, "fk_userRole_idx");

            entity.Property(e => e.Iduser).HasColumnName("iduser");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.ExitBool).HasColumnName("exitBool");
            entity.Property(e => e.IdAchievemnts).HasColumnName("idAchievemnts");
            entity.Property(e => e.IdStatistics).HasColumnName("idStatistics");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.IdAchievemntsNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAchievemnts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_idAchiev_From_User");

            entity.HasOne(d => d.IdStatisticsNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdStatistics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_statistic_Fqrom_User");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userRole");
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

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IduserRole).HasName("PRIMARY");

            entity.ToTable("user_role");

            entity.Property(e => e.IduserRole)
                .ValueGeneratedNever()
                .HasColumnName("iduser_role");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
