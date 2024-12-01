async function loadDashboardData() {
    const loadingText = "Đang tải...";
    document.querySelectorAll('.count').forEach(el => el.textContent = loadingText);

    try {
        const [nhanVienCount, phongBanCount, nghiViecCount, userCount] = await Promise.all([
            fetch('/api/NhanVien/Count').then(res => res.json()),
            fetch('/api/PhongBan/Count').then(res => res.json()),
            fetch('/api/NhanVien/Count/NghiViec').then(res => res.json()),
            fetch('/api/Account/Count').then(res => res.json())
        ]);

        document.querySelector('.nhanviencount').textContent = nhanVienCount;
        document.querySelector('.phongbancount').textContent = phongBanCount;
        document.querySelector('.nghivieccount').textContent = nghiViecCount;
        document.querySelector('.usercount').textContent = userCount;

    } catch (error) {
        document.querySelectorAll('.count').forEach(el => el.textContent = "Lỗi tải dữ liệu");
    }
}



document.addEventListener('DOMContentLoaded', loadDashboardData);
