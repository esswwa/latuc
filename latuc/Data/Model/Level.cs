using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class Level
{
    public int Idlevels { get; set; }

    public int Answer { get; set; }

    public string AnswerPractic { get; set; } = null!;

    public string Question { get; set; } = null!;

    public int ScoreTest { get; set; }

    public int ScorePractic { get; set; }

    public int Options { get; set; }

    public int LanguageLvl { get; set; }

    public int IdTheory { get; set; }

    public virtual Theory IdTheoryNavigation { get; set; } = null!;

    public virtual LevelsStatistic? LevelsStatistic { get; set; }

    public virtual Option OptionsNavigation { get; set; } = null!;
}
