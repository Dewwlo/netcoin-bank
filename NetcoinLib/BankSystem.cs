using System;

namespace NetcoinLib
{
    public class BankSystem
    {
        private readonly INetcoinRepository _netcoinRepository;
        public BankSystem(INetcoinRepository netcoinRepository)
        {
            _netcoinRepository = netcoinRepository;
        }

        public void ReadTextFile(string fileName) => _netcoinRepository.ReadSerializedData(fileName);

        public void SaveTextFile() => _netcoinRepository.Save();
    }
}
