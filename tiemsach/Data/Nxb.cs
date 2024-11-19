using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Nxb
{
    public long Id { get; set; }

    public string Ten { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();
}
