﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tangy.Data;
using Tangy.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tangy.Controllers.API
{
    [Route("api/[controller]")]
    public class CouponAPIController : Controller
    {
        private ApplicationDbContext _db;

        public CouponAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get(double orderTotal, String couponCode = null)
        {
            //Return E: for error, S: for succes
            var rtn = "";
            if (couponCode == null)
            {
                rtn = orderTotal + ":E";
                return Ok(rtn);
            }

            var couponFromDb = _db.Coupon.Where(c => c.Name.Equals(couponCode)).FirstOrDefault();
            if (couponFromDb == null)
            {
                rtn = orderTotal + ":E";
                return Ok(rtn);
            }
            if (couponFromDb.MinimumAmount > orderTotal)
            {
                rtn = orderTotal + ":E";
                return Ok(rtn);
            }
            if (Convert.ToInt32(couponFromDb.CouponType) == (int)Coupon.ECouponType.Dollar)
            {
                orderTotal = orderTotal - couponFromDb.Discount;
                rtn = orderTotal + ":S";
                return Ok(rtn);
            }
            else
            {
                orderTotal = orderTotal - (orderTotal * couponFromDb.Discount / 100);
                rtn = orderTotal + ":S";
                return Ok(rtn);
            }
        }


    }
}