using System.Threading.Tasks;
using Saturn72.Extensions;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Clients;

namespace Titan.Server.Services.Clients
{
    public class AutomationClientService : IAutomationClientService
    {
        private readonly IAutomationClientRepository _automationClientRepository;

        public AutomationClientService(IAutomationClientRepository automationClientRepository)
        {
            _automationClientRepository = automationClientRepository;
        }

        public Task<AutomationClientDomainModel> GetClientByIdAsync(long clientId)
        {
            Guard.GreaterThan(clientId, (long) 0, "Client Id should be greater than 0");

            return Task.FromResult(_automationClientRepository.GetAutoClientById(clientId));
        }
    }
}