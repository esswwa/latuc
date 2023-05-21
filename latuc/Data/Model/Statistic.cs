using System;
using System.Collections.Generic;

namespace latuc.Data.Model;
public partial class Statistic
{
    public int Idstatistic { get; set; }

    public int CountOfPassedLevel { get; set; }

    public int CountTry { get; set; }

    public int ResultTest { get; set; }

    public int LanguageLvl { get; set; }

    public int Score { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
