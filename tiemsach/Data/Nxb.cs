using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tiemsach.Data;

public partial class Nxb
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Không được bỏ trốg tên nhà xuất bản!")]
    [StringLength(255,ErrorMessage ="Tên nhà xuất bản không được quá 255 ký tự!")]
    public string Ten { get; set; } = null!;

    [Required(ErrorMessage = "Không được bỏ trống địa chỉ!")]
    [StringLength(255, ErrorMessage = "Địa chỉ nhà xuất bản không được quá 255 ký tự!")]
    public string Diachi { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();
}
