using QLNS.Models;
using System;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/LichSuPhongBan")]
    public class LichSuPhongBanController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/LichSuPhongBan
        [HttpGet]
        public IHttpActionResult GetLichSuPhongBans()
        {
            try
            {
                var lichSuPhongBanList = db.LichSuPhongBans.Select(ls => new LichSuPhongBanModel
                {
                    MaLichSu = ls.MaLichSu,
                    MaNV = (int) ls.MaNV,
                    MaPB = (int) ls.MaPB,
                    NgayChuyen = (DateTime) ls.NgayChuyen,
                    GhiChu = ls.GhiChu
                }).ToList();

                return Ok(lichSuPhongBanList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/LichSuPhongBan/5
        [HttpGet]
        public IHttpActionResult GetLichSuPhongBan(int id)
        {
            try
            {
                var lichSuPhongBan = db.LichSuPhongBans
                    .Where(ls => ls.MaLichSu == id)
                    .Select(ls => new LichSuPhongBanModel
                    {
                        MaLichSu = ls.MaLichSu,
                        MaNV = (int) ls.MaNV,
                        MaPB = (int) ls.MaPB,
                        NgayChuyen = (DateTime) ls.NgayChuyen,
                        GhiChu = ls.GhiChu
                    })
                    .FirstOrDefault();

                if (lichSuPhongBan == null)
                {
                    return NotFound();
                }

                return Ok(lichSuPhongBan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/LichSuPhongBan
        [HttpPost]
        public IHttpActionResult PostLichSuPhongBan([FromBody] LichSuPhongBanModel lichSuPhongBanModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lichSuPhongBan = new LichSuPhongBan
                {
                    MaNV = lichSuPhongBanModel.MaNV,
                    MaPB = lichSuPhongBanModel.MaPB,
                    NgayChuyen = lichSuPhongBanModel.NgayChuyen,
                    GhiChu = lichSuPhongBanModel.GhiChu
                };

                db.LichSuPhongBans.InsertOnSubmit(lichSuPhongBan);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + lichSuPhongBan.MaLichSu), lichSuPhongBanModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/LichSuPhongBan/5
        [HttpPut]
        public IHttpActionResult PutLichSuPhongBan(int id, [FromBody] LichSuPhongBanModel lichSuPhongBanModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != lichSuPhongBanModel.MaLichSu)
                {
                    return BadRequest();
                }

                var existingLichSuPhongBan = db.LichSuPhongBans.FirstOrDefault(ls => ls.MaLichSu == id);
                if (existingLichSuPhongBan == null)
                {
                    return NotFound();
                }

                existingLichSuPhongBan.MaNV = lichSuPhongBanModel.MaNV;
                existingLichSuPhongBan.MaPB = lichSuPhongBanModel.MaPB;
                existingLichSuPhongBan.NgayChuyen = lichSuPhongBanModel.NgayChuyen;
                existingLichSuPhongBan.GhiChu = lichSuPhongBanModel.GhiChu;

                db.SubmitChanges();
                return Ok(existingLichSuPhongBan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/LichSuPhongBan/5
        [HttpDelete]
        public IHttpActionResult DeleteLichSuPhongBan(int id)
        {
            try
            {
                var lichSuPhongBan = db.LichSuPhongBans.FirstOrDefault(ls => ls.MaLichSu == id);
                if (lichSuPhongBan == null)
                {
                    return NotFound();
                }

                db.LichSuPhongBans.DeleteOnSubmit(lichSuPhongBan);
                db.SubmitChanges();

                return Ok(lichSuPhongBan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
