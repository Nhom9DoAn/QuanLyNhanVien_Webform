using QLNS.Models;
using System;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Linq;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/TuyenDungUngVien")]
    public class TuyenDungUngVienController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/TuyenDung
        [HttpGet]
        [Route("TuyenDung")]
        public IHttpActionResult GetTuyenDung()
        {
            try
            {
                var tuyenDungList = db.TuyenDungs.Select(t => new TuyenDungModel
                {
                    MaUT = t.MaUT,
                    TenViTri = t.TenViTri,
                    MaPhong = (int) t.MaPhong,
                    SoLuong = (int) t.SoLuong,
                    TrangThai = t.TrangThai,
                    NgayBatDau = (DateTime) t.NgayBatDau,
                    NgayKetThuc = (DateTime) t.NgayKetThuc,
                    YeuCau = t.YeuCau,
                    NguoiPhuTrach = (int) t.NguoiPhuTrach
                }).ToList();

                return Ok(tuyenDungList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/TuyenDung/{id}
        [HttpGet]
        [Route("TuyenDung/{id}")]
        public IHttpActionResult GetTuyenDung(int id)
        {
            try
            {
                var tuyenDung = db.TuyenDungs
                    .Where(t => t.MaUT == id)
                    .Select(t => new TuyenDungModel
                    {
                        MaUT = t.MaUT,
                        TenViTri = t.TenViTri,
                        MaPhong = (int) t.MaPhong,
                        SoLuong = (int) t.SoLuong,
                        TrangThai = t.TrangThai,
                        NgayBatDau = (DateTime) t.NgayBatDau,
                        NgayKetThuc = (DateTime) t.NgayKetThuc,
                        YeuCau = t.YeuCau,
                        NguoiPhuTrach = (int) t.NguoiPhuTrach
                    })
                    .FirstOrDefault();

                if (tuyenDung == null)
                {
                    return NotFound();
                }

                return Ok(tuyenDung);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/TuyenDung
        [HttpPost]
        [Route("TuyenDung")]
        public IHttpActionResult PostTuyenDung([FromBody] TuyenDungModel tuyenDung)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newTuyenDung = new TuyenDung
                {
                    TenViTri = tuyenDung.TenViTri,
                    MaPhong = tuyenDung.MaPhong,
                    SoLuong = tuyenDung.SoLuong,
                    TrangThai = tuyenDung.TrangThai,
                    NgayBatDau = tuyenDung.NgayBatDau,
                    NgayKetThuc = tuyenDung.NgayKetThuc,
                    YeuCau = tuyenDung.YeuCau,
                    NguoiPhuTrach = tuyenDung.NguoiPhuTrach
                };

                db.TuyenDungs.InsertOnSubmit(newTuyenDung);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newTuyenDung.MaUT), newTuyenDung);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/TuyenDung/5
        [HttpPut]
        [Route("TuyenDung/{id}")]
        public IHttpActionResult PutTuyenDung(int id, [FromBody] TuyenDungModel tuyenDung)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != tuyenDung.MaUT)
                {
                    return BadRequest();
                }

                var existingTuyenDung = db.TuyenDungs.FirstOrDefault(t => t.MaUT == id);
                if (existingTuyenDung == null)
                {
                    return NotFound();
                }

                existingTuyenDung.TenViTri = tuyenDung.TenViTri;
                existingTuyenDung.MaPhong = tuyenDung.MaPhong;
                existingTuyenDung.SoLuong = tuyenDung.SoLuong;
                existingTuyenDung.TrangThai = tuyenDung.TrangThai;
                existingTuyenDung.NgayBatDau = tuyenDung.NgayBatDau;
                existingTuyenDung.NgayKetThuc = tuyenDung.NgayKetThuc;
                existingTuyenDung.YeuCau = tuyenDung.YeuCau;
                existingTuyenDung.NguoiPhuTrach = tuyenDung.NguoiPhuTrach;

                db.SubmitChanges();
                return Ok(existingTuyenDung);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/TuyenDung/5
        [HttpDelete]
        [Route("TuyenDung/{id}")]
        public IHttpActionResult DeleteTuyenDung(int id)
        {
            try
            {
                var tuyenDung = db.TuyenDungs.FirstOrDefault(t => t.MaUT == id);
                if (tuyenDung == null)
                {
                    return NotFound();
                }

                db.TuyenDungs.DeleteOnSubmit(tuyenDung);
                db.SubmitChanges();

                return Ok(tuyenDung);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/UngVien
        [HttpPost]
        [Route("UngVien")]
        public IHttpActionResult PostUngVien([FromBody] UngVienModel ungVien)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newUngVien = new UngVien
                {
                    MaUT = ungVien.MaUT,
                    HoTen = ungVien.HoTen,
                    Email = ungVien.Email,
                    DienThoai = ungVien.DienThoai,
                    DuongDanCV = ungVien.DuongDanCV,
                    NgayUngTuyen = ungVien.NgayUngTuyen,
                    TrangThai = ungVien.TrangThai
                };

                db.UngViens.InsertOnSubmit(newUngVien);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newUngVien.MaUngVien), newUngVien);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/UngVien/5
        [HttpPut]
        [Route("UngVien/{id}")]
        public IHttpActionResult PutUngVien(int id, [FromBody] UngVienModel ungVien)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != ungVien.MaUngVien)
                {
                    return BadRequest();
                }

                var existingUngVien = db.UngViens.FirstOrDefault(u => u.MaUngVien == id);
                if (existingUngVien == null)
                {
                    return NotFound();
                }

                existingUngVien.HoTen = ungVien.HoTen;
                existingUngVien.Email = ungVien.Email;
                existingUngVien.DienThoai = ungVien.DienThoai;
                existingUngVien.DuongDanCV = ungVien.DuongDanCV;
                existingUngVien.NgayUngTuyen = ungVien.NgayUngTuyen;
                existingUngVien.TrangThai = ungVien.TrangThai;

                db.SubmitChanges();
                return Ok(existingUngVien);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/UngVien/5
        [HttpDelete]
        [Route("UngVien/{id}")]
        public IHttpActionResult DeleteUngVien(int id)
        {
            try
            {
                var ungVien = db.UngViens.FirstOrDefault(u => u.MaUngVien == id);
                if (ungVien == null)
                {
                    return NotFound();
                }

                db.UngViens.DeleteOnSubmit(ungVien);
                db.SubmitChanges();

                return Ok(ungVien);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
