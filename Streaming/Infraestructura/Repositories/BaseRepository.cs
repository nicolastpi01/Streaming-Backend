using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura.Repositories.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual int ExecuteSqlCommand(string command)
        {
            object[] args = new object[0];
            return _context.Database.ExecuteSqlCommand(command, args);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entityToDelete);
            }
            _context.Set<TEntity>().Remove(entityToDelete);
            _context.SaveChangesAsync();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
            _context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetByPage(int page, int cantidad)
        {
            var data = await _context.Set<TEntity>().ToListAsync();
            return data.Skip((page - 1) * cantidad).Take(cantidad).ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

    }
}
