using LMS_UI.Models;
using System.Collections.Generic;

namespace LMS_UI.Services
{
    public class VideoService
    {
        private List<Video> _dummyVideos;
        public VideoService ()
        {
            _dummyVideos = new List<Video>
            {
                new Video { Id = 1, Title = "Video 1", Description = "Description for Video 1", Status="", FilePath="D:/LMS-Repo-Videos\\SampleVideo_1280x720_2mb.mp4" },
                new Video { Id = 2, Title = "Video 2", Description = "Description for Video 2", Status="Completed",FilePath = "D:/LMS-Repo-Videos\\SampleVideo_1280x720_2mb.mp4" },
                new Video { Id = 3, Title = "Video 3", Description = "Description for Video 3", Status="", FilePath="D:/LMS-Repo-Videos\\SampleVideo_1280x720_2mb.mp4" }
            };
        }


        public List<Video> GetDummyVideos()
        {
            //return all videos.
            return _dummyVideos;
        }

        public void AddVideo(Video newVideo)
        {
            // Get ID and increment by 1
            newVideo.Id = _dummyVideos.Max(v => v.Id) + 1;
            // Add new video 
            _dummyVideos.Add(newVideo);
        }

        public Video GetVideoById(int videoId)
        {
            //Find video by ID.
            return _dummyVideos.FirstOrDefault(v => v.Id == videoId);
        }

        public void UpdateVideoStatus(int videoId, string status)
        {
            // Update video watching status
            var video = _dummyVideos.Find(v => v.Id == videoId);
            if (video != null)
            {
                video.Status = status;
            }
        }

    }
}
