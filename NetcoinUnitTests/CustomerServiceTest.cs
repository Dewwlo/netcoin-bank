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
        private readonly INetcoinRepository _repository;
        public CustomerServiceTest()
        {
            _repository = new FakeNetcoinRepository();
        }

        [Fact]
        public void CanCreateCustomer()
        {
            CustomerService sut = new CustomerService(_repository);
            string legalId = "101112-1234";
            bool result = sut.CreateCustomer("John Doe", legalId, "Stockholms Län", "Gustav Adolfs Torg 1", "12312", "Stockholm", "Sverige", "070-123 123 123");
            Assert.True(result);
            Customer customer = _repository.GetCustomers()[0];
            Assert.Equal(legalId, customer.LegalId);
        }

        [Fact]
        public void CustomerSearchTest()
        {
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5);
            _repository.GetCustomers().AddRange(representation.Customers);
            CustomerService sut = new CustomerService(_repository);
            var result = sut.SearchAfterCustomerWithAreaOrName("Name");
            Assert.Equal(_repository.GetCustomers().Count, result.Count);
        }

        [Fact]
        public void CustomerSearchByCustomerId()
        {
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetCustomers().AddRange(representation.Customers);
            CustomerService sut = new CustomerService(fakeProvider);
            var result = sut.GetCustomerByCustomerId("1");
            Assert.Equal(1, result.CustomerId);
        }

        [Fact]
        public void RemoveCustomerValidatesAndRemoves()
        {
            //Assemble
            var setupData = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(1, 100M);
            _repository.GetAccounts().AddRange(setupData.Accounts);
            _repository.GetCustomers().AddRange(setupData.Customers);
            Customer customer = setupData.Customers[0];
            CustomerService sut = new CustomerService(_repository);

            //Act & assert
            Assert.False(sut.RemoveCustomer(customer));
            customer.Accounts.ElementAt(0).Balance = 0M;
            customer.Accounts.ElementAt(1).Balance = 0M;
            Assert.True(sut.RemoveCustomer(customer));
            Assert.True(_repository.GetCustomers().Count == 0);
            Assert.True(_repository.GetAccounts().Count == 0);
        }
    }
}
