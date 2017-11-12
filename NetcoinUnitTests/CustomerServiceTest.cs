using NetcoinLib;
using NetcoinLib.Models;
using NetcoinLib.Services;
using NetcoinUnitTests.Models;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NetcoinUnitTests
{
    public class CustomerServiceTest
    {
        [Fact]
        public void CanCreateCustomer()
        {
            INetcoinRepository repository = new FakeNetcoinRepository();
            CustomerService sut = new CustomerService(repository);
            string legalId = "101112-1234";
            bool result = sut.CreateCustomer("John Doe", legalId, "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige","070-123 123 123");
            Assert.True(result);
            Customer customer = repository.GetCustomers()[0];
            Assert.Equal(legalId, customer.LegalId);
        }

        [Fact]
        public void CustomerSearchTest()
        {
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetCustomers().AddRange(representation.Customers);
            CustomerService sut = new CustomerService(fakeProvider);
            var result = sut.SearchAfterCustomerWithAreaOrName("Name");
            Assert.Equal(fakeProvider.GetCustomers().Count, result.Count);
        }

        [Fact]
        public void CustomerSearchByCustomerId()
        {

        }

        [Fact]
        public void RemoveCustomerValidatesAndRemoves()
        {
            //Assemble
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            var setupData = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(1, 100M);
            fakeProvider.GetAccounts().AddRange(setupData.Accounts);
            fakeProvider.GetCustomers().AddRange(setupData.Customers);
            Customer customer = setupData.Customers[0];
            CustomerService sut = new CustomerService(fakeProvider);

            //Act & assert
            Assert.False(sut.RemoveCustomer(customer));
            customer.Accounts.ElementAt(0).Balance = 0M;
            customer.Accounts.ElementAt(1).Balance = 0M;
            Assert.True(sut.RemoveCustomer(customer));
            Assert.True(fakeProvider.GetCustomers().Count == 0);
            Assert.True(fakeProvider.GetAccounts().Count == 0);
        }
    }
}
