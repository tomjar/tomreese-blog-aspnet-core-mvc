using System;
using System.Collections.Generic;

namespace TRBlog.Database;

public partial class Message
{
    public long Id { get; set; }

    public string? IpAddress { get; set; }

    public string? Body { get; set; }

    public byte[]? Createtimestamp { get; set; }

    public string? Createdby { get; set; }
}
