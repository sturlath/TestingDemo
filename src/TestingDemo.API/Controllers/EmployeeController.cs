using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.Core.ModelsDtos;

namespace TestingDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("/api/employee/{id}")]
        public async Task<IActionResult> GetByIdQuotes([FromRoute][Required]int id)
        {
            try
            {
                var result = await service.GetByIdAsync(id).ConfigureAwait(false);

                if (result.HasError)
                    return BadRequest(result.ErrorMessage);

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("/api/employee/add")]
        public async Task<IActionResult> AddQuote([FromBody]EmployeeDto employeeDto)
        {
            try
            {
                var result = await service.AddAsync(employeeDto).ConfigureAwait(false);

                if (result.HasError)
                    return BadRequest(result.ErrorMessage);

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        //... etc.
    }
}
