using NetcoinLib;
using NetcoinLib.Models;
using NetcoinLib.Services;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
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

        [Fact]
        public void DeleteAccountValidatesAndRemoves()
        {
            //Assemble
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            var setupData = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(1, 100M);
            fakeProvider.GetAccounts().AddRange(setupData.Accounts);
            Account account1 = setupData.Accounts[0];
            Account account2 = setupData.Accounts[1];
            AccountService sut = new AccountService(fakeProvider);

            //Act & assert
            Assert.False(sut.RemoveAccount(account1));
            account1.Balance = 0M;
            Assert.True(sut.RemoveAccount(account1));
            Assert.False(sut.RemoveAccount(account2));
            Assert.True(setupData.Customers[0].Accounts.Count == 1 && fakeProvider.GetAccounts().Count == 1);
        }
    }
}
