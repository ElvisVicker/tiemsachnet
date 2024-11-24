using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tiemsach.Data;

public partial class Sach
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Không được bỏ trống tên sách!")]
    public string Ten { get; set; } = null!;

    [Required(ErrorMessage = "Không được bỏ trống hình ảnh!")]
    public string Image { get; set; } = null!;

    [Required(ErrorMessage =  "Không được bỏ trống giá nhập!")]
    public double Gianhap { get; set; }

    public double Giaxuat { get; set; }

    public string? Mota { get; set; }

    public int Soluong { get; set; }

    public bool Tinhtrang { get; set; }

    public long TacgiaId { get; set; }

    public long LoaisachId { get; set; }

    public virtual Loaisach Loaisach { get; set; } = null!;

    public virtual Tacgia Tacgia { get; set; } = null!;
}
