using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using QLNS.Models;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/ChucVu")]
    public class ChucVuController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/ChucVu
        [HttpGet]
        public IHttpActionResult GetChucVus()
        {
            try
            {
                var chucVuList = db.ChucVus.Select(cv => new ChucVuModel
                {
                    MaCV = cv.MaCV,
                    TenCV = cv.TenCV,
                    MoTa = cv.MoTa
                }).ToList();

                return Ok(chucVuList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/ChucVu/5
        [HttpGet]
        public IHttpActionResult GetChucVu(int id)
        {
            try
            {
                var chucVu = db.ChucVus
                    .Where(cv => cv.MaCV == id)
                    .Select(cv => new ChucVuModel
                    {
                        MaCV = cv.MaCV,
                        TenCV = cv.TenCV,
                        MoTa = cv.MoTa
                    })
                    .FirstOrDefault();

                if (chucVu == null)
                {
                    return NotFound();
                }

                return Ok(chucVu);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/ChucVu
        [HttpPost]
        public IHttpActionResult PostChucVu([FromBody] ChucVuModel chucVuModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newChucVu = new ChucVu
                {
                    TenCV = chucVuModel.TenCV,
                    MoTa = chucVuModel.MoTa
                };

                db.ChucVus.InsertOnSubmit(newChucVu);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newChucVu.MaCV), chucVuModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/ChucVu/5
        [HttpPut]
        public IHttpActionResult PutChucVu(int id, [FromBody] ChucVuModel chucVuModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != chucVuModel.MaCV)
                {
                    return BadRequest();
                }

                var existingChucVu = db.ChucVus.FirstOrDefault(cv => cv.MaCV == id);
                if (existingChucVu == null)
                {
                    return NotFound();
                }

                // Update ChucVu fields
                existingChucVu.TenCV = chucVuModel.TenCV;
                existingChucVu.MoTa = chucVuModel.MoTa;

                db.SubmitChanges();
                return Ok(existingChucVu);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/ChucVu/5
        [HttpDelete]
        public IHttpActionResult DeleteChucVu(int id)
        {
            try
            {
                var chucVu = db.ChucVus.FirstOrDefault(cv => cv.MaCV == id);
                if (chucVu == null)
                {
                    return NotFound();
                }

                db.ChucVus.DeleteOnSubmit(chucVu);
                db.SubmitChanges();

                return Ok(chucVu);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
