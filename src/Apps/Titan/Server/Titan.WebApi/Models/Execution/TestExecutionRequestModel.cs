using Saturn72.Common.WebApi.Models;

namespace Titan.WebApi.Models.Execution
{
    public class TestExecutionRequestModel : ApiModelBase
    {
        public long TestId { get; set; }
        public long AutomationClientId { get; set; }
    }
}