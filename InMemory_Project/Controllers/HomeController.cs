using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMemory_Project.Models;
using InMemory_Project.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace InMemory_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectContext _db;
        private IMemoryCache _cache;
        private static ILogger<HomeController> _logger;

        public HomeController(ProjectContext db, IMemoryCache cache, ILogger<HomeController> logger)
        {
            _db = db;
            _cache = cache;
            _logger = logger;
        }

        // in memort cahche
        public IActionResult Index()
        {
            IList<User> model;
            if (!_cache.TryGetValue("users", out model))
            {
                model = _db.Users.ToList();

                var cacheEntryOption = new MemoryCacheEntryOptions()
                   .SetPriority(CacheItemPriority.NeverRemove)
                   //.SetAbsoluteExpiration(TimeSpan.FromDays(1))
                   .SetSlidingExpiration(TimeSpan.FromSeconds(3))
                   .RegisterPostEvictionCallback(UserCahceEvicated);
                _cache.Set("users", model, cacheEntryOption);
            }
            return View(model);
        }
        private static void UserCahceEvicated(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogWarning($"users cache evicated : {reason} | state: {state}");
        }

    }
}
