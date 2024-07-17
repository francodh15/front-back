using apiBack.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace apiBack.Repositorios
{
    public class CustomerDataBaseContext : DbContext
    {
        public CustomerDataBaseContext(DbContextOptions<CustomerDataBaseContext> options) :
            base(options)
        {

        }
        public DbSet<CustomerEntity> tableback { get; set; }

        public async Task<CustomerEntity> Get(int id)
        {
            return await tableback.FirstAsync(x => x.Id == id);
        }
        public async Task<bool> Delete(int id)
        {
            CustomerEntity entity = await Get(id);
            tableback.Remove(entity);
            SaveChanges();
            return true;
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                address = customerDto.address,
                firstName = customerDto.firstName,
                lastName = customerDto.lastName,
                email = customerDto.email,
                phone = customerDto.phone
            };
            EntityEntry<CustomerEntity> response = await tableback.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ah podido guardar"));

        }
    }
    public class CustomerEntity
    {
        public int? Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                address = address,
                firstName = firstName,
                lastName = lastName,
                email = email,
                phone = phone,
                Id = Id ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}
