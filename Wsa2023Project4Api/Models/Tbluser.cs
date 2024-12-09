using System;
using System.Collections.Generic;

namespace Wsa2023Project4Api.Models;

public partial class Tbluser
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? Roleid { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public virtual Tblrole? Role { get; set; }
}
