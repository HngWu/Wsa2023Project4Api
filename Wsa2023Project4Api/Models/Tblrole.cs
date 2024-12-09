using System;
using System.Collections.Generic;

namespace Wsa2023Project4Api.Models;

public partial class Tblrole
{
    public int Id { get; set; }

    public string? Rolename { get; set; }

    public string? RoleDescription { get; set; }

    public virtual ICollection<Tbluser> Tblusers { get; set; } = new List<Tbluser>();
}
