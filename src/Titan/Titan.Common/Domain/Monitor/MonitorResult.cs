using System;
using Saturn72.Core.Audit;
using Saturn72.Core.Domain;

namespace Titan.Common.Domain.Monitor
{
    public class MonitorResult:DomainModelBase, IUpdatedAudit
    {
        public DateTime CreatedOnUtc { get; set; }
        public long CreatedByUserId { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public long? UpdatedByUserId { get; set; }
        public string Actual { get; set; }
        public string ActualType { get; set; }
        public string Expected { get; set; }
        public string ExpectedType { get; set; }
        public string ComparisonTypeCode { get; set; }
    }
}
