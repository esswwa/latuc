using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class User
{
    public int Iduser { get; set; }

    public string Email { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdStatistics { get; set; }

    public int IdAchievemnts { get; set; }

    public int Role { get; set; }

    public int ExitBool { get; set; }

    public virtual UserAchievement IdAchievemntsNavigation { get; set; } = null!;

    public virtual Statistic IdStatisticsNavigation { get; set; } = null!;

    public virtual ICollection<LevelsStatistic> LevelsStatistics { get; } = new List<LevelsStatistic>();

    public virtual UserRole RoleNavigation { get; set; } = null!;
}
