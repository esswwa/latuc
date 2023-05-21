using System;
using System.Collections.Generic;

namespace latuc;

public partial class Level
{
    public int Idlevels { get; set; }

    public int Options { get; set; }

    public int LanguageLvl { get; set; }

    public int IdTheory { get; set; }

    public int Practic { get; set; }

    public virtual Theory IdTheoryNavigation { get; set; } = null!;

    public virtual LevelsStatistic? LevelsStatistic { get; set; }

    public virtual Option OptionsNavigation { get; set; } = null!;

    public virtual Practic PracticNavigation { get; set; } = null!;
}
