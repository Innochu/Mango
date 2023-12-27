using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Migrations;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.CouponDto;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;

        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result =  _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet("{Id:int}")]
        public ResponseDto Get(int Id)
        {
            try
            {
               var obj = _db.Coupons.First(u => u.CouponId == Id);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message += ex.Message;
            }
            return _response;
        }

        [HttpGet("{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
               Coupon obj = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (obj == null)
                {
                    _response.Result = false;
                }
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message += ex.Message;
            }
            return _response;
        }
        
        [HttpPost]
        public ResponseDto Post([FromBody]CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
              _db.Coupons.Add(obj);
                _db.SaveChanges();
              
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message += ex.Message;
            }
            return _response;
        }
        
        [HttpPut]
        public ResponseDto Put([FromBody]CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
              _db.Coupons.Update(obj);
                _db.SaveChanges();
              
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message += ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            try
            {
                Coupon coupon = await _db.Coupons.FindAsync(id);

                if (coupon != null)
                {
                    _db.Coupons.Remove(coupon);
                    await _db.SaveChangesAsync();

                   _response.Result = new ResponseDto { IsSuccess = true };
                }
                else
                {
                    _response.Result = new ResponseDto { IsSuccess = false, Message = "Coupon not found" };
                }
            }
            catch (Exception ex)
            {
                _response.Result = new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
