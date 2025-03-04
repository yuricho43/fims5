using System;
using System.Collections.Generic;

namespace Fims5.Models;

public partial class Titem
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public int TestNo { get; set; }

    public string? Title { get; set; }

    public string? Unit { get; set; }

    public int Channels { get; set; }

    public string? ExpressionMode { get; set; }

    public string? Ch1Data { get; set; }

    public string? Ch2Data { get; set; }

    public string? Ch3Data { get; set; }

    public string? Ch4Data { get; set; }

    public DateTime? Ch1Time { get; set; }

    public DateTime? Ch2Time { get; set; }

    public DateTime? Ch3Time { get; set; }

    public DateTime? Ch4Time { get; set; }

    public DateTime? InspectDateTime { get; set; }

    public int TsheetId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Tsheet Tsheet { get; set; } = null!;
}
