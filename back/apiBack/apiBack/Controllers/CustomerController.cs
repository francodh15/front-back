using apiBack.casos_de_uso;
using apiBack.Dto;
using apiBack.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiBack.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomerController : Controller

    {
        public readonly CustomerDataBaseContext _customerDataBaseContext;
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

        public CustomerController(CustomerDataBaseContext customerDataBaseContext, IUpdateCustomerUseCase updateCustomerUseCase)
        {
            _customerDataBaseContext = customerDataBaseContext;
            _updateCustomerUseCase = updateCustomerUseCase;
        }

        //Esto va a recibir una url asi "api/customer"
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]

        public async Task<IActionResult> GetCustomer()
        {
            var result = _customerDataBaseContext.tableback
              .Select(c => c.ToDto()).ToList();

            return new OkObjectResult(result);
        }
        //Esto va a recibir una url asi "api/customer/{id}"

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            CustomerEntity result = await _customerDataBaseContext.Get(id);
            return new OkObjectResult(result.ToDto);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]

        public async Task<IActionResult> DeleteCustomer (int id)
        {
            var result = await _customerDataBaseContext.Delete(id);
            return new OkObjectResult(result);

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]

        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await _customerDataBaseContext.Add(customer);
            return new CreatedResult($"https://localhost:7030/api/customer/{result.Id}",null);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
