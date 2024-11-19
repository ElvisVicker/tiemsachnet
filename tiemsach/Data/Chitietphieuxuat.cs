using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Chitietphieuxuat
{
    public long PhieuxuatId { get; set; }

    public double Giaxuat { get; set; }

    public int Soluong { get; set; }

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long SachId { get; set; }

    public virtual Phieuxuat Phieuxuat { get; set; } = null!;

    public virtual Sach Sach { get; set; } = null!;
}
