using System;
using System.Collections.Generic;

namespace Fims5.Models;

public partial class Tsheet
{
    public int Id { get; set; }

    public string? ProductModel { get; set; }

    public string? ProductSerial { get; set; }

    public string? Customer { get; set; }

    public string? EndUser { get; set; }

    public string? ProductType { get; set; }

    public string? SpecFile { get; set; }

    public string? Comment { get; set; }

    public string? InspectorName { get; set; }

    public DateTime InspectionStartDateTime { get; set; }

    public DateTime InspectionEndDateTime { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string? CloserName { get; set; }

    public DateTime ClosingEndDateTime { get; set; }

    public DateTime ClosingStartDateTime { get; set; }

    public virtual ICollection<Titem> Titems { get; set; } = new List<Titem>();
}
