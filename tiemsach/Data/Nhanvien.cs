using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Nhanvien
{
    public long Id { get; set; }

    public bool Tinhtrang { get; set; }

    public string? Vitri { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Nguoidung IdNavigation { get; set; } = null!;

    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();

    public virtual ICollection<Phieuxuat> Phieuxuats { get; set; } = new List<Phieuxuat>();
}
