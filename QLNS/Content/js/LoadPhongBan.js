async function loadPhongBanList() {
    const tableBody = document.getElementById('phongBanTableBody');

    try {
        const phongBanList = await fetch('/api/PhongBan').then(res => {
            if (!res.ok) throw new Error('Failed to fetch data');
            return res.json();
        });

        if (phongBanList.length === 0) {
            tableBody.innerHTML = `
                <tr>
                    <td colspan="6">Không có phòng ban nào.</td>
                </tr>`;
            return;
        }

        phongBanList.forEach(item => {
            const row = document.createElement('tr');

            row.innerHTML = `
                <td><input type="checkbox" class="rowCheckbox" data-id="${item.MaPB}" /></td>
                <td>${item.MaPB}</td>
                <td>${item.TenPB}</td>
                <td>${item.DiaChi}</td>
                <td>${item.SDTPB}</td>
                <td>
                    <a href="/PhongBan/Edit/${item.MaPB}">Edit</a> |
                    <a href="/PhongBan/Details/${item.MaPB}">Details</a> |
                    <a href="/PhongBan/Delete/${item.MaPB}" onclick="return confirm('Are you sure?');">Delete</a>
                </td>`;

            tableBody.appendChild(row);
        });

        document.getElementById('selectAll').addEventListener('change', function () {
            const isChecked = this.checked;
            document.querySelectorAll('.rowCheckbox').forEach(cb => cb.checked = isChecked);
        });
    } catch (error) {
        console.error('Error:', error);
        tableBody.innerHTML = `
            <tr>
                <td colspan="6">Lỗi khi tải danh sách phòng ban.</td>
            </tr>`;
    }
}

document.addEventListener('DOMContentLoaded', loadPhongBanList);
