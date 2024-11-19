async function loadDashboardData() {
    try {
        const [nhanVienCount, phongBanCount, nghiViecCount, userCount] = await Promise.all([
            fetch('/api/NhanVien/Count').then(res => res.json()),
            fetch('/api/PhongBan/Count').then(res => res.json()),
            fetch('/api/NhanVien/Count/NghiViec').then(res => res.json()),
            fetch('/api/Account/Count').then(res => res.json()),
        ]);

        document.querySelector('.nhanviencount').textContent = nhanVienCount;
        document.querySelector('.phongbancount').textContent = phongBanCount;
        document.querySelector('.nghivieccount').textContent = nghiViecCount;
        document.querySelector('.usercount').textContent = userCount;
    } catch (error) {
        console.error('Error loading dashboard data:', error);
    }
}

document.addEventListener('DOMContentLoaded', loadDashboardData);