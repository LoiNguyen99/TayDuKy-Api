using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TayDuKy.Services;

namespace TayDuKy.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost("images")]
        public async Task<IActionResult> uploadfile([FromForm] ImageData data)
        {
            Stream fs = data.File.OpenReadStream();
            String result =  await FileServices.getFile(fs, data.Name, data.token);

            var Image = new
            {
                imageUrl = result
            };

            return Created("/",Image);
        }


    }

    public class ImageData {
        public IFormFile File { get; set; }
        public String Name { get; set; }
        public String token { get; set; }

    }
}
