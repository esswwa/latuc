using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class Achievement
{
    public int Idachievements { get; set; }

    public string AchievementName { get; set; } = null!;

    public string AchievementImg { get; set; } = null!;

    public string AchievementCondition { get; set; } = null!;

    public int Taken { get; set; }

    public virtual ICollection<UserAchievement> UserAchievements { get; } = new List<UserAchievement>();
}
