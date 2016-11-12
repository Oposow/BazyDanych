using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class ParticipantModel
    {
        public int ParticipantId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string UserStamp { get; set; }
        public string TimeStamp { get; set; }
        public string PESEL { get; set; }

    }
}
