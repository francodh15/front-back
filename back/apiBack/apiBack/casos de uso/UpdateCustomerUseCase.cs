using apiBack.Repositorios;

namespace apiBack.casos_de_uso
{

    public interface IUpdateCustomerUseCase
    {
        Task<Dto.CustomerDto?> Execute(Dto.CustomerDto customer);
    }

    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly CustomerDataBaseContext _customerDataBaseContext;

        public UpdateCustomerUseCase(CustomerDataBaseContext customerDataBaseContext)
        {

            _customerDataBaseContext = customerDataBaseContext;
        }
        public async Task<Dto.CustomerDto?> Execute(Dto.CustomerDto customer)
        {
            var entity = await _customerDataBaseContext.Get(customer.Id);

            if (entity == null)
                return null;
            entity.firstName = customer.firstName;
            entity.lastName = customer.lastName;
            entity.email = customer.email;
            entity.phone = customer.phone;
            entity.address = customer.address;

            await _customerDataBaseContext.Actualizar(entity);

            return entity.ToDto();

        }
    }
}
