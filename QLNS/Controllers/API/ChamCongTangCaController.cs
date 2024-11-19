using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using QLNS.Models;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/ChamCongTangCa")]
    public class ChamCongTangCaController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
            ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/ChamCongTangCa/ChamCong
        [HttpGet]
        [Route("ChamCong")]
        public IHttpActionResult GetChamCongs()
        {
            try
            {
                var chamCongList = db.ChamCongs.Select(cc => new ChamCongModel
                {
                    MaChamCong = cc.MaChamCong,
                    MaNV = (int) cc.MaNV,
                    Ngay = (DateTime) cc.Ngay,
                    GioVao = (TimeSpan) cc.GioVao,
                    GioRa = (TimeSpan) cc.GioRa,
                    TrangThai = cc.TrangThai,
                    GhiChu = cc.GhiChu

                }).ToList();

                return Ok(chamCongList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/ChamCongTangCa/ChamCong/{id}
        [HttpGet]
        [Route("ChamCong/{id}")]
        public IHttpActionResult GetChamCong(int id)
        {
            try
            {
                var chamCong = db.ChamCongs.Where(cc => cc.MaChamCong == id)
                    .Select(cc => new ChamCongModel
                    {
                        MaChamCong = cc.MaChamCong,
                        MaNV = (int) cc.MaNV,
                        Ngay = (DateTime) cc.Ngay,
                        GioVao = (TimeSpan) cc.GioVao,
                        GioRa = (TimeSpan) cc.GioRa,
                        TrangThai = cc.TrangThai,
                        GhiChu = cc.GhiChu
                    })
                    .FirstOrDefault();

                if (chamCong == null)
                {
                    return NotFound();
                }

                return Ok(chamCong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/ChamCongTangCa/ChamCong
        [HttpPost]
        [Route("ChamCong")]
        public IHttpActionResult PostChamCong([FromBody] ChamCongModel chamCongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newChamCong = new ChamCong
                {
                    MaNV = chamCongModel.MaNV,
                    Ngay = chamCongModel.Ngay,
                    GioVao = chamCongModel.GioVao,
                    GioRa = chamCongModel.GioRa,
                    TrangThai = chamCongModel.TrangThai,
                    GhiChu = chamCongModel.GhiChu
                };

                db.ChamCongs.InsertOnSubmit(newChamCong);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newChamCong.MaChamCong), chamCongModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/ChamCongTangCa/ChamCong/{id}
        [HttpPut]
        [Route("ChamCong/{id}")]
        public IHttpActionResult PutChamCong(int id, [FromBody] ChamCongModel chamCongModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != chamCongModel.MaChamCong)
                {
                    return BadRequest();
                }

                var existingChamCong = db.ChamCongs.FirstOrDefault(cc => cc.MaChamCong == id);
                if (existingChamCong == null)
                {
                    return NotFound();
                }

                // Update fields
                existingChamCong.MaNV = chamCongModel.MaNV;
                existingChamCong.Ngay = chamCongModel.Ngay;
                existingChamCong.GioVao = chamCongModel.GioVao;
                existingChamCong.GioRa = chamCongModel.GioRa;
                existingChamCong.TrangThai = chamCongModel.TrangThai;
                existingChamCong.GhiChu = chamCongModel.GhiChu;

                db.SubmitChanges();
                return Ok(existingChamCong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/ChamCongTangCa/ChamCong/{id}
        [HttpDelete]
        [Route("ChamCong/{id}")]
        public IHttpActionResult DeleteChamCong(int id)
        {
            try
            {
                var chamCong = db.ChamCongs.FirstOrDefault(cc => cc.MaChamCong == id);
                if (chamCong == null)
                {
                    return NotFound();
                }

                db.ChamCongs.DeleteOnSubmit(chamCong);
                db.SubmitChanges();

                return Ok(chamCong);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/ChamCongTangCa/TangCa
        [HttpGet]
        [Route("TangCa")]
        public IHttpActionResult GetTangCas()
        {
            try
            {
                var tangCaList = db.TangCas.Select(tc => new TangCaModel
                {
                    MaTangCa = tc.MaTangCa,
                    MaNV = (int) tc.MaNV,
                    Ngay = (DateTime) tc.Ngay,
                    GioBatDau = (TimeSpan) tc.GioBatDau,
                    GioKetThuc = (TimeSpan) tc.GioKetThuc,
                    SoGio = (int) tc.SoGio,
                    HeSo = (int) tc.HeSo,
                    TrangThai = tc.TrangThai
                }).ToList();

                return Ok(tangCaList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/ChamCongTangCa/TangCa/{id}
        [HttpGet]
        [Route("TangCa/{id}")]
        public IHttpActionResult GetTangCa(int id)
        {
            try
            {
                var tangCa = db.TangCas.Where(tc => tc.MaTangCa == id)
                    .Select(tc => new TangCaModel
                    {
                        MaTangCa = tc.MaTangCa,
                        MaNV = (int) tc.MaNV,
                        Ngay = (DateTime) tc.Ngay,
                        GioBatDau = (TimeSpan) tc.GioBatDau,
                        GioKetThuc = (TimeSpan) tc.GioKetThuc,
                        SoGio = (int) tc.SoGio,
                        HeSo = (int) tc.HeSo,
                        TrangThai = tc.TrangThai
                    })
                    .FirstOrDefault();

                if (tangCa == null)
                {
                    return NotFound();
                }

                return Ok(tangCa);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/ChamCongTangCa/TangCa
        [HttpPost]
        [Route("TangCa")]
        public IHttpActionResult PostTangCa([FromBody] TangCaModel tangCaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newTangCa = new TangCa
                {
                    MaNV = tangCaModel.MaNV,
                    Ngay = tangCaModel.Ngay,
                    GioBatDau = tangCaModel.GioBatDau,
                    GioKetThuc = tangCaModel.GioKetThuc,
                    SoGio = tangCaModel.SoGio,
                    HeSo = tangCaModel.HeSo,
                    TrangThai = tangCaModel.TrangThai
                };

                db.TangCas.InsertOnSubmit(newTangCa);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newTangCa.MaTangCa), tangCaModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/ChamCongTangCa/TangCa/{id}
        [HttpPut]
        [Route("TangCa/{id}")]
        public IHttpActionResult PutTangCa(int id, [FromBody] TangCaModel tangCaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != tangCaModel.MaTangCa)
                {
                    return BadRequest();
                }

                var existingTangCa = db.TangCas.FirstOrDefault(tc => tc.MaTangCa == id);
                if (existingTangCa == null)
                {
                    return NotFound();
                }

                // Update fields
                existingTangCa.MaNV = tangCaModel.MaNV;
                existingTangCa.Ngay = tangCaModel.Ngay;
                existingTangCa.GioBatDau = tangCaModel.GioBatDau;
                existingTangCa.GioKetThuc = tangCaModel.GioKetThuc;
                existingTangCa.SoGio = tangCaModel.SoGio;
                existingTangCa.HeSo = tangCaModel.HeSo;
                existingTangCa.TrangThai = tangCaModel.TrangThai;

                db.SubmitChanges();
                return Ok(existingTangCa);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/ChamCongTangCa/TangCa/{id}
        [HttpDelete]
        [Route("TangCa/{id}")]
        public IHttpActionResult DeleteTangCa(int id)
        {
            try
            {
                var tangCa = db.TangCas.FirstOrDefault(tc => tc.MaTangCa == id);
                if (tangCa == null)
                {
                    return NotFound();
                }

                db.TangCas.DeleteOnSubmit(tangCa);
                db.SubmitChanges();

                return Ok(tangCa);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NghiPhepThuongPhat/NghiPhep
        [HttpGet]
        [Route("NghiPhep")]
        public IHttpActionResult GetNghiPheps()
        {
            try
            {
                var nghiPhepList = db.NghiPheps.Select(np => new NghiPhepModel
                {
                    MaNghiPhep = np.MaNghiPhep,
                    MaNV = (int) np.MaNV,
                    NgayBatDau = (DateTime) np.NgayBatDau,
                    NgayKetThuc = (DateTime) np.NgayKetThuc,
                    TongNgay = (int) np.TongNgay,
                    LyDo = np.LyDo,
                    TrangThai = np.TrangThai
                }).ToList();

                return Ok(nghiPhepList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NghiPhepThuongPhat/NghiPhep/{id}
        [HttpGet]
        [Route("NghiPhep/{id}")]
        public IHttpActionResult GetNghiPhep(int id)
        {
            try
            {
                var nghiPhep = db.NghiPheps.Where(np => np.MaNghiPhep == id)
                    .Select(np => new NghiPhepModel
                    {
                        MaNghiPhep = np.MaNghiPhep,
                        MaNV = (int) np.MaNV,
                        NgayBatDau = (DateTime) np.NgayBatDau,
                        NgayKetThuc = (DateTime) np.NgayKetThuc,
                        TongNgay = (int) np.TongNgay,
                        LyDo = np.LyDo,
                        TrangThai = np.TrangThai
                    })
                    .FirstOrDefault();

                if (nghiPhep == null)
                {
                    return NotFound();
                }

                return Ok(nghiPhep);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/NghiPhepThuongPhat/NghiPhep
        [HttpPost]
        [Route("NghiPhep")]
        public IHttpActionResult PostNghiPhep([FromBody] NghiPhepModel nghiPhepModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newNghiPhep = new NghiPhep
                {
                    MaNV = nghiPhepModel.MaNV,
                    NgayBatDau = nghiPhepModel.NgayBatDau,
                    NgayKetThuc = nghiPhepModel.NgayKetThuc,
                    TongNgay = nghiPhepModel.TongNgay,
                    LyDo = nghiPhepModel.LyDo,
                    TrangThai = nghiPhepModel.TrangThai
                };

                db.NghiPheps.InsertOnSubmit(newNghiPhep);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newNghiPhep.MaNghiPhep), nghiPhepModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/NghiPhepThuongPhat/NghiPhep/{id}
        [HttpPut]
        [Route("NghiPhep/{id}")]
        public IHttpActionResult PutNghiPhep(int id, [FromBody] NghiPhepModel nghiPhepModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != nghiPhepModel.MaNghiPhep)
                {
                    return BadRequest();
                }

                var existingNghiPhep = db.NghiPheps.FirstOrDefault(np => np.MaNghiPhep == id);
                if (existingNghiPhep == null)
                {
                    return NotFound();
                }

                // Update fields
                existingNghiPhep.MaNV = nghiPhepModel.MaNV;
                existingNghiPhep.NgayBatDau = nghiPhepModel.NgayBatDau;
                existingNghiPhep.NgayKetThuc = nghiPhepModel.NgayKetThuc;
                existingNghiPhep.TongNgay = nghiPhepModel.TongNgay;
                existingNghiPhep.LyDo = nghiPhepModel.LyDo;
                existingNghiPhep.TrangThai = nghiPhepModel.TrangThai;

                db.SubmitChanges();
                return Ok(existingNghiPhep);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/NghiPhepThuongPhat/NghiPhep/{id}
        [HttpDelete]
        [Route("NghiPhep/{id}")]
        public IHttpActionResult DeleteNghiPhep(int id)
        {
            try
            {
                var nghiPhep = db.NghiPheps.FirstOrDefault(np => np.MaNghiPhep == id);
                if (nghiPhep == null)
                {
                    return NotFound();
                }

                db.NghiPheps.DeleteOnSubmit(nghiPhep);
                db.SubmitChanges();

                return Ok(nghiPhep);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       

        // GET: api/NghiPhepThuongPhat/ThuongPhat
        [HttpGet]
        [Route("ThuongPhat")]
        public IHttpActionResult GetThuongPhats()
        {
            try
            {
                var thuongPhatList = db.ThuongPhats.Select(tp => new ThuongPhatModel
                {
                    MaTP = tp.MaTP,
                    MaNV = (int) tp.MaNV,
                    Ngay = (DateTime) tp.Ngay,
                    Loai = tp.Loai,
                    SoTien = (int) tp.SoTien,
                    LyDo = tp.LyDo,
                    TrangThai = tp.TrangThai
                }).ToList();

                return Ok(thuongPhatList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/NghiPhepThuongPhat/ThuongPhat/{id}
        [HttpGet]
        [Route("ThuongPhat/{id}")]
        public IHttpActionResult GetThuongPhat(int id)
        {
            try
            {
                var thuongPhat = db.ThuongPhats.Where(tp => tp.MaTP == id)
                    .Select(tp => new ThuongPhatModel
                    {
                        MaTP = tp.MaTP,
                        MaNV = (int) tp.MaNV,
                        Ngay = (DateTime) tp.Ngay,
                        Loai = tp.Loai,
                        SoTien = (int) tp.SoTien,
                        LyDo = tp.LyDo,
                        TrangThai = tp.TrangThai
                    })
                    .FirstOrDefault();

                if (thuongPhat == null)
                {
                    return NotFound();
                }

                return Ok(thuongPhat);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/NghiPhepThuongPhat/ThuongPhat
        [HttpPost]
        [Route("ThuongPhat")]
        public IHttpActionResult PostThuongPhat([FromBody] ThuongPhatModel thuongPhatModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newThuongPhat = new ThuongPhat
                {
                    MaNV = thuongPhatModel.MaNV,
                    Ngay = thuongPhatModel.Ngay,
                    Loai = thuongPhatModel.Loai,
                    SoTien = thuongPhatModel.SoTien,
                    LyDo = thuongPhatModel.LyDo,
                    TrangThai = thuongPhatModel.TrangThai
                };

                db.ThuongPhats.InsertOnSubmit(newThuongPhat);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newThuongPhat.MaTP), thuongPhatModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/NghiPhepThuongPhat/ThuongPhat/{id}
        [HttpPut]
        [Route("ThuongPhat/{id}")]
        public IHttpActionResult PutThuongPhat(int id, [FromBody] ThuongPhatModel thuongPhatModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != thuongPhatModel.MaTP)
                {
                    return BadRequest();
                }

                var existingThuongPhat = db.ThuongPhats.FirstOrDefault(tp => tp.MaTP == id);
                if (existingThuongPhat == null)
                {
                    return NotFound();
                }

                // Update fields
                existingThuongPhat.MaNV = thuongPhatModel.MaNV;
                existingThuongPhat.Ngay = thuongPhatModel.Ngay;
                existingThuongPhat.Loai = thuongPhatModel.Loai;
                existingThuongPhat.SoTien = thuongPhatModel.SoTien;
                existingThuongPhat.LyDo = thuongPhatModel.LyDo;
                existingThuongPhat.TrangThai = thuongPhatModel.TrangThai;

                db.SubmitChanges();
                return Ok(existingThuongPhat);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/NghiPhepThuongPhat/ThuongPhat/{id}
        [HttpDelete]
        [Route("ThuongPhat/{id}")]
        public IHttpActionResult DeleteThuongPhat(int id)
        {
            try
            {
                var thuongPhat = db.ThuongPhats.FirstOrDefault(tp => tp.MaTP == id);
                if (thuongPhat == null)
                {
                    return NotFound();
                }

                db.ThuongPhats.DeleteOnSubmit(thuongPhat);
                db.SubmitChanges();

                return Ok(thuongPhat);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
