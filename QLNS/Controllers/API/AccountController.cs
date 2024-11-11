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
    public class AccountController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
               ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM1"].ConnectionString);

        // GET: api/TaiKhoan
        public IHttpActionResult GetAccounts()
        {
            try
            {
                var accounts = db.TaiKhoans.Select(t => new Account
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
                    .Select(t => new Account
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
        public IHttpActionResult PutAccount(int id, Account account)
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
