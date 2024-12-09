using System;
using System.Collections.Generic;

namespace Wsa2023Project4Api.Models;

public partial class TblAmenity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public byte[]? Description { get; set; }

    public int? SpotId { get; set; }

    public virtual TblTouristSpot? Spot { get; set; }
}
