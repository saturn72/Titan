
namespace Titan.Framework.Validation
{
    public class ValidationStep
    {
        public bool Result { get; set; }
        public string Message { get; set; }


        public static ValidationStep BuildValidationResponseStep(bool result, string message)
        {
            return new ValidationStep
            {
                Result = result,
                Message = message
            };
        }
    }
}