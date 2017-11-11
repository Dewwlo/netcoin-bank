using NetcoinDbLib;
using NetcoinLib;

namespace NetcoinCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu(new BankSystem(SerializationRepository.Instance));
            menu.MenuSelection(args);           
        }
    }
}
