using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Caching.Memory;
using InMemoryCaching.Models;
using InMemoryCaching.Services;
using System.Diagnostics;

namespace InMemoryCaching
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class DataMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IMemoryCache _MemoryCache;

        private readonly IService<Employee> _service;

        public DataMiddleware(RequestDelegate next,
            IMemoryCache memCache,
            IService<Employee> serv)
        {
            _next = next;
            _MemoryCache = memCache;
            _service = serv;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string key = "MyMemoryKey-Cache";
            List<Employee> Employees;

            //S1: We will try to get the Cache data
            //If the data is present in cache the 
            //Condition will be true else it is false 
            if (!_MemoryCache.TryGetValue(key, out Employees))
            {
                //fetch the data from the object
                Employees = _service.Get().ToList();
                //Save the received data in cache
                _MemoryCache.Set(key, Employees, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));

                Debug.WriteLine("Data is added in Cache");

            }
            else
            {
                Debug.WriteLine("Data is Retrived from in Cache");
            }

           
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DataMiddleware>();
        }
    }
}
