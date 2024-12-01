using System;
using System.Linq;
using System.Web.Http;
using QLNS.Models;
using System.Configuration;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/PhongBan")]
    public class PhongBanController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
               ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/PhongBan
        public IHttpActionResult GetPhongBans()
        {
            try
            {
                var phongBans = db.PhongBans.Select(pb => new PhongBanModel
                {
                    MaPB = pb.MaPB,
                    TenPB = pb.TenPB,
                    DiaChi = pb.DiaChi,
                    SDTPB = pb.SDTPB
                }).ToList();

                return Ok(phongBans);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/PhongBan/5
        public IHttpActionResult GetPhongBan(int id)
        {
            try
            {
                var phongBan = db.PhongBans
                    .Where(pb => pb.MaPB == id)
                    .Select(pb => new PhongBanModel
                    {
                        MaPB = pb.MaPB,
                        TenPB = pb.TenPB,
                        DiaChi = pb.DiaChi,
                        SDTPB = pb.SDTPB
                    })
                    .FirstOrDefault();

                if (phongBan == null)
                {
                    return NotFound();
                }

                return Ok(phongBan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/PhongBan
        public IHttpActionResult PostPhongBan([FromBody] PhongBanModel phongBan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newPhongBan = new QLNS.Models.PhongBan
                {
                    TenPB = phongBan.TenPB,
                    DiaChi = phongBan.DiaChi,
                    SDTPB = phongBan.SDTPB
                };

                db.PhongBans.InsertOnSubmit(newPhongBan);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newPhongBan.MaPB), phongBan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/PhongBan/5
        public IHttpActionResult PutPhongBan(int id, [FromBody] PhongBanModel phongBan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != phongBan.MaPB)
                {
                    return BadRequest();
                }

                var existingPhongBan = db.PhongBans.FirstOrDefault(pb => pb.MaPB == id);
                if (existingPhongBan == null)
                {
                    return NotFound();
                }

                // Update PhongBan fields
                existingPhongBan.TenPB = phongBan.TenPB;
                existingPhongBan.DiaChi = phongBan.DiaChi;
                existingPhongBan.SDTPB = phongBan.SDTPB;

                db.SubmitChanges();
                return Ok(existingPhongBan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/PhongBan/5
        public IHttpActionResult DeletePhongBan(int id)
        {
            try
            {
                var phongBan = db.PhongBans.FirstOrDefault(pb => pb.MaPB == id);
                if (phongBan == null)
                {
                    return NotFound();
                }

                db.PhongBans.DeleteOnSubmit(phongBan);
                db.SubmitChanges();

                return Ok(phongBan);
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
                int count = db.PhongBans.Count();
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
