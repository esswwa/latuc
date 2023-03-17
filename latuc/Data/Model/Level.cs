using System;
using System.Collections.Generic;

namespace latuc;

public partial class Level
{
    public int Idlevels { get; set; }

    public int Answer { get; set; }

    public string AnswerPractic { get; set; } = null!;

    public string Question { get; set; } = null!;
}
