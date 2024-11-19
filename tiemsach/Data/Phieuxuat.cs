using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Phieuxuat
{
    public long Id { get; set; }

    public long KhachhangId { get; set; }

    public string? Tendiachi { get; set; }

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long NhanvienId { get; set; }

    public virtual Khachhang Khachhang { get; set; } = null!;

    public virtual Nhanvien Nhanvien { get; set; } = null!;
}
