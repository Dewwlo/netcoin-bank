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
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5, 100M);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetAccounts().AddRange(representation.Accounts);
            BankSystem sut = new BankSystem(fakeProvider);
            sut.Initialize();

            //Act
            sut.WithdrawFromAccount(2, 100M);

            //Act & assert
            Assert.True(fakeProvider.GetAccounts().Single(x => x.AccountId == 2).Balance == 0);
            Assert.Throws<InvalidOperationException>(() => sut.WithdrawFromAccount(1, 101M));
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithdrawFromAccount(1, -1M));
            Assert.Throws<NullReferenceException>(() => sut.WithdrawFromAccount(100, 1M));
        }

        [Fact]
        public void DepositToAccount()
        {
            //Assemble
            NetcoinRepoRepresentation representation = NetcoinRepositoryUtility.CreateSampleCustomersAndAccounts(5, 0M);
            INetcoinRepository fakeProvider = new FakeNetcoinRepository();
            fakeProvider.GetAccounts().AddRange(representation.Accounts);
            BankSystem sut = new BankSystem(fakeProvider);
            sut.Initialize();

            //Act
            sut.DepositToAccount(2, 100M);

            //Act & assert
            Assert.True(fakeProvider.GetAccounts().Single(x => x.AccountId == 2).Balance == 100);
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.WithdrawFromAccount(1, -1M));
            Assert.Throws<NullReferenceException>(() => sut.WithdrawFromAccount(100, 1M));
        }
    }
}
