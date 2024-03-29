﻿using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class LevelsStatistic
{
    public int Idlevels { get; set; }

    public int Iduser { get; set; }

    public int ScoreTest { get; set; }

    public DateOnly Date { get; set; }

    public int CountTryTest { get; set; }

    public int ScorePractic { get; set; }

    public int LevelComplete { get; set; }

    public int CountTryPractic { get; set; }

    public int ScoreTheory { get; set; }

    public int Id_level { get; set; }

    public virtual Level IdlevelsNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
