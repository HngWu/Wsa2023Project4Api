using System;
using System.Collections.Generic;

namespace Wsa2023Project4Api.Models;

public partial class TblMunicipality
{
    public int Id { get; set; }

    public string? MunName { get; set; }

    public byte[]? Logo { get; set; }

    public byte[]? MunMap { get; set; }

    public string? MunDescription { get; set; }

    public virtual ICollection<TblTouristSpot> TblTouristSpots { get; set; } = new List<TblTouristSpot>();
}
