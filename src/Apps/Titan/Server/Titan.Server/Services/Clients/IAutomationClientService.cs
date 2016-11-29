using System.Threading.Tasks;
using Titan.Common.Domain.Clients;

namespace Titan.Server.Services.Clients
{
    public interface IAutomationClientService
    {
        Task<AutomationClientDomainModel> GetClientByIdAsync(long clientId);
    }
}
