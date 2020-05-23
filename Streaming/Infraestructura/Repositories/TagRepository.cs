using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Repositories
{
    public class TagRepository : BaseRepository<TagEntity>, ITagRepository
    {
        public TagRepository(DbContext context) : base(context)
        {
        }

        public override async Task<List<TagEntity>> GetAll()
        {
            return await ((MediaContext)_context).Tags.Select(x => x).ToListAsync();
        }
    }
}
