using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetPractice.Presentation.Controllers.v1
{
    public class MediaController : Controller
    {
        // Add an action to download a file
        [HttpGet("/media/products/{fileName}")]
        [Authorize(Policy = "Access")]
        public IActionResult DownloadMedia(string fileName)
        {
            var filePath = Path.Combine("media/products", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // File not found  
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName); 
        }
    }
}
