using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Streaming.Infraestructura.Entities;

namespace Streaming.Infraestructura.Repositories.contracts
{
    public interface IMediaRepository : IBaseRepository<MediaEntity>
    {
        //Task<RespuestaModel> InsertParametria(ComisionParametria parametria);

        //List<ComisionParametria> getFila(string sistema, string CUIT, string producto, string concepto, string segmento, string canal);
        //Task<RespuestaModel> UpdateParametria(ComisionParametria parametria);


        //Task<RespuestaModel> DelParametria(int number);
    }
}
