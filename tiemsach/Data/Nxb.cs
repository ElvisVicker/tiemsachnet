using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tiemsach.Data;

public partial class Nxb
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Không được bỏ trốg tên nhà xuất bản")]
    public string Ten { get; set; } = null!;

    [Required(ErrorMessage = "Không được bỏ trống địa chỉ")]
    public string Diachi { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();
}
