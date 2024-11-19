using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Quyen
{
    public long Id { get; set; }

    public string Ten { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool Cnnguoidung { get; set; }

    public bool Cntacgia { get; set; }

    public bool Cnloaisach { get; set; }

    public bool Cnsach { get; set; }

    public bool Cnquyen { get; set; }

    public bool Cnnhap { get; set; }

    public bool Cnxuat { get; set; }

    public bool Cnnxb { get; set; }

    public virtual ICollection<Nguoidung> Nguoidungs { get; set; } = new List<Nguoidung>();
}
