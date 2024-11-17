function clearErrorMessages() {
    document.getElementById("errorTenDN").textContent = "";
    document.getElementById("errorMatKhau").textContent = "";
}

async function login() {
    const username = document.getElementById("TenDN").value;
    const password = document.getElementById("MatKhau").value;
    const remember = document.getElementById("remember").checked;
    const loginMessage = document.getElementById("loginMessage");

    document.getElementById("errorTenDN").textContent = "";
    document.getElementById("errorMatKhau").textContent = "";
    loginMessage.style.display = "none";

    if (!username) {
        document.getElementById("errorTenDN").textContent = "Vui lòng nhập tên đăng nhập";
        return;
    }
    if (!password) {
        document.getElementById("errorMatKhau").textContent = "Vui lòng nhập mật khẩu";
        return;
    }

    try {
        const response = await fetch('/api/Account/Login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                tenDN: username,
                matKhau: password,
                remember: remember
            })
        });

        const data = await response.json();

        if (response.ok) {
            if (remember) {
                localStorage.setItem('rememberedUser', JSON.stringify({
                    username: username,
                    password: password
                }));
            } else {
                localStorage.removeItem('rememberedUser');
            }

            localStorage.setItem('userInfo', JSON.stringify({
                maNV: data.maNV,
                tenDN: data.tenDN,
                isAdmin: data.isAdmin
            }));

            if (data.isAdmin) {
                window.location.href = '/Admin/Index';
            } else {
                window.location.href = '/Home/Index';
            }
        } else {
            loginMessage.textContent = data.message || "Tên đăng nhập hoặc mật khẩu không đúng";
            loginMessage.style.display = "block";
        }
    } catch (err) {
        console.error("Lỗi:", err);
        loginMessage.textContent = "Đã xảy ra lỗi trong quá trình đăng nhập. Vui lòng thử lại.";
        loginMessage.style.display = "block";
    }
}

document.getElementById('TenDN').addEventListener('input', clearErrorMessages);
document.getElementById('MatKhau').addEventListener('input', clearErrorMessages);

document.addEventListener('DOMContentLoaded', function () {
    const rememberedUser = localStorage.getItem('rememberedUser');
    if (rememberedUser) {
        const { username, password } = JSON.parse(rememberedUser);
        document.getElementById('TenDN').value = username;
        document.getElementById('MatKhau').value = password;
        document.getElementById('remember').checked = true;
    }
});
