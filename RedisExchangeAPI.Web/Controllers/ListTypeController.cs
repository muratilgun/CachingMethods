using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Service;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string listKey = "names";
        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }
        public IActionResult Index()
        {
            List<string> list = new List<string>();
            if (db.KeyExists(listKey))
            {
                db.ListRange(listKey).ToList().ForEach(z =>
                {
                    list.Add(z.ToString());
                });
            }

            return View(list);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name != null)
                db.ListLeftPush(listKey, name);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            if (name != null)
                db.ListRemoveAsync(listKey, name).Wait();
            
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFirstItem()
        {
            //listenin başından siler
            db.ListLeftPop(listKey);
            //listenin sonundan siler
            //db.ListRightPop(listKey);
            return RedirectToAction("Index");

        }
    }
}
