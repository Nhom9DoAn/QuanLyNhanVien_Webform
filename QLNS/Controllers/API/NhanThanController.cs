using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using QLNS.Models;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/NhanThan")]
    public class NhanThanController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
               ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/NhanThan
        [HttpGet]
        public IHttpActionResult GetNhanThans()
        {
            try
            {
                var nhanThanList = db.NhanThans.Select(nt => new NhanThanModel
                {
                    MaNT = nt.MaNT,
                    MaNV = (int)  nt.MaNV,
                    HoTen = nt.HoTen,
                    QuanHe = nt.QuanHe,
                    NgaySinh = (DateTime) nt.NgaySinh,
                    DienThoai = nt.DienThoai,
                    DiaChi = nt.DiaChi
                }).ToList();

                return Ok(nhanThanList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NhanThan/5
        [HttpGet]
        public IHttpActionResult GetNhanThan(int id)
        {
            try
            {
                var nhanThan = db.NhanThans
                    .Where(nt => nt.MaNT == id)
                    .Select(nt => new NhanThanModel
                    {
                        MaNT = nt.MaNT,
                        MaNV = (int) nt.MaNV,
                        HoTen = nt.HoTen,
                        QuanHe = nt.QuanHe,
                        NgaySinh = (DateTime) nt.NgaySinh,
                        DienThoai = nt.DienThoai,
                        DiaChi = nt.DiaChi
                    })
                    .FirstOrDefault();

                if (nhanThan == null)
                {
                    return NotFound();
                }
                return Ok(nhanThan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/NhanThan
        [HttpPost]
        public IHttpActionResult PostNhanThan([FromBody] NhanThanModel nhanThanModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var nhanThan = new NhanThan
                {
                    MaNV = nhanThanModel.MaNV,
                    HoTen = nhanThanModel.HoTen,
                    QuanHe = nhanThanModel.QuanHe,
                    NgaySinh = nhanThanModel.NgaySinh,
                    DienThoai = nhanThanModel.DienThoai,
                    DiaChi = nhanThanModel.DiaChi
                };

                db.NhanThans.InsertOnSubmit(nhanThan);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + nhanThan.MaNT), nhanThanModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/NhanThan/5
        [HttpPut]
        public IHttpActionResult PutNhanThan(int id, [FromBody] NhanThanModel nhanThanModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != nhanThanModel.MaNT)
                {
                    return BadRequest();
                }

                var existingNhanThan = db.NhanThans.FirstOrDefault(nt => nt.MaNT == id);
                if (existingNhanThan == null)
                {
                    return NotFound();
                }

                existingNhanThan.HoTen = nhanThanModel.HoTen;
                existingNhanThan.QuanHe = nhanThanModel.QuanHe;
                existingNhanThan.NgaySinh = nhanThanModel.NgaySinh;
                existingNhanThan.DienThoai = nhanThanModel.DienThoai;
                existingNhanThan.DiaChi = nhanThanModel.DiaChi;

                db.SubmitChanges();
                return Ok(existingNhanThan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/NhanThan/5
        [HttpDelete]
        public IHttpActionResult DeleteNhanThan(int id)
        {
            try
            {
                var nhanThan = db.NhanThans.FirstOrDefault(nt => nt.MaNT == id);
                if (nhanThan == null)
                {
                    return NotFound();
                }

                db.NhanThans.DeleteOnSubmit(nhanThan);
                db.SubmitChanges();

                return Ok(nhanThan);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
