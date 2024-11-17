using QLNS.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLNS.Controllers.API
{
    [RoutePrefix("api/HocVanDaoTao")]
    public class HocVanDaoTaoController : ApiController
    {
        private QuanLyNhanSuDataContext db = new QuanLyNhanSuDataContext(
               ConfigurationManager.ConnectionStrings["QL_NHANSU_UDTM"].ConnectionString);

        // GET: api/HocVanDaoTao/KhoaDaoTao
        [HttpGet]
        [Route("KhoaDaoTao")]
        public IHttpActionResult GetKhoaDaoTao()
        {
            try
            {
                var khoaDaoTaoList = db.KhoaDaoTaos.Select(k => new KhoaDaoTaoModel
                {
                    MaKhoaDaoTao = k.MaKhoaDaoTao,
                    TenKhoaHoc = k.TenKhoaHoc,
                    DonViDaoTao = k.DonViDaoTao,
                    NgayBatDau = k.NgayBatDau,
                    NgayKetThuc = k.NgayKetThuc,
                    ChiPhi = (int) k.ChiPhi,
                    TrangThai = k.TrangThai
                }).ToList();

                return Ok(khoaDaoTaoList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/HocVanDaoTao/KhoaDaoTao/5
        [HttpGet]
        [Route("KhoaDaoTao/{id}")]
        public IHttpActionResult GetKhoaDaoTao(int id)
        {
            try
            {
                var khoaDaoTao = db.KhoaDaoTaos
                    .Where(k => k.MaKhoaDaoTao == id)
                    .Select(k => new KhoaDaoTaoModel
                    {
                        MaKhoaDaoTao = k.MaKhoaDaoTao,
                        TenKhoaHoc = k.TenKhoaHoc,
                        DonViDaoTao = k.DonViDaoTao,
                        NgayBatDau = k.NgayBatDau,
                        NgayKetThuc = k.NgayKetThuc,
                        ChiPhi = (int) k.ChiPhi,
                        TrangThai = k.TrangThai
                    })
                    .FirstOrDefault();

                if (khoaDaoTao == null)
                {
                    return NotFound();
                }

                return Ok(khoaDaoTao);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/HocVanDaoTao/KhoaDaoTao
        [HttpPost]
        [Route("KhoaDaoTao")]
        public IHttpActionResult PostKhoaDaoTao([FromBody] KhoaDaoTaoModel khoaDaoTao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newKhoaDaoTao = new KhoaDaoTao
                {
                    TenKhoaHoc = khoaDaoTao.TenKhoaHoc,
                    DonViDaoTao = khoaDaoTao.DonViDaoTao,
                    NgayBatDau = khoaDaoTao.NgayBatDau,
                    NgayKetThuc = khoaDaoTao.NgayKetThuc,
                    ChiPhi = khoaDaoTao.ChiPhi,
                    TrangThai = khoaDaoTao.TrangThai
                };

                db.KhoaDaoTaos.InsertOnSubmit(newKhoaDaoTao);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newKhoaDaoTao.MaKhoaDaoTao), newKhoaDaoTao);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/HocVanDaoTao/KhoaDaoTao/5
        [HttpPut]
        [Route("KhoaDaoTao/{id}")]
        public IHttpActionResult PutKhoaDaoTao(int id, [FromBody] KhoaDaoTaoModel khoaDaoTao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != khoaDaoTao.MaKhoaDaoTao)
                {
                    return BadRequest();
                }

                var existingKhoaDaoTao = db.KhoaDaoTaos.FirstOrDefault(k => k.MaKhoaDaoTao == id);
                if (existingKhoaDaoTao == null)
                {
                    return NotFound();
                }

                existingKhoaDaoTao.TenKhoaHoc = khoaDaoTao.TenKhoaHoc;
                existingKhoaDaoTao.DonViDaoTao = khoaDaoTao.DonViDaoTao;
                existingKhoaDaoTao.NgayBatDau = khoaDaoTao.NgayBatDau;
                existingKhoaDaoTao.NgayKetThuc = khoaDaoTao.NgayKetThuc;
                existingKhoaDaoTao.ChiPhi = khoaDaoTao.ChiPhi;
                existingKhoaDaoTao.TrangThai = khoaDaoTao.TrangThai;

                db.SubmitChanges();
                return Ok(existingKhoaDaoTao);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/HocVanDaoTao/KhoaDaoTao/5
        [HttpDelete]
        [Route("KhoaDaoTao/{id}")]
        public IHttpActionResult DeleteKhoaDaoTao(int id)
        {
            try
            {
                var khoaDaoTao = db.KhoaDaoTaos.FirstOrDefault(k => k.MaKhoaDaoTao == id);
                if (khoaDaoTao == null)
                {
                    return NotFound();
                }

                db.KhoaDaoTaos.DeleteOnSubmit(khoaDaoTao);
                db.SubmitChanges();

                return Ok(khoaDaoTao);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/HocVanDaoTao/HocVanBangCap
        [HttpGet]
        [Route("HocVanBangCap")]
        public IHttpActionResult GetHocVanBangCap()
        {
            try
            {
                var hocVanBangCapList = db.HocVanBangCaps.Select(h => new HocVanBangCapModel
                {
                    MaHVBC = h.MaHVBC,
                    MaNV = (int) h.MaNV,
                    TenTruong = h.TenTruong,
                    ChuyenNganh = h.ChuyenNganh,
                    BangCap = h.BangCap,
                    NamTotNghiep = (int) h.NamTotNghiep,
                    DiemTB = (float) h.DiemTB,
                    HinhThucHoc = h.HinhThucHoc,
                    GhiChu = h.GhiChu
                }).ToList();

                return Ok(hocVanBangCapList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/HocVanDaoTao/HocVanBangCap
        [HttpPost]
        [Route("HocVanBangCap")]
        public IHttpActionResult PostHocVanBangCap([FromBody] HocVanBangCapModel hocVanBangCap)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newHocVanBangCap = new HocVanBangCap
                {
                    MaNV = hocVanBangCap.MaNV,
                    TenTruong = hocVanBangCap.TenTruong,
                    ChuyenNganh = hocVanBangCap.ChuyenNganh,
                    BangCap = hocVanBangCap.BangCap,
                    NamTotNghiep = hocVanBangCap.NamTotNghiep,
                    DiemTB = hocVanBangCap.DiemTB,
                    HinhThucHoc = hocVanBangCap.HinhThucHoc,
                    GhiChu = hocVanBangCap.GhiChu
                };

                db.HocVanBangCaps.InsertOnSubmit(newHocVanBangCap);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newHocVanBangCap.MaHVBC), newHocVanBangCap);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/HocVanDaoTao/ThamGiaDaoTao
        [HttpGet]
        [Route("ThamGiaDaoTao")]
        public IHttpActionResult GetThamGiaDaoTao()
        {
            try
            {
                var thamGiaDaoTaoList = db.ThamGiaDaoTaos.Select(t => new TGDaoTaoModel
                {
                    MaNV = t.MaNV,
                    MaKhoaDaoTao = t.MaKhoaDaoTao,
                    NgayThamGia = (DateTime)t.NgayThamGia,
                    KetQua = t.KetQua,
                    ChungChi = t.ChungChi
                }).ToList();

                return Ok(thamGiaDaoTaoList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/HocVanDaoTao/ThamGiaDaoTao
        [HttpPost]
        [Route("ThamGiaDaoTao")]
        public IHttpActionResult PostThamGiaDaoTao([FromBody] TGDaoTaoModel thamGiaDaoTao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newThamGiaDaoTao = new ThamGiaDaoTao
                {
                    MaNV = thamGiaDaoTao.MaNV,
                    MaKhoaDaoTao = thamGiaDaoTao.MaKhoaDaoTao,
                    NgayThamGia = thamGiaDaoTao.NgayThamGia,
                    KetQua = thamGiaDaoTao.KetQua,
                    ChungChi = thamGiaDaoTao.ChungChi
                };

                db.ThamGiaDaoTaos.InsertOnSubmit(newThamGiaDaoTao);
                db.SubmitChanges();

                return Created(new Uri(Request.RequestUri + "/" + newThamGiaDaoTao.MaNV + "/" + newThamGiaDaoTao.MaKhoaDaoTao), newThamGiaDaoTao);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
