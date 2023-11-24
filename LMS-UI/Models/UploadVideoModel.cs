using System.ComponentModel.DataAnnotations;

namespace LMS_UI.Models
{
    public class UploadVideoModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a video file.")]
        [Display(Name = "Video File")]
        public IFormFile VideoFile { get; set; }
    }
}
