using System;
using System.Collections.Generic;

namespace TRBlog.Database;

public partial class Event
{
    public long Id { get; set; }

    public string? IpAddress { get; set; }

    public string? Category { get; set; }

    public string? Description { get; set; }

    public byte[]? Createtimestamp { get; set; }
}
