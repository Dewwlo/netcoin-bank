using NetcoinLib;
using NetcoinLib.Models;
using NetcoinLib.Services;
using NetcoinUnitTests.Repositories;
using System;
using System.Collections.Generic;
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

        }
    }
}
