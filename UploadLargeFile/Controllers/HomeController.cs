using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UploadLargeFile.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace UploadLargeFile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 70000)]
        [RequestSizeLimit(70000)]
        public IActionResult Upload(IFormFile file, [FromServices] IHostingEnvironment oHostingEnvironment)
        {
            string fileName = $"{oHostingEnvironment.WebRootPath}\\UploadedFiles\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            ViewData["message"] = $"File uploaded Successful. File Length : {file.Length}bytes";
            return View("index");
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}