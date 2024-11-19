using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Diachi
{
    public long Id { get; set; }

    public string Ten { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public virtual ICollection<Khachhang> Khachhangs { get; set; } = new List<Khachhang>();
}
