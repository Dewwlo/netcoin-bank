using NetcoinLib;
using NetcoinUnitTests.Models;
using NetcoinUnitTests.Repositories;
using NetcoinUnitTests.Utilities;
using System;
using System.Linq;
using Xunit;

namespace NetcoinUnitTests
{
    public class BankSystemTests
    {
        [Fact]
        public void WithdrawFromAccount()
        {
            //Assemble
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetAccounts().AddRange(representation.Accounts);
            BankSystem sut = new BankSystem(fakeProvider);

            //Act
            sut.WithdrawFromAccount(2, 1);

            //Act & assert
            Assert.True(fakeProvider.GetAccounts().Single(x => x.AccountId == 2).Balance == 0);
            Assert.Throws<Exception>(() => sut.WithdrawFromAccount(1, 100));
            Assert.Throws<NullReferenceException>(() => sut.WithdrawFromAccount(100, 1));
        }
    }
}
