﻿using Guardian.Domain.Interfaces.IRepository;
using Guardian.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Guardian.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _db.GuardianTasks.Include(u => u.GuardianModel).ToList();
            this.dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            //It does not get execute right away
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null) //Responsible to select some specif properties that has FK relation in DataBase
            {
                foreach (var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))//In case we have more than one property
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1)
        {
            //It does not get execute right away
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (pageSize > 0) //Only if page size is provided
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }

                //pageNumber = 2 AND size = 5
                //skip.(5 * (2-1).Take(5)
                //skip.(5*1).Take(5)                
                //In this case we will skip 5 and take five record on the next page
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);//Basic skip for pagination
            }

            if (includeProperties != null) //Responsible to select some specif properties that has FK relation in DataBase
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync(); //This is deffered execution. ToList() causes immediate execution
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
