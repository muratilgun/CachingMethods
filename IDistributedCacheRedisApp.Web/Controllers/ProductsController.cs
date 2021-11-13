using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            #region ESKİ
            //_distributedCache.SetString("name", "Murat", cacheEntryOptions);
            //await _distributedCache.SetStringAsync("surname", "Niyazi", cacheEntryOptions); 
            #endregion
            Product product = new Product { Id = 2, Name = "Kalem2", Price = 100 };
            string jsonproduct = JsonConvert.SerializeObject(product);
            await _distributedCache.SetStringAsync("product:2",jsonproduct,cacheEntryOptions);

            return View();
        }

        public async Task<IActionResult> Show()
        {
            #region ESKİ
            //string name = _distributedCache.GetString("name");
            //string surname = await _distributedCache.GetStringAsync("name");
            //ViewBag.name = name;
            //ViewBag.surname = surname; 
            #endregion
            string jsonproduct = await _distributedCache.GetStringAsync("product:1");
            Product p = JsonConvert.DeserializeObject<Product>(jsonproduct);
            ViewBag.product = p;
            return View();
        }
        public IActionResult Remove()
        {
            _distributedCache.Remove("name");
            _distributedCache.Remove("surname");
            if (_distributedCache.GetString("surname") == null)
            {
                ViewBag.mesaj = "Datalar silindi.";
                return View();
            }
            ViewBag.mesaj = "Datalar silinmedi.";
            return View();
        }
    }
}
