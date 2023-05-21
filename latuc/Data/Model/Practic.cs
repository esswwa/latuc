using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class Practic
{
    public int Idpractic { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public virtual ICollection<Level> Levels { get; } = new List<Level>();
}
