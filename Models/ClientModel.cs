using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConferenceManagementSystemGenerator.Consts;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class ClientModel
    {
        public int ClientId { get; set; }
        public ClientType ClientType { get; set; }
        public string ClientName { get; set; }
        public AddressModel RegisterAddress { get; set; }
        public AddressModel ContactAddress { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string PESEL { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TimeStamp { get;set;}
        public string StudentCardId { get; set; }
    }
}
