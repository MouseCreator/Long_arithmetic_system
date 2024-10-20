using Microsoft.AspNetCore.Mvc;
using API.BinaryWrappers;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiniteFieldController : ControllerBase
    {
        [HttpPost("add")]
        public ActionResult<string> Add([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.Add(request.NumberA, request.NumberB, request.Mod, request.ErrStr);
            return Ok(result);
        }

        [HttpPost("subtract")]
        public ActionResult<string> Subtract([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.Subtract(request.NumberA, request.NumberB, request.Mod, request.ErrStr);
            return Ok(result);
        }

        [HttpPost("multiply")]
        public ActionResult<string> Multiply([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.Multiply(request.NumberA, request.NumberB, request.Mod, request.ErrStr);
            return Ok(result);
        }

        [HttpPost("divide")]
        public ActionResult<string> Divide([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.Divide(request.NumberA, request.NumberB, request.Mod, request.ErrStr);
            return Ok(result);
        }

        [HttpPost("fastpow")]
        public ActionResult<string> FastPow([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.FastPow(request.NumberA, request.Degree, request.Mod, request.ErrStr);
            return Ok(result);
        }

        [HttpPost("inverse")]
        public ActionResult<string> Inverse([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.Inverse(request.NumberA, request.Mod, request.ErrStr);
            return Ok(result);
        }

        // Add more endpoints for the other methods similarly...

        // Example for checking if a number is a generator
        [HttpPost("is-generator")]
        public ActionResult<bool> IsGenerator([FromBody] FiniteFieldOperationRequest request)
        {
            var result = FiniteFieldWrapper.IsGenerator(request.NumberA, request.Mod, request.ErrStr);
            return Ok(result);
        }
    }

    public class FiniteFieldOperationRequest
    {
        public string NumberA { get; set; }
        public string NumberB { get; set; }
        public string Mod { get; set; }
        public string ErrStr { get; set; }
        public string Degree { get; set; } // Optional for operations that need it
    }
    
    public class FinitFieldOperationWithOneNumberRequest
    {
        public string Number { get; set; }
        public string Mod { get; set; }
        public string ErrStr { get; set; }
    }
}
