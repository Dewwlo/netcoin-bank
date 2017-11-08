using NetcoinLib.Models;
using NetcoinUnitTests.Models;
using System;
using System.Collections.Generic;

namespace NetcoinUnitTests.Utilities
{
    public static class NetcoinRepositoryUtility
    {
        //returns a sample to use with the netcoin fake repository
        //if no default account balance is given, the balance is the same as the customer id
        //if a default is given it is applied to every account
        //the customer names and IDs start on 1 and follows a pattern like this: CustomerId = 1, Name = 1Name
        //every customer gets two accounts with unique account IDs starting on 1
        public static NetcoinRepoRepresentation CreateSampleCustomersAndAccounts(int amountOfCustomers, decimal? defaultAccountBalance = null)
        {
            NetcoinRepoRepresentation retVal = new NetcoinRepoRepresentation();
            int accountNoTracker = 1;
            for (int i = 1; i <= amountOfCustomers; i++)
            {
                Customer customer = new Customer()
                {
                    CustomerId = i,
                    Name = $"{ i }Name",
                    Address = $"{ i }Address",
                    Area = $"{ i }Area",
                    LegalId = $"{ i }LegalId",
                    PostalCode = $"{ i }PostalCode",
                    Accounts = new List<Account>()
                };

                for (int n = 0; n < 2; n++)
                {
                    Account account;
                    if (defaultAccountBalance != null)
                        account = CreateAccountForSample(accountNoTracker, (decimal)defaultAccountBalance, customer);
                    else
                        account = CreateAccountForSample(accountNoTracker, Convert.ToDecimal(i), customer);

                    accountNoTracker++;
                    customer.Accounts.Add(account);
                    retVal.Accounts.Add(account);
                }

                retVal.Customers.Add(customer);
            }

            return retVal;
        }

        private static Account CreateAccountForSample(int accountId, decimal balance, Customer customer)
        {
            return new Account() { AccountId = accountId, Balance = balance, CustomerId = customer.CustomerId, Customer = customer };
        }
    }
}
