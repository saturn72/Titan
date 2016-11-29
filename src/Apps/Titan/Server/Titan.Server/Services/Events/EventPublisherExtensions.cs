using Saturn72.Core.Domain;

namespace Saturn72.Core.Services.Events
{
    public static class EventPublisherExtensions
    {
        public static void DomainModelUpdated<TDomainModel>(this IEventPublisher eventPublisher,
            TDomainModel domainModel)
            where TDomainModel : DomainModelBase<long>
        {
            eventPublisher.DomainModelUpdated<TDomainModel, long>(domainModel);
        }

        public static void DomainModelCreated<TDomainModel>(this IEventPublisher eventPublisher,
            TDomainModel domainModel)
            where TDomainModel : DomainModelBase<long>
        {
            eventPublisher.DomainModelCreated<TDomainModel, long>(domainModel);
        }

        public static void DomainModelDeleted<TDomainModel>(this IEventPublisher eventPublisher,
            TDomainModel domainModel)
            where TDomainModel : DomainModelBase<long>
        {
            eventPublisher.DomainModelDeleted<TDomainModel, long>(domainModel);
        }
    }
}