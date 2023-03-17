using System;
using System.Collections.Generic;

namespace latuc;

public partial class UserAchievement
{
    public int IduserAchievements { get; set; }

    public int Taken { get; set; }

    public int IdUser { get; set; }

    public int IdAchievements { get; set; }

    public virtual Achievement IdAchievementsNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
