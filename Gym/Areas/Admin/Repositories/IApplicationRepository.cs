using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public interface IApplicationRepository<TEntity>
    {
        IList<TEntity> List();

        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
