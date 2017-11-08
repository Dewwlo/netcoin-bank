using NetcoinLib;
using NetcoinLib.Services;
using NetcoinUnitTests.Models;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NetcoinUnitTests
{
    public class CustomerServiceTest
    {
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
    }
}
