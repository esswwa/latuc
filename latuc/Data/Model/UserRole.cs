using System;
using System.Collections.Generic;

namespace latuc.Data.Model;

public partial class UserRole
{
    public int IduserRole { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
