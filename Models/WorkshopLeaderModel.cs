using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class WorkshopLeaderModel
    {
        public int LeaderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserStamp { get; set; }
    }
}
