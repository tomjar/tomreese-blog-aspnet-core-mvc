using System;
using System.Collections.Generic;

namespace TRBlog.Database;

public partial class Sspw
{
    public long Id { get; set; }

    public string? Salt { get; set; }

    public string? Pw { get; set; }

    public string? Username { get; set; }

    public byte[]? Createtimestamp { get; set; }

    public byte[]? Modifytimestamp { get; set; }
}
