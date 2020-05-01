using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Repositories
{
    public class MediaRepository : BaseRepository<MediaEntity>, IMediaRepository
    {
        public MediaRepository(DbContext context) : base(context)
        {
        }

        public override async Task<List<MediaEntity>> GetAll()
        {
            return await ((MediaContext)_context).Medias.Select(x => x).ToListAsync();
        }
    }    
}
