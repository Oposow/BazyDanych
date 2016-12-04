using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConferenceManagementSystemGenerator.Models;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class AddressProvider
    {
        readonly List<string> Cities;
        readonly List<string> Streets;

        public AddressProvider(List<string> _Cities, List<string> _Streets)
        {
            Cities = _Cities;
            Streets = _Streets;
        }

        public AddressModel GenerateAddress()
        {
            var randomGenerator = new Random();
            return new AddressModel()
            {
                Street = new StringBuilder().Append(Streets[randomGenerator.Next(0, Streets.Count())]).Append(' ').Append(randomGenerator.Next(1, 101)).ToString(),
                City = Cities[randomGenerator.Next(0, Cities.Count())],
                Country = "USA",
                PostalCode = new StringBuilder().Append(randomGenerator.Next(10, 100)).Append('-').Append(randomGenerator.Next(100, 1000)).ToString()
            };
    
        }
    }
}
