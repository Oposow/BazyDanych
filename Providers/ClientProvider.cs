using SqlConferenceManagementSystemGenerator.Models;
using SqlConferenceManagementSystemGenerator.Consts;
using System.Collections.Generic;
using System;
using System.Text;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class ClientProvider
    {
        int LastClientId { get; set; }
        List<string> ClientsNames { get; set; }
        Random randomGenerator { get; set; }
        AddressProvider _adressProvider { get; set; }
        List<string> FirstNames;
        List<string> LastNames;
        List<string> emails;
        public ClientProvider(int LastClientId, List<string> clientsNames, AddressProvider iAddressProvider, List<string> firstNames, List<string> lastNames, List<string> Emails)
        {
            this.LastClientId = LastClientId;
            ClientsNames = clientsNames;
            _adressProvider = iAddressProvider;
            FirstNames = firstNames;
            LastNames = lastNames;
            randomGenerator = new Random();
            emails = Emails;
        }

        public ClientModel CreateClient()
        {
            var result = new ClientModel()
            {
                ClientId = ++LastClientId,
                ClientType = (ClientType)randomGenerator.Next(0, 2),
                ContactAddress = _adressProvider.GenerateAddress(),
                RegisterAddress = _adressProvider.GenerateAddress(),
                Phone = randomGenerator.Next(100000000, 999999999).ToString(),
                Email = emails[randomGenerator.Next(0,emails.Count)]
            };
            if (result.ClientType == ClientType.IndividualClient)
            {
                result.IdentityCardNumber = new StringBuilder().Append((char)randomGenerator.Next(65, 91)).Append((char)randomGenerator.Next(65, 91)).Append((char)randomGenerator.Next(65, 91)).Append(randomGenerator.Next(100000, 999999)).ToString();
                if (randomGenerator.Next() % 3 == 0)
                    result.StudentCardId = randomGenerator.Next(100000, 999999).ToString();
                result.ClientName = FirstNames[randomGenerator.Next(0, FirstNames.Count)] + " " + LastNames[randomGenerator.Next(0, LastNames.Count)];
            }
            else
            {
                result.NIP = randomGenerator.Next(10000, 99999).ToString() + randomGenerator.Next(10000, 99999);
                result.REGON = randomGenerator.Next(100000000, 999999999).ToString();
                result.ClientName = ClientsNames[randomGenerator.Next(0, ClientsNames.Count)];
            }

            return result;
        }
    }
}
