using NetcoinLib;
using NetcoinUnitTests.Models;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using Xunit;

namespace NetcoinUnitTests
{
    public class FakeRepositoryTests
    {
        [Fact]
        public void FakeRepositoryAndSampleCreationWorksForTests()
        {
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomers(5);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetCustomers().AddRange(representation.Customers);
            fakeProvider.GetAccounts().AddRange(representation.Accounts);
            
            Assert.True(fakeProvider.GetCustomers().Count == 5);
            Assert.True(fakeProvider.GetAccounts().Count == 10);
        }
    }
}
