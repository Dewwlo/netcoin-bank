using System;
using Microsoft.VisualBasic.CompilerServices;
using NetcoinDbLib;
using NetcoinLib;

namespace NetcoinCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankSystem = new BankSystem(new SerializationRepository());
            bankSystem.ReadTextFile(args[0]);
        }
    }
}
