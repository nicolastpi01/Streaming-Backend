using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Streaming.Controllers;
using Streaming.Infraestructura.Entities;

namespace Streaming.Infraestructura.Repositories.contracts
{
    public interface IStreamRepository : IBaseRepository<MediaEntity>
    {
        MediaEntity getMediaById(string Id);
        Task<List<MediaEntity>> PaginarMedia(int indice, int offset);
        Task<List<string>> GetSugerencias(string sugerencia);
        Task<List<MediaEntity>> SearchVideos(string busqueda);
        int GetTotalVideos();
        //FileStreamResult GetFile(string path, ControllerBase controller);
        FileStreamResult GetFileById(string fileId, ControllerBase videoController);
        FileStreamResult GetImagenById(string fileId, ControllerBase controller);
    }
}
