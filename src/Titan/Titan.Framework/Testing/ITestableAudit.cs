using System;

namespace Titan.Framework.Testing
{
    public interface ITestableAudit
    {
        DateTime CreatedOnUtc { get; }
        DateTime DisposedOnUtc { get; }
        DateTime ExecutionStartedOnUtc { get; }
        DateTime ExecutionEndedOnUtc { get; }
    }
}