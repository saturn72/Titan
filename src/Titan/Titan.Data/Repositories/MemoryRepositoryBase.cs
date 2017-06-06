using System.Collections.Generic;
using System.Linq;
using Saturn72.Core.Domain;

namespace Titan.Data.Repositories
{
    public abstract class MemoryRepositoryBase<TDomainModel> where TDomainModel: DomainModelBase
    {
        protected static long Index;
        protected static ICollection<TDomainModel> RepositoryData = new List<TDomainModel>();

        protected ICollection<TDomainModel> GetTableAsCollection()
        {
            return RepositoryData;
        }

        public virtual IEnumerable<TDomainModel> Collection
        {
            get { return RepositoryData.ToArray(); }
        }

        public virtual void Add(TDomainModel model)
        {
            model.Id = ++Index;
            RepositoryData.Add(model);
        }
    }
}