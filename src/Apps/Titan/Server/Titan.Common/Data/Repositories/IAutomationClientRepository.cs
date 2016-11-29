using Titan.Common.Domain.Clients;

namespace Titan.Common.Data.Repositories
{
    public interface IAutomationClientRepository
    {
        AutomationClientDomainModel GetAutoClientById(long clientId);
    }
}