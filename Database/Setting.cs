using System;
using System.Collections.Generic;

namespace TRBlog.Database;

public partial class Setting
{
    public long Id { get; set; }

    public string? ArchiveView { get; set; }

    public string? AboutSection { get; set; }
}
