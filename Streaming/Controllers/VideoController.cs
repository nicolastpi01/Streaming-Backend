using System;
using System.Threading.Tasks;
using S = System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {

        private const string videoFilePath = "~/Movies/";
        private System.Collections.ArrayList listaVideos;

        public VideoController(IConfiguration Configuration)
        {
            listaVideos = new System.Collections.ArrayList();
            listaVideos.Add(Configuration["video1"]);
            listaVideos.Add(Configuration["video2"]);
            listaVideos.Add(Configuration["video3"]);
        }

        [Route("getFileById")]
        public FileResult getFileById(int fileId)
        {
            fileId = fileId < 0 ? 0 : fileId;
            fileId = fileId > 2 ? 2 : fileId;
            return PhysicalFile(Path.GetFullPath(@listaVideos[fileId] as string), "application/octet-stream", enableRangeProcessing: true);
        }

        /// Gets the live video.
        [HttpGet]
        //[Route("api/video/")] -> Si no tiene nada esta es la ruta por defecto
        //public S.IHttpActionResult GetLiveVideo(string fileName)
        public String GetLiveVideo()
        {
            //string filePath = Path.Combine(Path.GetDirectoryName(videoFilePath), "small.mp4");
            //Console.WriteLine(filePath);
            //return new VideoFileActionResult(filePath);
            return "This is my default action...";
        }

        /// Gets the live video using post.
        /// The request.
        /*
        [HttpPost]
        [Route("api/video/live")]
        public S.IHttpActionResult GetLiveVideoPost(VideoFileDownloadRequest request)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath(videoFilePath), request.FileName);
            return new VideoFileActionResult(filePath);
        } 
        */
    }

    /// Action Result for Returning Stream
    public class VideoFileActionResult : S.IHttpActionResult
    {
        private const long BufferLength = 65536;
        public VideoFileActionResult(string videoFilePath)
        {
            this.Filepath = videoFilePath;
        }

        public string Filepath { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            FileInfo fileInfo = new FileInfo(this.Filepath);
            long totalLength = fileInfo.Length;
            response.Content = new PushStreamContent((outputStream, httpContent, transportContext) =>
            {
                OnStreamConnected(outputStream, httpContent, transportContext);
            });

            response.Content.Headers.ContentLength = totalLength;
            return Task.FromResult(response);
        }

        private async void OnStreamConnected(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[BufferLength];

                using (var nypdVideo = File.Open(this.Filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var videoLength = (int)nypdVideo.Length;
                    var videoBytesRead = 1;

                    while (videoLength > 0 && videoBytesRead > 0)
                    {
                        videoBytesRead = nypdVideo.Read(buffer, 0, Math.Min(videoLength, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, videoBytesRead);
                        videoLength -= videoBytesRead;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                // Close output stream as we are done
                outputStream.Close();
            }
        }
        
    }
}