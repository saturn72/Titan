using System;
using Saturn72.Core.Audit;
using Saturn72.Core.Domain;

namespace Titan.Common.Domain.Execution
{
    public class TestExecutionRequestDomainModel : DomainModelBase<long>, ICreatedAudit
    {
        public long TestId { get; set; }
        public long ClientId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}