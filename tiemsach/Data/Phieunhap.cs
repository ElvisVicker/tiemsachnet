using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Phieunhap
{
    public long Id { get; set; }

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long NhanvienId { get; set; }

    public long NxbId { get; set; }

    public virtual Nhanvien Nhanvien { get; set; } = null!;

    public virtual Nxb Nxb { get; set; } = null!;
}
