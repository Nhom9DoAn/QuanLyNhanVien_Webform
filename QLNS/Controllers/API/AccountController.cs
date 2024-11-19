using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLNS.Models;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
               ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // POST: api/Account/Login
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] AccountModel account)
        {
            try
            {
                // Kiểm tra đầu vào có hợp lệ không
                if (account == null || string.IsNullOrEmpty(account.tenDN) || string.IsNullOrEmpty(account.matKhau))
                {
                    return BadRequest("Tên đăng nhập và mật khẩu không được để trống.");
                }

                // Tìm kiếm tài khoản trong database
                var existingAccount = db.TaiKhoans
                    .FirstOrDefault(a => a.TenDangNhap == account.tenDN && a.MatKhau == account.matKhau);

                if (existingAccount == null)
                {
                    return Unauthorized();
                }

                bool isAdmin = existingAccount.VaiTro == "admin"; 

                return Ok(new
                {
                    message = "Đăng nhập thành công",
                    maNV = existingAccount.MaNV,
                    tenDN = existingAccount.TenDangNhap,
                    isAdmin = isAdmin,
                    redirectTo = isAdmin ? "~/Admin/index" : "/user"
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Các phương thức khác giữ nguyên
        // GET: api/TaiKhoan
        public IHttpActionResult GetAccounts()
        {
            try
            {
                var accounts = db.TaiKhoans.Select(t => new AccountModel
                {
                    maNV = (int) t.MaNV,
                    tenDN = t.TenDangNhap,
                    matKhau = t.MatKhau
                }).ToList();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/TaiKhoan/5
        public IHttpActionResult GetMaNVAccount(int id)
        {
            try
            {
                var account = db.TaiKhoans
                    .Where(a => a.MaNV == id)
                    .Select(t => new AccountModel
                    {
                        maNV = (int) t.MaNV,
                        tenDN = t.TenDangNhap,
                        matKhau = t.MatKhau
                    })
                    .FirstOrDefault();

                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/TaiKhoan/5
        public IHttpActionResult PutAccount(int id, AccountModel account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != account.maNV)
                {
                    return BadRequest();
                }

                var existingAccount = db.TaiKhoans.FirstOrDefault(a => a.MaNV == id);
                if (existingAccount == null)
                {
                    return NotFound();
                }

                existingAccount.TenDangNhap = account.tenDN;
                existingAccount.MatKhau = account.matKhau;

                db.SubmitChanges();
                return Ok(existingAccount);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/TaiKhoan/5
        public IHttpActionResult DeleteAccount(int id)
        {
            try
            {
                var account = db.TaiKhoans.FirstOrDefault(a => a.MaNV == id);
                if (account == null)
                {
                    return NotFound();
                }

                db.TaiKhoans.DeleteOnSubmit(account);
                db.SubmitChanges();

                return Ok(account);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("Count")]
        public IHttpActionResult GetCount()
        {
            try
            {
                int count = db.TaiKhoans.Count();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}