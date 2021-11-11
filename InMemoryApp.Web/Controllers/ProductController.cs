using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            #region ESKİ
            ////1. yol
            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());

            //}
            //2. yol
            //if (!_memoryCache.TryGetValue("zaman",out string zamancache))
            //{

            //    MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            //    options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);

            //}

            #endregion
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            //beraber kullanılması best pratice
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            //options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.High;
            options.RegisterPostEvictionCallback((key, value, reason, state) => 
            {
                _memoryCache.Set("callback", $"{key} --> {value}=> sebep: {reason}");
            });
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            return View();
        }
        public IActionResult Show()
        {
            #region ESKİ
            //_memoryCache.GetOrCreate<string>("zaman",entry => 
            //{

            //    return DateTime.Now.ToString();
            //});
            //ViewBag.zaman = _memoryCache.Get<string>("zaman");

            #endregion

            _memoryCache.TryGetValue("zaman", out string zamancache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = zamancache;
            ViewBag.callback = callback;
            return View();
        }
    }
}
