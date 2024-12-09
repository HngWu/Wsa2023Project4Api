using System;
using System.Collections.Generic;

namespace Wsa2023Project4Api.Models;

public partial class TblTouristSpot
{
    public int Id { get; set; }

    public string? Tname { get; set; }

    public string? Address { get; set; }

    public int? MunId { get; set; }

    public string? SpotDescription { get; set; }

    public string? Picture { get; set; }

    public string? Rating { get; set; }

    public string? Entrancefee { get; set; }

    public virtual TblMunicipality? Mun { get; set; }

    public virtual ICollection<TblAmenity> TblAmenities { get; set; } = new List<TblAmenity>();
}
