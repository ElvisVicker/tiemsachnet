using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tiemsach.Data;

public partial class Nguoidung
{
    public long Id { get; set; }
    [Required(ErrorMessage = "The Quyen field is required.")]
    public long QuyenId { get; set; }

    public string Hoten { get; set; } = null!;

    public bool Gioitinh { get; set; }

    public bool Vaitro { get; set; }

    [RegularExpression(@"0[123456789]\d{8}", ErrorMessage = "Phone number invalid")]
    public string Sodienthoai { get; set; } = null!;

    public string? Diachi { get; set; }

    public bool Tinhtrang { get; set; }

    public string? Image { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Khachhang? Khachhang { get; set; }

    public virtual Nhanvien? Nhanvien { get; set; }

    public virtual Quyen Quyen { get; set; } = null!;
}
