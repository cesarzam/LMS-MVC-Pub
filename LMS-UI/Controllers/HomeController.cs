using LMS_UI.Models;
using LMS_UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LMS_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VideoService _videoService;
        public HomeController(ILogger<HomeController> logger,VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;   
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyCourse()
        {
            return View();
        }
        public IActionResult CourseCatalog()
        {
            return View();
        }
        public IActionResult CourseSchedule()
        {
            return View();
        }

        public IActionResult Certificates()
        {
            return View();
        }

        public IActionResult Resources()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult Support()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult VideoAdmin()
        {
            var videos = _videoService.GetDummyVideos();
            return View(videos);
        }
        public IActionResult VideoUser()
        {
            var videos = _videoService.GetDummyVideos();
            return View(videos);

        }

        public IActionResult UploadVideo()
        {
            return View();
        }

        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> Upload(UploadVideoModel model, IFormFile videoFile)
        {
            if (ModelState.IsValid)
            {
                if (videoFile != null && videoFile.Length > 0 && videoFile.Length <= 1_073_741_824)
                {
                    var filePath = Path.Combine("D:\\LMS-Repo-Videos", videoFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                    }

                    var newVideo = new Video
                    {
                        Title = model.Title,
                        Description = model.Description,
                        FilePath = filePath
                    };

                    _videoService.AddVideo(newVideo);

                    return RedirectToAction("VideoAdmin");
                }
                else
                {

                    ModelState.AddModelError("VideoFile", "Please upload a file less than 1 GB.");
                }
            
            }
            else
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"Model Error: {error.ErrorMessage}");
                    }
                }
            }

            return View(model);
        }

        public IActionResult ViewVideo([FromRoute] int Id)
        {
            var video = _videoService.GetVideoById(Id);

            return View(video);
        }

        public IActionResult UserViewVideo([FromRoute] int Id)
        {
            var video = _videoService.GetVideoById(Id);

            return View(video);
        }


        [HttpGet("/Videos/{videoFileName}")]
        public IActionResult GetVideo(string videoFileName)
        {
            var videoPath = Path.Combine("D:/LMS-Repo-Videos", videoFileName);

            if (System.IO.File.Exists(videoPath))
            {
                var stream = System.IO.File.OpenRead(videoPath);
                return File(stream, "video/mp4");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult TrackVideoPlayback(int videoId, double completionPercentage, bool isVideoEnd)
        {
            // Simulate storing tracking data in a database or performing other actions
            Console.WriteLine($"Video ID: {videoId}, Completion Percentage: {completionPercentage}%, Video End: {isVideoEnd}");

            // Update the video status if it's completed
            if (isVideoEnd && completionPercentage >= 100)
            {
                _videoService.UpdateVideoStatus(videoId, "Completed");
            }

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}