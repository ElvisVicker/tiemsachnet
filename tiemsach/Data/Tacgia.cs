using System;
using System.Collections.Generic;

namespace tiemsach.Data;

public partial class Tacgia
{
    public long Id { get; set; }

    public string Ten { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int Namsinh { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
