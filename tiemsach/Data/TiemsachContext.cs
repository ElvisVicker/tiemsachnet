using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tiemsach.Data;

public partial class TiemsachContext : DbContext
{
    public TiemsachContext()
    {
    }

    public TiemsachContext(DbContextOptions<TiemsachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietphieunhap> Chitietphieunhaps { get; set; }

    public virtual DbSet<Chitietphieuxuat> Chitietphieuxuats { get; set; }

    public virtual DbSet<Diachi> Diachis { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaisach> Loaisaches { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Nxb> Nxbs { get; set; }

    public virtual DbSet<Phieunhap> Phieunhaps { get; set; }

    public virtual DbSet<Phieuxuat> Phieuxuats { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<Tacgia> Tacgia { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=EV\\SQLEXPRESS;Initial Catalog=tiemsach;Integrated Security=True;Trust Server Certificate=True");








    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietphieunhap>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_chitietphieunhap");

            entity.ToTable("chitietphieunhap");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Gianhap).HasColumnName("gianhap");
            entity.Property(e => e.PhieunhapId).HasColumnName("phieunhap_id");
            entity.Property(e => e.SachId).HasColumnName("sach_id");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Phieunhap).WithMany()
                .HasForeignKey(d => d.PhieunhapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chitietphieunhap_phieunhap");

            entity.HasOne(d => d.Sach).WithMany()
                .HasForeignKey(d => d.SachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chitietphieunhap_sach");
        });



		modelBuilder.Entity<Chitietphieuxuat>(entity =>
		{
			// Define a composite key
			entity.HasKey(e => new { e.PhieuxuatId, e.SachId })
				.HasName("PK_chitietphieuxuat");

			entity.ToTable("chitietphieuxuat");

			entity.Property(e => e.CreatedAt)
				.HasColumnType("datetime")
				.HasColumnName("created_at");
			entity.Property(e => e.DeletedAt)
				.HasColumnType("datetime")
				.HasColumnName("deleted_at");
			entity.Property(e => e.Giaxuat).HasColumnName("giaxuat");
			entity.Property(e => e.PhieuxuatId).HasColumnName("phieuxuat_id");
			entity.Property(e => e.SachId).HasColumnName("sach_id");
			entity.Property(e => e.Soluong).HasColumnName("soluong");
			entity.Property(e => e.Tinhtrang)
				.HasDefaultValue(true)
				.HasColumnName("tinhtrang");
			entity.Property(e => e.UpdatedAt)
				.HasColumnType("datetime")
				.HasColumnName("updated_at");

			// Define the relationship with Phieuxuat
			entity.HasOne(d => d.Phieuxuat)
				.WithMany(p => p.Chitietphieuxuat) // Assuming you have a collection in Phieuxuat
				.HasForeignKey(d => d.PhieuxuatId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_chitietphieuxuat_phieuxuat");

			// Define the relationship with Sach
			entity.HasOne(d => d.Sach)
				.WithMany()
				.HasForeignKey(d => d.SachId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_chitietphieuxuat_sach");
		});



		//modelBuilder.Entity<Chitietphieuxuat>(entity =>
  //      {
  //          entity
  //              .HasNoKey()
  //              .ToTable("chitietphieuxuat");

  //          entity.Property(e => e.CreatedAt)
  //              .HasColumnType("datetime")
  //              .HasColumnName("created_at");
  //          entity.Property(e => e.DeletedAt)
  //              .HasColumnType("datetime")
  //              .HasColumnName("deleted_at");
  //          entity.Property(e => e.Giaxuat).HasColumnName("giaxuat");
  //          entity.Property(e => e.PhieuxuatId).HasColumnName("phieuxuat_id");
  //          entity.Property(e => e.SachId).HasColumnName("sach_id");
  //          entity.Property(e => e.Soluong).HasColumnName("soluong");
  //          entity.Property(e => e.Tinhtrang)
  //              .HasDefaultValue(true)
  //              .HasColumnName("tinhtrang");
  //          entity.Property(e => e.UpdatedAt)
  //              .HasColumnType("datetime")
  //              .HasColumnName("updated_at");

  //          entity.HasOne(d => d.Phieuxuat).WithMany()
  //              .HasForeignKey(d => d.PhieuxuatId)
  //              .OnDelete(DeleteBehavior.ClientSetNull)
  //              .HasConstraintName("FK_chitietphieuxuat_phieuxuat");

  //          entity.HasOne(d => d.Sach).WithMany()
  //              .HasForeignKey(d => d.SachId)
  //              .OnDelete(DeleteBehavior.ClientSetNull)
  //              .HasConstraintName("FK_chitietphieuxuat_sach");
  //      });

        modelBuilder.Entity<Diachi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__diachi__3213E83F870B429E");

            entity.ToTable("diachi");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Tinhtrang).HasColumnName("tinhtrang");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.ToTable("khachhang");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.DiachiId).HasColumnName("diachi_id");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Diachi).WithMany(p => p.Khachhangs)
                .HasForeignKey(d => d.DiachiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_khachhang_diachi");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Khachhang)
                .HasForeignKey<Khachhang>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_khachhang_nguoidung");
        });

        modelBuilder.Entity<Loaisach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__loaisach__3213E83FC381F05B");

            entity.ToTable("loaisach");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nguoidun__3213E83F3BCFE33A");

            entity.ToTable("nguoidung");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("diachi");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gioitinh).HasColumnName("gioitinh");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasColumnName("hoten");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.QuyenId).HasColumnName("quyen_id");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(255)
                .HasColumnName("sodienthoai");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Vaitro).HasColumnName("vaitro");

            entity.HasOne(d => d.Quyen).WithMany(p => p.Nguoidungs)
                .HasForeignKey(d => d.QuyenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nguoidung_quyen");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.ToTable("nhanvien");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Vitri)
                .HasMaxLength(255)
                .HasColumnName("vitri");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Nhanvien)
                .HasForeignKey<Nhanvien>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nhanvien_nguoidung");
        });

        modelBuilder.Entity<Nxb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nxb__3213E83F872B580B");

            entity.ToTable("nxb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("diachi");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Tinhtrang).HasColumnName("tinhtrang");
        });

        modelBuilder.Entity<Phieunhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__phieunha__3213E83FFA37F8FE");

            entity.ToTable("phieunhap");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.NhanvienId).HasColumnName("nhanvien_id");
            entity.Property(e => e.NxbId).HasColumnName("nxb_id");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.Phieunhaps)
                .HasForeignKey(d => d.NhanvienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_phieunhap_nhanvien");

            entity.HasOne(d => d.Nxb).WithMany(p => p.Phieunhaps)
                .HasForeignKey(d => d.NxbId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_phieunhap_nxb");
        });

        modelBuilder.Entity<Phieuxuat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__phieuxua__3213E83F71566D45");

            entity.ToTable("phieuxuat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.KhachhangId).HasColumnName("khachhang_id");
            entity.Property(e => e.NhanvienId).HasColumnName("nhanvien_id");
            entity.Property(e => e.Tendiachi)
                .HasMaxLength(255)
                .HasColumnName("tendiachi");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Khachhang).WithMany(p => p.Phieuxuats)
                .HasForeignKey(d => d.KhachhangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_phieuxuat_khachhang");

            entity.HasOne(d => d.Nhanvien).WithMany(p => p.Phieuxuats)
                .HasForeignKey(d => d.NhanvienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_phieuxuat_nhanvien");
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__quyen__3213E83F1235F2D2");

            entity.ToTable("quyen");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cnloaisach).HasColumnName("cnloaisach");
            entity.Property(e => e.Cnnguoidung).HasColumnName("cnnguoidung");
            entity.Property(e => e.Cnnhap).HasColumnName("cnnhap");
            entity.Property(e => e.Cnnxb).HasColumnName("cnnxb");
            entity.Property(e => e.Cnquyen).HasColumnName("cnquyen");
            entity.Property(e => e.Cnsach).HasColumnName("cnsach");
            entity.Property(e => e.Cntacgia).HasColumnName("cntacgia");
            entity.Property(e => e.Cnxuat).HasColumnName("cnxuat");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sach__3213E83F9390006E");

            entity.ToTable("sach");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gianhap).HasColumnName("gianhap");
            entity.Property(e => e.Giaxuat).HasColumnName("giaxuat");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.LoaisachId).HasColumnName("loaisach_id");
            entity.Property(e => e.Mota).HasColumnName("mota");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.TacgiaId).HasColumnName("tacgia_id");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");

            entity.HasOne(d => d.Loaisach).WithMany(p => p.Saches)
                .HasForeignKey(d => d.LoaisachId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sach_loaisach");

            entity.HasOne(d => d.Tacgia).WithMany(p => p.Saches)
                .HasForeignKey(d => d.TacgiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sach_tacgia");
        });

        modelBuilder.Entity<Tacgia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tacgia__3213E83F765AD05F");

            entity.ToTable("tacgia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Namsinh).HasColumnName("namsinh");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasColumnName("ten");
            entity.Property(e => e.Tinhtrang)
                .HasDefaultValue(true)
                .HasColumnName("tinhtrang");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
