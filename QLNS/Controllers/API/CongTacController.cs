using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using QLNS.Models;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/CongTac")]
    public class CongTacController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
               ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/CongTac
        [HttpGet]
        public IHttpActionResult GetCongTacs()
        {
            try
            {
                var congTacList = db.CongTacs.Select(ct => new CongTacModel
                {
                    MaCT = ct.MaCT,
                    MaNV = (int) ct.MaNV,
                    NgayBatDau = (DateTime) ct.NgayBatDau,
                    NgayKetThuc = (DateTime) ct.NgayKetThuc,
                    DiaDiem = ct.DiaDiem,
                    MucDich = ct.MucDich,
                    BieuMau = ct.BieuMau,
                    TrangThai = ct.TrangThai
                }).ToList();

                return Ok(congTacList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/CongTac/5
        [HttpGet]
        public IHttpActionResult GetCongTac(int id)
        {
            try
            {
                var congTac = db.CongTacs
                    .Where(ct => ct.MaCT == id)
                    .Select(ct => new CongTacModel
                    {
                        MaCT = ct.MaCT,
                        MaNV = (int) ct.MaNV,
                        NgayBatDau = (DateTime) ct.NgayBatDau,
                        NgayKetThuc = (DateTime) ct.NgayKetThuc,
                        DiaDiem = ct.DiaDiem,
                        MucDich = ct.MucDich,
                        BieuMau = ct.BieuMau,
                        TrangThai = ct.TrangThai
                    })
                    .FirstOrDefault();

                if (congTac == null)
                {
                    return NotFound();
                }

                return Ok(congTac);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/CongTac
        [HttpPost]
        public IHttpActionResult PostCongTac([FromBody] CongTacModel congTacModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var congTac = new CongTac
                {
                    MaNV = congTacModel.MaNV,
                    NgayBatDau = congTacModel.NgayBatDau,
                    NgayKetThuc = congTacModel.NgayKetThuc,
                    DiaDiem = congTacModel.DiaDiem,
                    MucDich = congTacModel.MucDich,
                    BieuMau = congTacModel.BieuMau,
                    TrangThai = congTacModel.TrangThai
                };

                db.CongTacs.InsertOnSubmit(congTac);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + congTac.MaCT), congTacModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/CongTac/5
        [HttpPut]
        public IHttpActionResult PutCongTac(int id, [FromBody] CongTacModel congTacModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != congTacModel.MaCT)
                {
                    return BadRequest();
                }

                var existingCongTac = db.CongTacs.FirstOrDefault(ct => ct.MaCT == id);
                if (existingCongTac == null)
                {
                    return NotFound();
                }

                existingCongTac.NgayBatDau = congTacModel.NgayBatDau;
                existingCongTac.NgayKetThuc = congTacModel.NgayKetThuc;
                existingCongTac.DiaDiem = congTacModel.DiaDiem;
                existingCongTac.MucDich = congTacModel.MucDich;
                existingCongTac.BieuMau = congTacModel.BieuMau;
                existingCongTac.TrangThai = congTacModel.TrangThai;

                db.SubmitChanges();
                return Ok(existingCongTac);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/CongTac/5
        [HttpDelete]
        public IHttpActionResult DeleteCongTac(int id)
        {
            try
            {
                var congTac = db.CongTacs.FirstOrDefault(ct => ct.MaCT == id);
                if (congTac == null)
                {
                    return NotFound();
                }

                db.CongTacs.DeleteOnSubmit(congTac);
                db.SubmitChanges();

                return Ok(congTac);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
