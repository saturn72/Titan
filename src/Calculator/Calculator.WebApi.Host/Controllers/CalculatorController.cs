using System.Collections.Generic;
using System.Web.Http;
using Calculator.WebApi.Host.Data;
using Calculator.WebApi.Host.Domain;

namespace Calculator.WebApi.Host.Controllers
{
    namespace OwinSelfhostSample
    {
        [RoutePrefix("api/calculator")]
        public class CalculatorController : ApiController
        {
            [HttpPost]
            [Route("add/{x}/{y}")]
            public Expression Add(int x, int y)
            {
                return ExpressionService.Add(x, y);
            }

            [HttpPost]
            [Route("sub/{x}/{y}")]
            public Expression Sub(int x, int y)
            {
              
                return ExpressionService.Subtract(x, y);
            }

            [HttpPost]
            [Route("mul/{x}/{y}")]
            public Expression Multiply(int x, int y)
            {
               
                return ExpressionService.Multiply(x,y);
            }

            [HttpPost]
            [Route("div/{x}/{y}")]
            public Expression Divide(int x, int y)
            {
                return ExpressionService.Divide(x:x, y:y);
            }

            // GET api/values 
            [HttpGet]
            [Route("")]
            public IEnumerable<Expression> GetAll()
            {
                return ExpressionService.All;
            }

            // GET api/values/5 
            [HttpGet]
            [Route("{id}")]
            public Expression Get(long id)
            {
                return ExpressionService.GetById(id);
            }
        }
    }
}