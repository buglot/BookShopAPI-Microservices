using AutoMapper;
using BookShop.Services.CouponAPI.Data;
using BookShop.Services.CouponAPI.Model;
using BookShop.Services.CouponAPI.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private  IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper _mapper)
        {
            this._db = db;
            this._mapper = _mapper;
            _response = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objectlist = _db.Coupons.ToList();
                
                _response.Result = objectlist;
                
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSucess = false;

            }
            return _response;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon objectlist = _db.Coupons.First(data => data.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(objectlist);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSucess = false;

            }
            return _response;
        }
        [HttpGet]
        [Route("GetCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon data = _db.Coupons.First((data) => data.CouponCode.ToLower() == code.ToLower());
                _response.Result= _mapper.Map<CouponDto>(data);
            }catch(Exception ex)
            {
                _response.IsSucess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto datapost)
        {
            try
            {
                Coupon data = _mapper.Map<Coupon>(datapost);
                _db.Coupons.Add(data);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto dataupdate)
        {
            try
            {
                Coupon data = _mapper.Map<Coupon>(dataupdate);
                _db.Coupons.Update(data);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(data);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon data = _db.Coupons.First(data => data.CouponId == id);
                _db.Coupons.Remove(data);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
