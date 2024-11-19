using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Chitietphieunhap
{
    public long PhieunhapId { get; set; }

    public double Gianhap { get; set; }

    public int Soluong { get; set; }

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long SachId { get; set; }

    public virtual Phieunhap Phieunhap { get; set; } = null!;

    public virtual Sach Sach { get; set; } = null!;
}
