using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PicsController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetImage(int id)
        {
            var webRoot = _env.WebRootPath;
            var path = Path.Combine($"{webRoot}/Pics/", $"Event{id}.jpg");
            var buffer = System.IO.File.ReadAllBytes(path);
            return File(buffer, "image/jpeg");
        }
    }
}
