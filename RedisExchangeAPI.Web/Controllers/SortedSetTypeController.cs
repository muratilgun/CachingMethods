using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {

        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string listKey = "hashnames";
        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
