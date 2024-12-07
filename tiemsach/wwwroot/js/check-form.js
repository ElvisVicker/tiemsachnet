function showDeleteConfirmation(deleteUrl) {
    Swal.fire({
        title: 'Bạn có chắc chắn muốn xóa?',
        text: "Hành động này không thể hoàn tác!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Có',
        cancelButtonText: 'Hủy'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = deleteUrl;
        }
    });
}

document.getElementById("phieunhap-form").addEventListener("submit", function (e) {
    e.preventDefault();

    let gianhap = document.getElementById("Gianhap").value;
    let soluong = document.getElementById("Soluong").value;

    document.getElementById("errorGianhap").textContent = "";
    document.getElementById("errorSoluong").textContent = "";

    let isValid = true;

    if (!gianhap || parseFloat(gianhap) <= 0) {
        document.getElementById("errorGianhap").textContent = "Giá nhập không được để trống và phải lớn hơn 0.";
        isValid = false;
    }

    if (!soluong || parseInt(soluong) <= 0) {
        document.getElementById("errorSoluong").textContent = "Số lượng không được để trống và phải lớn hơn 0.";
        isValid = false;
    }

    if (isValid) {
        this.submit();
    }
});