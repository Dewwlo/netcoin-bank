using System;

namespace NetcoinCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.ShowBankLogo();
            menu.ShowMenu();
            menu.MenuSelection();           
        }
    }
}
