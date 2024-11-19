using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Sach
{
    public long Id { get; set; }

    public string Ten { get; set; } = null!;

    public string Image { get; set; } = null!;

    public double Gianhap { get; set; }

    public double Giaxuat { get; set; }

    public string? Mota { get; set; }

    public int Soluong { get; set; }

    public bool Tinhtrang { get; set; }

    public long TacgiaId { get; set; }

    public long LoaisachId { get; set; }

    public virtual Loaisach Loaisach { get; set; } = null!;

    public virtual Tacgia Tacgia { get; set; } = null!;
}
