using Saturn72.Common.Data.Repositories;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Clients;
using Titan.DB.Model.Models.Server;

namespace Titan.DB.Model.Repositories
{
    public class AutomationClientRepository : RepositoryBase<AutomationClientDomainModel, long, AutomationClient>,
        IAutomationClientRepository
    {
        #region ctor

        public AutomationClientRepository(IUnitOfWork<AutomationClientDomainModel, long> unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        public AutomationClientDomainModel GetAutoClientById(long clientId)
        {
            return GetById(clientId);
        }
    }
}