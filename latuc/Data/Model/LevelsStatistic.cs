using System;
using System.Collections.Generic;

namespace latuc;

public partial class LevelsStatistic
{
    public int Idlevels { get; set; }

    public int Iduser { get; set; }

    public int? Score { get; set; }

    public DateOnly? Date { get; set; }

    public int? CountTry { get; set; }

    public virtual User IduserNavigation { get; set; } = null!;
}
