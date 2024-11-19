using System;
using System.Linq;
using System.Web.Http;
using QLNS.Models;
using System.Configuration;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/NhanVien")]
    public class NhanVienController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/NhanVien
        public IHttpActionResult GetNhanViens()
        {
            try
            {
                var nhanViens = db.NhanViens.Select(nv => new NhanVienModel
                {
                    MaNV = nv.MaNV,
                    MaPB = nv.MaPB ?? 0,
                    MaCV = nv.MaCV ?? 0,
                    HoTen = nv.HoTen,
                    GioiTinh = nv.GioiTinh,
                    NgaySinh = nv.NgaySinh ?? DateTime.Now,
                    CCCD = nv.CCCD,
                    Email = nv.Email,
                    DienThoai = nv.DienThoai,
                    DiaChi = nv.DiaChi,
                    NgayVaoLam = nv.NgayVaoLam ?? DateTime.Now,
                    LuongCB = nv.LuongCB ?? 0,
                    TrangThai = nv.TrangThai,
                    SoYeuLyLich = nv.SoYeuLyLich,
                    NguoiQuanLy = nv.NguoiQuanLy ?? 0,
                    Hinh = nv.Hinh != null ? nv.Hinh.ToArray() : null,
                    TinhTrangHonNhan = nv.TinhTrangHonNhan,
                    DanToc = nv.DanToc,
                    MaChamCong = nv.MaChamCong ?? 0
                }).ToList();

                return Ok(nhanViens);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NhanVien/5
        [Route("{id:int}")]
        public IHttpActionResult GetNhanVien(int id)
        {
            try
            {
                var nhanVien = db.NhanViens
                    .Where(nv => nv.MaNV == id)
                    .Select(nv => new NhanVienModel
                    {
                        MaNV = nv.MaNV,
                        MaPB = nv.MaPB ?? 0,
                        MaCV = nv.MaCV ?? 0,
                        HoTen = nv.HoTen,
                        GioiTinh = nv.GioiTinh,
                        NgaySinh = nv.NgaySinh ?? DateTime.Now,
                        CCCD = nv.CCCD,
                        Email = nv.Email,
                        DienThoai = nv.DienThoai,
                        DiaChi = nv.DiaChi,
                        NgayVaoLam = nv.NgayVaoLam ?? DateTime.Now,
                        LuongCB = nv.LuongCB ?? 0,
                        TrangThai = nv.TrangThai,
                        SoYeuLyLich = nv.SoYeuLyLich,
                        NguoiQuanLy = nv.NguoiQuanLy ?? 0,
                        Hinh = nv.Hinh != null ? nv.Hinh.ToArray() : null,
                        TinhTrangHonNhan = nv.TinhTrangHonNhan,
                        DanToc = nv.DanToc,
                        MaChamCong = nv.MaChamCong ?? 0
                    })
.FirstOrDefault();


                if (nhanVien == null)
                {
                    return NotFound();
                }

                return Ok(nhanVien);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/NhanVien
        [HttpPost]
        public IHttpActionResult PostNhanVien([FromBody] NhanVienModel nhanVienModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(nhanVienModel.HoTen))
                {
                    return BadRequest("Họ tên không được để trống");
                }

                if (nhanVienModel.MaPB <= 0)
                {
                    return BadRequest("Mã phòng ban không hợp lệ");
                }

                if (nhanVienModel.MaCV <= 0)
                {
                    return BadRequest("Mã chức vụ không hợp lệ");
                }

                if (db.NhanViens.Any(nv => nv.MaChamCong == nhanVienModel.MaChamCong))
                {
                    return BadRequest("Mã chấm công đã tồn tại");
                }

                if (!db.NhanViens.Any(nv => nv.MaNV == nhanVienModel.NguoiQuanLy))
                {
                    return BadRequest("Người quản lý không tồn tại");
                }


                var newNhanVien = new NhanVien
                {
                    MaPB = nhanVienModel.MaPB,
                    MaCV = nhanVienModel.MaCV,
                    HoTen = nhanVienModel.HoTen,
                    GioiTinh = nhanVienModel.GioiTinh,
                    NgaySinh = nhanVienModel.NgaySinh,
                    CCCD = nhanVienModel.CCCD,
                    Email = nhanVienModel.Email,
                    DienThoai = nhanVienModel.DienThoai,
                    DiaChi = nhanVienModel.DiaChi,
                    NgayVaoLam = nhanVienModel.NgayVaoLam,
                    LuongCB = nhanVienModel.LuongCB,
                    TrangThai = nhanVienModel.TrangThai,
                    SoYeuLyLich = nhanVienModel.SoYeuLyLich,
                    NguoiQuanLy = nhanVienModel.NguoiQuanLy,
                    Hinh = nhanVienModel.Hinh,
                    TinhTrangHonNhan = nhanVienModel.TinhTrangHonNhan,
                    DanToc = nhanVienModel.DanToc,
                    MaChamCong = nhanVienModel.MaChamCong
                };

                db.NhanViens.InsertOnSubmit(newNhanVien);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newNhanVien.MaNV), nhanVienModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/NhanVien/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutNhanVien(int id, [FromBody] NhanVienModel nhanVienModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingNhanVien = db.NhanViens.FirstOrDefault(nv => nv.MaNV == id);
                if (existingNhanVien == null)
                {
                    return NotFound();
                }

                // Update fields
                existingNhanVien.MaPB = nhanVienModel.MaPB;
                existingNhanVien.MaCV = nhanVienModel.MaCV;
                existingNhanVien.HoTen = nhanVienModel.HoTen;
                existingNhanVien.GioiTinh = nhanVienModel.GioiTinh;
                existingNhanVien.NgaySinh = nhanVienModel.NgaySinh;
                existingNhanVien.CCCD = nhanVienModel.CCCD;
                existingNhanVien.Email = nhanVienModel.Email;
                existingNhanVien.DienThoai = nhanVienModel.DienThoai;
                existingNhanVien.DiaChi = nhanVienModel.DiaChi;
                existingNhanVien.NgayVaoLam = nhanVienModel.NgayVaoLam;
                existingNhanVien.LuongCB = nhanVienModel.LuongCB;
                existingNhanVien.TrangThai = nhanVienModel.TrangThai;
                existingNhanVien.SoYeuLyLich = nhanVienModel.SoYeuLyLich;
                existingNhanVien.NguoiQuanLy = nhanVienModel.NguoiQuanLy;
                existingNhanVien.Hinh = nhanVienModel.Hinh;
                existingNhanVien.TinhTrangHonNhan = nhanVienModel.TinhTrangHonNhan;
                existingNhanVien.DanToc = nhanVienModel.DanToc;
                existingNhanVien.MaChamCong = nhanVienModel.MaChamCong;

                db.SubmitChanges();
                return Ok(existingNhanVien);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/NhanVien/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteNhanVien(int id)
        {
            try
            {
                var nhanVien = db.NhanViens.FirstOrDefault(nv => nv.MaNV == id);
                if (nhanVien == null)
                {
                    return NotFound();
                }

                db.NhanViens.DeleteOnSubmit(nhanVien);
                db.SubmitChanges();

                return Ok(nhanVien);
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
                int count = db.NhanViens.Count();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Count/NghiViec")]
        public IHttpActionResult GetNghiPhepCount()
        {
            try
            {
                int count = db.NhanViens.Count(nv =>
                    nv.TrangThai == "Đã nghỉ"
                );

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
