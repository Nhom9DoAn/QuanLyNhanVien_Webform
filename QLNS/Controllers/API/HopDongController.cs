using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using QLNS.Models;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/HopDong")]
    public class HopDongController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/HopDong
        [HttpGet]
        public IHttpActionResult GetHopDongs()
        {
            try
            {
                var hopDongList = db.HopDongs.Select(hd => new HopDongModel
                {
                    MaHD = hd.MaHD,
                    MaNV = (int) hd.MaNV,
                    LoaiHD = hd.LoaiHD,
                    NgayBatDau = (DateTime) hd.NgayBatDau,
                    NgayKetThuc = (DateTime) hd.NgayKetThuc,
                    BieuMau = hd.BieuMau,
                    TinhTrang = hd.TinhTrang
                }).ToList();

                return Ok(hopDongList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/HopDong/{id}
        [HttpGet]
        public IHttpActionResult GetHopDong(int id)
        {
            try
            {
                var hopDong = db.HopDongs.Where(hd => hd.MaHD == id)
                    .Select(hd => new HopDongModel
                    {
                        MaHD = hd.MaHD,
                        MaNV = (int) hd.MaNV,
                        LoaiHD = hd.LoaiHD,
                        NgayBatDau = (DateTime )hd.NgayBatDau,
                        NgayKetThuc = (DateTime) hd.NgayKetThuc,
                        BieuMau = hd.BieuMau,
                        TinhTrang = hd.TinhTrang
                    })
                    .FirstOrDefault();

                if (hopDong == null)
                {
                    return NotFound();
                }

                return Ok(hopDong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/HopDong
        [HttpPost]
        public IHttpActionResult PostHopDong([FromBody] HopDongModel hopDongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newHopDong = new HopDong
                {
                    MaNV = hopDongModel.MaNV,
                    LoaiHD = hopDongModel.LoaiHD,
                    NgayBatDau = hopDongModel.NgayBatDau,
                    NgayKetThuc = hopDongModel.NgayKetThuc,
                    BieuMau = hopDongModel.BieuMau,
                    TinhTrang = hopDongModel.TinhTrang
                };

                db.HopDongs.InsertOnSubmit(newHopDong);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newHopDong.MaHD), hopDongModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/HopDong/{id}
        [HttpPut]
        public IHttpActionResult PutHopDong(int id, [FromBody] HopDongModel hopDongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != hopDongModel.MaHD)
                {
                    return BadRequest();
                }

                var existingHopDong = db.HopDongs.FirstOrDefault(hd => hd.MaHD == id);
                if (existingHopDong == null)
                {
                    return NotFound();
                }

                // Update fields
                existingHopDong.MaNV = hopDongModel.MaNV;
                existingHopDong.LoaiHD = hopDongModel.LoaiHD;
                existingHopDong.NgayBatDau = hopDongModel.NgayBatDau;
                existingHopDong.NgayKetThuc = hopDongModel.NgayKetThuc;
                existingHopDong.BieuMau = hopDongModel.BieuMau;
                existingHopDong.TinhTrang = hopDongModel.TinhTrang;

                db.SubmitChanges();
                return Ok(existingHopDong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/HopDong/{id}
        [HttpDelete]
        public IHttpActionResult DeleteHopDong(int id)
        {
            try
            {
                var hopDong = db.HopDongs.FirstOrDefault(hd => hd.MaHD == id);
                if (hopDong == null)
                {
                    return NotFound();
                }

                db.HopDongs.DeleteOnSubmit(hopDong);
                db.SubmitChanges();

                return Ok(hopDong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
