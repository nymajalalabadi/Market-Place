using MarketPlace.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetQuery();

        Task AddEntity(TEntity entity);

        Task AddRangeEntities(List<TEntity> entities);

        Task<TEntity> GetEntityById(long entityId);

        void EditEntity(TEntity entity);

        void DeleteEntity(TEntity entity);

        Task DeleteEntity(long entityId);

        void DeletePermanent(TEntity entity);

        void DeletePermanentEntities(List<TEntity> entities);

        Task DeletePermanent(long entityId);

        Task SaveChanges();

    }
}
