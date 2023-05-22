using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class Option
{
    public int Idoption { get; set; }

    public string Number1 { get; set; } = null!;

    public string Number2 { get; set; } = null!;

    public string Number3 { get; set; } = null!;

    public string Number4 { get; set; } = null!;

    public int Answer { get; set; }

    public string Question { get; set; } = null!;

    public int Numberquestion { get; set; }

    public virtual ICollection<Level> Levels { get; } = new List<Level>();
}
