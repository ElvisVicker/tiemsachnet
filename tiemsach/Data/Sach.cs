using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tiemsach.Data;

public partial class Sach
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Không được bỏ trốnng tên sách!")]
    [StringLength(255, ErrorMessage = "Tên sách không được quá 255 ký tự")]
    public string Ten { get; set; } = null!;


    //[Required(ErrorMessage = "Không được bỏ trống hình ảnh!")]

    [Required(ErrorMessage ="Sách phải có hình ảnh!")]
    public string Image { get; set; } = null!;
    public double Gianhap { get; set; } = 0;

    public double Giaxuat { get; set; } = 0;

    public string? Mota { get; set; }

    public int Soluong { get; set; } = 0;

    public bool Tinhtrang { get; set; }

    public long TacgiaId { get; set; }

    public long LoaisachId { get; set; }

    public virtual Loaisach Loaisach { get; set; } = null!;

    public virtual Tacgia Tacgia { get; set; } = null!;
}
