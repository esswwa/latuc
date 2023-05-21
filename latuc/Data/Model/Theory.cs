using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class Theory
{
    public int IdTheory { get; set; }

    public string Text { get; set; } = null!;

    public virtual ICollection<Level> Levels { get; } = new List<Level>();
}
