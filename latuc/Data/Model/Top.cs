using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class Top
{
    public int Idtop { get; set; }

    public int IdUser { get; set; }

    public int Reiting { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
