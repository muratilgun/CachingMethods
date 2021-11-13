using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "Murat Niyazi");
            db.StringSet("ziyaretci", 100);
            return View();
        }

        public IActionResult Show()
        {
            var value = db.StringGet("name");
            //var ziy = db.StringIncrement("ziyaretci", 10);
            //async metottan data almak istiyorsak .result kullanılır.
            var count = db.StringDecrementAsync("ziyaretci", 1).Result;
            //async metodu sadece çalıştırmak istiyorsak .wait kullanılır.
            //db.StringDecrementAsync("ziyaretci", 1).Wait();

            //var deger = db.StringGetRange("name", 0, 3);
            var deger = db.StringLength("name");
            if (value.HasValue)
            {
                ViewBag.value = value.ToString();
                //ViewBag.ziyaretci = ziy.ToString();
                ViewBag.count = count.ToString();
                ViewBag.deger = deger.ToString();
            }
            return View();
        }

    }
}
