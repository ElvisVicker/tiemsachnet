using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Khachhang
{
    public long Id { get; set; }

    public long DiachiId { get; set; }

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Diachi Diachi { get; set; } = null!;

    public virtual Nguoidung? IdNavigation { get; set; } = null!;

    public virtual ICollection<Phieuxuat> Phieuxuats { get; set; } = new List<Phieuxuat>();
}
