using NetcoinLib;
using NetcoinLib.Models;
using NetcoinLib.Services;
using NetcoinUnitTests.Repositories;
using Xunit;

namespace NetcoinUnitTests
{

    public class AccountServiceTests
    {
        [Fact]
        public void CanCreateAccount()
        {

            INetcoinRepository repository = new FakeNetcoinRepository();
            CustomerService cService = new CustomerService(repository);
            cService.CreateCustomer("John Doe", "101112-1234", "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 123");
            Customer customer = repository.GetCustomers().Find(x => x.LegalId == "101112-1234");
            AccountService sut = new AccountService(repository);
            
            bool result = sut.CreateAccount(customer);
            Assert.True(result);

            result = sut.CreateAccount(customer.CustomerId);
            Assert.True(result);
        }
    }
}
