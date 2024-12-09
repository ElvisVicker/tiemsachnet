document.addEventListener("DOMContentLoaded", function () {
    const previewImage = document.getElementById("preview-image");

    const imageUrl = previewImage.getAttribute("data-img");
    const imageArea = document.querySelector('.img-area');

    if (imageUrl) {
        const img = document.createElement('img');
        img.src = "/Customer/images/" + imageUrl;
        imageArea.appendChild(img);
        imageArea.classList.add('active');
    }



});

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

function inputClick() {
    const inputFile = document.querySelector('#file-edit');

    inputFile.click()
}

const phieunhapForm = document.getElementById("phieunhap-form")

if (phieunhapForm) {

    phieunhapForm.addEventListener("submit", function (e) {
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
}

const selectImage = document.querySelector('.select-image');
const inputFile = document.querySelector('#file');
const imgArea = document.querySelector('.img-area');

if (selectImage) {
    selectImage.addEventListener('click', function () {
        inputFile.click();
    })

    inputFile.addEventListener('change', function () {
        const image = this.files[0]
        if (image.size < 2000000) {
            const reader = new FileReader();
            reader.onload = () => {
                const allImg = imgArea.querySelectorAll('img');
                allImg.forEach(item => item.remove());
                const imgUrl = reader.result;
                const img = document.createElement('img');
                img.src = imgUrl;
                imgArea.appendChild(img);
                imgArea.classList.add('active');
                imgArea.dataset.img = image.name;
            }
            reader.readAsDataURL(image);
        } else {
            alert("Image size more than 2MB");
        }
    })
}

// const inputFileEdit = document.querySelector('#file-edit');
// const selectImageEdit = document.querySelector('.select-image-edit');
// const imgAreaEdit = document.querySelector('.img-area-edit');
// if (selectImageEdit) {
//     console.log(inputFileEdit)
//     selectImageEdit.addEventListener('click', function () {
//         inputFileEdit.click();
//     })

//     inputFileEdit.addEventListener('change', function () {
//         const image = this.files[0]
//         if (image.size < 2000000) {
//             const reader = new FileReader();
//             reader.onload = () => {
//                 const allImg = imgAreaEdit.querySelectorAll('img');
//                 allImg.forEach(item => item.remove());
//                 const imgUrl = reader.result;
//                 const img = document.createElement('img');
//                 img.src = imgUrl;
//                 imgAreaEdit.appendChild(img);
//                 imgAreaEdit.classList.add('active');
//                 imgAreaEdit.dataset.img = image.name;
//             }
//             reader.readAsDataURL(image);
//         } else {
//             alert("Image size more than 2MB");
//         }
//     })
// }