async function loadChartData() {
    try {
        // Biểu đồ phòng ban
        const phongBanData = await fetch('/api/NhanVien/ThongKeTheoPhongBan').then(res => res.json());
        const phongBanLabels = phongBanData.map(item => item.TenPhongBan);
        const phongBanValues = phongBanData.map(item => item.SoLuong);

        new Chart(document.getElementById('pieChartPhongBan'), {
            type: 'pie',
            data: {
                labels: phongBanLabels,
                datasets: [{
                    data: phongBanValues,
                    backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF'],
                }]
            }
        });

        // Biểu đồ giới tính
        const gioiTinhData = await fetch('/api/NhanVien/ThongKeTheoGioiTinh').then(res => res.json());
        const gioiTinhLabels = gioiTinhData.map(item => item.GioiTinh);
        const gioiTinhValues = gioiTinhData.map(item => item.SoLuong);

        new Chart(document.getElementById('pieChartGioiTinh'), {
            type: 'pie',
            data: {
                labels: gioiTinhLabels,
                datasets: [{
                    data: gioiTinhValues,
                    backgroundColor: ['#FF6384', '#36A2EB'],
                }]
            }
        });

        const chucVuData = await fetch('/api/NhanVien/NhanVienTheoChucVu').then(res => res.json());
        const chucVuLabels = chucVuData.map(item => item.TenCV);
        const chucVuValues = chucVuData.map(item => item.SoLuong);

        // Tạo biểu đồ cột
        new Chart(document.getElementById('barChartChucVu'), {
            type: 'bar',
            data: {
                labels: chucVuLabels,
                datasets: [{
                    label: 'Số lượng nhân viên',
                    data: chucVuValues,
                    backgroundColor: '#36A2EB',
                }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        beginAtZero: true,
                    }
                }

            }
        });

        // Biểu đồ nghỉ phép (Area Chart)
        const nghiPhepData = await fetch('/api/NhanVien/NhanVienNghiPhepTheoThang').then(res => res.json());
        const nghiPhepLabels = nghiPhepData.map(item => `Tháng ${item.Thang}`);
        const nghiPhepValues = nghiPhepData.map(item => item.SoLuongNghiPhep);

        new Chart(document.getElementById('areaChartNghiPhep'), {
            type: 'line',
            data: {
                labels: nghiPhepLabels,
                datasets: [{
                    label: 'Số lượng nhân viên nghỉ phép',
                    data: nghiPhepValues,
                    fill: true,
                    borderColor: '#36A2EB',
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    tension: 1
                }]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        beginAtZero: true,
                    }
                }
            }
        });

    } catch (error) {
        console.error("Lỗi tải dữ liệu biểu đồ", error);
    }
}

document.addEventListener('DOMContentLoaded', loadChartData);
