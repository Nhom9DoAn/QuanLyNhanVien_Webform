using QLNS.Models;
using System;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/Luong")]
    public class LuongController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/Luong
        [HttpGet]
        [Route("Luong")]
        public IHttpActionResult GetLuong()
        {
            try
            {
                var luongList = db.Luongs.Select(l => new LuongModel
                {
                    MaNV = l.MaNV,
                    Thang = l.Thang,
                    Nam = l.Nam,
                    LuongCoBan = (int) l.LuongCoBan,
                    PhuCap = (int) l.PhuCap,
                    Thuong = (int) l.Thuong,
                    Phat = (int) l.Phat,
                    TienTangCa = (int) l.TienTangCa,
                    TongLuong = (int) l.TongLuong,
                    TrangThai = l.TrangThai
                }).ToList();

                return Ok(luongList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Luong/{MaNV}/{Thang}/{Nam}
        [HttpGet]
        [Route("Luong/{MaNV}/{Thang}/{Nam}")]
        public IHttpActionResult GetLuong(int MaNV, int Thang, int Nam)
        {
            try
            {
                var luong = db.Luongs
                    .Where(l => l.MaNV == MaNV && l.Thang == Thang && l.Nam == Nam)
                    .Select(l => new LuongModel
                    {
                        MaNV = l.MaNV,
                        Thang = l.Thang,
                        Nam = l.Nam,
                        LuongCoBan = (int)l.LuongCoBan,
                        PhuCap = (int) l.PhuCap,
                        Thuong = (int) l.Thuong,
                        Phat = (int) l.Phat,
                        TienTangCa = (int) l.TienTangCa,
                        TongLuong = (int) l.TongLuong,
                        TrangThai = l.TrangThai
                    })
                    .FirstOrDefault();

                if (luong == null)
                {
                    return NotFound();
                }

                return Ok(luong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Luong
        [HttpPost]
        [Route("Luong")]
        public IHttpActionResult PostLuong([FromBody] LuongModel luongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newLuong = new Luong
                {
                    MaNV = luongModel.MaNV,
                    Thang = luongModel.Thang,
                    Nam = luongModel.Nam,
                    LuongCoBan = luongModel.LuongCoBan,
                    PhuCap = luongModel.PhuCap,
                    Thuong = luongModel.Thuong,
                    Phat = luongModel.Phat,
                    TienTangCa = luongModel.TienTangCa,
                    TongLuong = luongModel.TongLuong,
                    TrangThai = luongModel.TrangThai
                };

                db.Luongs.InsertOnSubmit(newLuong);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newLuong.MaNV), newLuong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Luong/5
        [HttpPut]
        [Route("Luong/{MaNV}/{Thang}/{Nam}")]
        public IHttpActionResult PutLuong(int MaNV, int Thang, int Nam, [FromBody] LuongModel luongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (MaNV != luongModel.MaNV || Thang != luongModel.Thang || Nam != luongModel.Nam)
                {
                    return BadRequest();
                }

                var existingLuong = db.Luongs.FirstOrDefault(l => l.MaNV == MaNV && l.Thang == Thang && l.Nam == Nam);
                if (existingLuong == null)
                {
                    return NotFound();
                }

                existingLuong.LuongCoBan = luongModel.LuongCoBan;
                existingLuong.PhuCap = luongModel.PhuCap;
                existingLuong.Thuong = luongModel.Thuong;
                existingLuong.Phat = luongModel.Phat;
                existingLuong.TienTangCa = luongModel.TienTangCa;
                existingLuong.TongLuong = luongModel.TongLuong;
                existingLuong.TrangThai = luongModel.TrangThai;

                db.SubmitChanges();
                return Ok(existingLuong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Luong/5
        [HttpDelete]
        [Route("Luong/{MaNV}/{Thang}/{Nam}")]
        public IHttpActionResult DeleteLuong(int MaNV, int Thang, int Nam)
        {
            try
            {
                var luong = db.Luongs.FirstOrDefault(l => l.MaNV == MaNV && l.Thang == Thang && l.Nam == Nam);
                if (luong == null)
                {
                    return NotFound();
                }

                db.Luongs.DeleteOnSubmit(luong);
                db.SubmitChanges();

                return Ok(luong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/LichSuLuong
        [HttpPost]
        [Route("LichSuLuong")]
        public IHttpActionResult PostLichSuLuong([FromBody] LichSuLuongModel lichSuLuongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newLichSuLuong = new LichSuLuong
                {
                    MaNV = lichSuLuongModel.MaNV,
                    NgayThayDoi = lichSuLuongModel.NgayThayDoi,
                    LoaiThayDoi = lichSuLuongModel.LoaiThayDoi,
                    SoTienThayDoi = lichSuLuongModel.SoTienThayDoi,
                    LuongCB = lichSuLuongModel.LuongCB,
                    GhiChu = lichSuLuongModel.GhiChu
                };

                db.LichSuLuongs.InsertOnSubmit(newLichSuLuong);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newLichSuLuong.MaLichSuLuong), newLichSuLuong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/LichSuLuong/{MaNV}
        [HttpGet]
        [Route("LichSuLuong/{MaNV}")]
        public IHttpActionResult GetLichSuLuong(int MaNV)
        {
            try
            {
                var lichSuLuongList = db.LichSuLuongs
                    .Where(l => l.MaNV == MaNV)
                    .Select(l => new LichSuLuongModel
                    {
                        MaLichSuLuong = l.MaLichSuLuong,
                        MaNV = (int) l.MaNV,
                        NgayThayDoi = (DateTime) l.NgayThayDoi,
                        LoaiThayDoi = l.LoaiThayDoi,
                        SoTienThayDoi = (int) l.SoTienThayDoi,
                        LuongCB = (int) l.LuongCB,
                        GhiChu = l.GhiChu
                    }).ToList();

                return Ok(lichSuLuongList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
