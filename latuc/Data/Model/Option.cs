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

    public virtual ICollection<Level> Levels { get; } = new List<Level>();
}
