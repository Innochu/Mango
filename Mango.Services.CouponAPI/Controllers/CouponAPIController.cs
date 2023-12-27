using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Migrations;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CouponAPIController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                return objList;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }


        [HttpGet("{Id:int}")]
        public object Get(int Id)
        {
            try
            {
               var objList = _db.Coupons.First(u => u.CouponId == Id);
                return objList;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
}
