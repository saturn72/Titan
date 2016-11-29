using System;
using Saturn72.Core.Audit;
using Saturn72.Core.Domain;

namespace Titan.Common.Domain.Testing
{
    public class TestDomainModel : DomainModelBase<long>, IFullAudit
    {
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
    }
}